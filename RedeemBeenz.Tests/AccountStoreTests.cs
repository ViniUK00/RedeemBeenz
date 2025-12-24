using System;
using RedeemBeenz;
using NUnit.Framework;
using RedeemBeenz.Component;

namespace RedeemBeenz.Tests;

public class AccountStoreTests
{
    [Test]
    public void GetBalance_Default_IsZero()
    {
        var store = new AccountStore();
        var employeeId = Guid.NewGuid();

        var balance = store.GetBalance(employeeId);

        Assert.That(balance, Is.EqualTo(0));
    }

    [Test]
    public void Credit_IncreasesBalance()
    {
        var store = new AccountStore();
        var id = Guid.NewGuid();

        store.Credit(id, 100, "Initial");
        store.Credit(id, 50, "Bonus");

        Assert.That(store.GetBalance(id), Is.EqualTo(150));
    }

    [Test]
    public void Debit_DecreasesBalance_ButNotBelowZero()
    {
        var store = new AccountStore();
        var id = Guid.NewGuid();

        store.Credit(id, 50, "Initial");
        store.Debit(id, 20, "Purchase");
        store.Debit(id, 100, "Too much");

        Assert.That(store.GetBalance(id), Is.EqualTo(0));
    }
}