using RedeemBeenz.Interfaces;
using RedeemBeenz.Models;
using static RedeemBeenz.Component.OclContracts;

namespace RedeemBeenz.Component;

public class BeenzRewards : IRewardsAPI
{
    private readonly IAccountStore _accounts;
    private readonly ICatalogue _catalogue;
    private readonly IRedemptionStore _redemptions;

    public BeenzRewards(IAccountStore accounts, ICatalogue catalogue, IRedemptionStore redemptions)
    {
        _accounts = accounts;
        _catalogue = catalogue;
        _redemptions = redemptions;
    }

    public bool Login(string email, string password)
    {
        // For demo, always true
        return true;
    }

    public int GetBalance(Guid employeeId) => _accounts.GetBalance(employeeId);

    public List<Reward> ListRewards() => _catalogue.ListAll();

    public bool CheckAvailability(Guid rewardId, int qty)
    {
        var r = _catalogue.FindById(rewardId);
        return r != null && r.InStock >= qty;
    }

    public Redemption Redeem(Guid employeeId, Guid rewardId, int qty, DeliveryOption delivery)
    {
        // OCL PRECONDITIONS
        Pre(qty >= 1, "qty must be >= 1");
        Pre(delivery != null, "delivery must not be null");
        Pre(delivery.IsValid(), "delivery must be valid");

        var reward = _catalogue.FindById(rewardId);
        Pre(reward != null, "reward must exist");

        // OCL invariants on Reward (defensive checks)
        Inv(reward.PointsCost > 0, "Reward.pointsCost must be > 0");
        Inv(reward.InStock >= 0, "Reward.inStock must be >= 0");

        Pre(reward.InStock >= qty, "reward stock must be >= qty");

        int cost = reward.PointsCost * qty;

        int balancePre = _accounts.GetBalance(employeeId);
        int stockPre = reward.InStock;

        Pre(balancePre >= cost, "employee balance must be >= cost");

        // ACTION
        _accounts.Debit(employeeId, cost, "Redemption");
        reward.InStock -= qty;

        var redemption = new Redemption
        {
            RedemptionId = Guid.NewGuid(),
            EmployeeId = employeeId,
            RewardId = rewardId,
            Quantity = qty,
            Status = RedemptionStatus.Confirmed,
            RewardName = reward.Name,
            TotalCost = cost
        };

        var result = _redemptions.Create(redemption);

        // OCL POSTCONDITIONS
        Post(result.Status == RedemptionStatus.Confirmed, "result.status must be Confirmed");
        Post(_accounts.GetBalance(employeeId) == balancePre - cost, "balance must decrease by cost");
        Post(reward.InStock == stockPre - qty, "stock must decrease by qty");

        // Balance invariant (never negative)
        Inv(_accounts.GetBalance(employeeId) >= 0, "account balance must never be negative");
        Inv(reward.InStock >= 0, "reward stock must never be negative");

        return result;
    }

    public RedemptionStatus GetRedemptionStatus(Guid redemptionId)
        => _redemptions.GetStatus(redemptionId);

    public bool CancelRedemption(Guid employeeId, Guid redemptionId)
        => _redemptions.Cancel(employeeId, redemptionId);
}
