using RedeemBeenz.Interfaces;
using RedeemBeenz.Models;

namespace RedeemBeenz.Component;

using System;
using System.Collections.Generic;

public class RedemptionStore : IRedemptionStore
{
    private readonly Dictionary<Guid, Redemption> store = new();
    
    public Redemption Create(Redemption r)
    {
        store[r.RedemptionId] = r;
        return r;
    }

    public RedemptionStatus GetStatus(Guid redemptionId)
        => store.ContainsKey(redemptionId) ? store[redemptionId].Status : RedemptionStatus.Failed;

    public bool Cancel(Guid employeeId, Guid redemptionId)
    {
        if (store.ContainsKey(redemptionId))
        {
            store[redemptionId].Status = RedemptionStatus.Cancelled;
            return true;
        }
        return false;
    }

    public List<Redemption> GetByEmployee(Guid employeeId)
        => store.Values.Where(r => r.EmployeeId == employeeId).OrderByDescending(r => r.CreatedAt).ToList();
}
