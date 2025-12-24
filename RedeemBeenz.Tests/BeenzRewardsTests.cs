using System;
using System.Linq;
using NUnit.Framework;
using RedeemBeenz.Component;
using RedeemBeenz.Models;

namespace RedeemBeenz.Tests;

public class BeenzRewardsTests
{
    [Test]
    public void CheckAvailability_True_WhenEnoughStock()
    {
        var accounts = new AccountStore();
        var cat = new Catalogue();
        var red = new RedemptionStore();
        var api = new BeenzRewards(accounts, cat, red);

        var reward = cat.ListAll().First();

        Assert.That(api.CheckAvailability(reward.RewardId, 1), Is.True);
    }

    // ---------------------------
    // OCL PRE: qty >= 1
    // ---------------------------
    [Test]
    public void Redeem_Throws_WhenQtyLessThanOne()
    {
        var accounts = new AccountStore();
        var cat = new Catalogue();
        var red = new RedemptionStore();
        var api = new BeenzRewards(accounts, cat, red);

        var employeeId = Guid.NewGuid();
        var reward = cat.ListAll().First();

        accounts.Credit(employeeId, 1000, "Seed");
        var delivery = new DeliveryOption { Method = DeliveryMethod.EVoucher };

        var ex = Assert.Throws<InvalidOperationException>(() =>
            api.Redeem(employeeId, reward.RewardId, 0, delivery));

        Assert.That(ex!.Message, Does.Contain("OCL PRE"));
    }

    // ---------------------------
    // OCL PRE: delivery must be valid
    // ---------------------------
    [Test]
    public void Redeem_Throws_WhenDeliveryInvalid()
    {
        var accounts = new AccountStore();
        var cat = new Catalogue();
        var red = new RedemptionStore();
        var api = new BeenzRewards(accounts, cat, red);

        var employeeId = Guid.NewGuid();
        var reward = cat.ListAll().First();

        accounts.Credit(employeeId, 1000, "Seed");

        var invalidDelivery = new DeliveryOption
        {
            Method = DeliveryMethod.Postal,
            Address = "" 
        };

        var ex = Assert.Throws<InvalidOperationException>(() =>
            api.Redeem(employeeId, reward.RewardId, 1, invalidDelivery));

        Assert.That(ex!.Message, Does.Contain("delivery must be valid"));
    }

    // ---------------------------
    // OCL PRE: enough balance
    // ---------------------------
    [Test]
    public void Redeem_Throws_WhenInsufficientBalance()
    {
        var accounts = new AccountStore();
        var cat = new Catalogue();
        var red = new RedemptionStore();
        var api = new BeenzRewards(accounts, cat, red);

        var employeeId = Guid.NewGuid();
        var reward = cat.ListAll().First();

        // no credit => 0 balance
        var delivery = new DeliveryOption { Method = DeliveryMethod.EVoucher };

        var ex = Assert.Throws<InvalidOperationException>(() =>
            api.Redeem(employeeId, reward.RewardId, 1, delivery));

        Assert.That(ex!.Message, Does.Contain("enough beenz").Or.Contain("balance"));
    }

    // ---------------------------
    // OCL PRE: enough stock
    // ---------------------------
    [Test]
    public void Redeem_Throws_WhenInsufficientStock()
    {
        var accounts = new AccountStore();
        var cat = new Catalogue();
        var red = new RedemptionStore();
        var api = new BeenzRewards(accounts, cat, red);

        var employeeId = Guid.NewGuid();
        var reward = cat.ListAll().First();

        accounts.Credit(employeeId, 5000, "Seed");
        var delivery = new DeliveryOption { Method = DeliveryMethod.EVoucher };

        int tooMuchQty = reward.InStock + 1;

        var ex = Assert.Throws<InvalidOperationException>(() =>
            api.Redeem(employeeId, reward.RewardId, tooMuchQty, delivery));

        Assert.That(ex!.Message, Does.Contain("stock"));
    }

    // ---------------------------
    // SUCCESS: check OCL POST
    // ---------------------------
    [Test]
    public void Redeem_Succeeds_And_UpdatesBalanceAndStockCorrectly()
    {
        var accounts = new AccountStore();
        var cat = new Catalogue();
        var red = new RedemptionStore();
        var api = new BeenzRewards(accounts, cat, red);

        var employeeId = Guid.NewGuid();
        var reward = cat.ListAll().First();

        int qty = 2;
        int cost = reward.PointsCost * qty;

        accounts.Credit(employeeId, 1000, "Seed");

        int balancePre = accounts.GetBalance(employeeId);
        int stockPre = reward.InStock;

        var delivery = new DeliveryOption { Method = DeliveryMethod.EVoucher };

        var redemption = api.Redeem(employeeId, reward.RewardId, qty, delivery);

        Assert.That(redemption.Status, Is.EqualTo(RedemptionStatus.Confirmed));
        Assert.That(accounts.GetBalance(employeeId), Is.EqualTo(balancePre - cost));
        Assert.That(reward.InStock, Is.EqualTo(stockPre - qty));
    }
}
