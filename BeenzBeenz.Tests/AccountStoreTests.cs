namespace BeenzBeenz.Tests;

using System;
using BeenzR;
using Xunit;
public class AccountStoreTests
{
    [Fact]
    public void GetBalance_Default_IsZero()
    {
        var store = new FakeAccountStore();
        var employeeId = Guid.NewGuid();

        int balance = store.GetBalance(employeeId);

        Assert.Equal(0, balance);
    }

    [Fact]
    public void Credit_IncreasesBalance()
    {
        var store = new FakeAccountStore();
        var employeeId = Guid.NewGuid();

        store.Credit(employeeId, 100, "Initial");
        store.Credit(employeeId, 50, "Bonus");

        Assert.Equal(150, store.GetBalance(employeeId));
    }

    [Fact]
    public void Debit_DecreasesBalance_ButNotBelowZero()
    {
        var store = new FakeAccountStore();
        var employeeId = Guid.NewGuid();

        store.Credit(employeeId, 50, "Initial");
        store.Debit(employeeId, 20, "Purchase");
        store.Debit(employeeId, 100, "Too much");

        Assert.Equal(0, store.GetBalance(employeeId));
    }
}
