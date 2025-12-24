using RedeemBeenz.Models;

namespace RedeemBeenz.Interfaces;

public interface IRedemptionStore
{
    Redemption Create(Redemption r);
    RedemptionStatus GetStatus(Guid redemptionId);
    bool Cancel(Guid employeeId, Guid redemptionId);
    List<Redemption> GetByEmployee(Guid employeeId);
}
