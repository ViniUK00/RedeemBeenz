using RedeemBeenz.Models;

namespace RedeemBeenz.Interfaces;

public interface IRewardsAPI
{
    bool Login(string email, string password);
    int GetBalance(Guid employeeId);
    List<Reward> ListRewards();
    bool CheckAvailability(Guid rewardId, int qty);
    Redemption Redeem(Guid employeeId, Guid rewardId, int qty, DeliveryOption delivery);
    RedemptionStatus GetRedemptionStatus(Guid redemptionId);
    bool CancelRedemption(Guid employeeId, Guid redemptionId);
}