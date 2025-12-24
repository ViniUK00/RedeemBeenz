using System;
using System.Linq;
using NUnit.Framework;
using RedeemBeenz.Component;
using RedeemBeenz.Models;

namespace RedeemBeenz.Tests;

public class RedemptionStoreTests
{
    [Test]
    public void Create_StoresAndReturnsRedemption()
    {
        var store = new RedemptionStore();
        var r = new Redemption
        {
            RedemptionId = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(),
            RewardId = Guid.NewGuid(),
            Status = RedemptionStatus.Confirmed
        };

        store.Create(r);

        Assert.That(store.GetStatus(r.RedemptionId), Is.EqualTo(RedemptionStatus.Confirmed));
    }

    [Test]
    public void GetByEmployee_FiltersCorrectly()
    {
        var store = new RedemptionStore();
        var emp1 = Guid.NewGuid();
        var emp2 = Guid.NewGuid();

        store.Create(new Redemption { RedemptionId = Guid.NewGuid(), EmployeeId = emp1, Status = RedemptionStatus.Confirmed });
        store.Create(new Redemption { RedemptionId = Guid.NewGuid(), EmployeeId = emp2, Status = RedemptionStatus.Confirmed });

        var list = store.GetByEmployee(emp1);

        Assert.That(list, Has.Count.EqualTo(1));
        Assert.That(list[0].EmployeeId, Is.EqualTo(emp1));
    }
}