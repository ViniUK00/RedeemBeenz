using System;
using RedeemBeenz;
using NUnit.Framework;
using RedeemBeenz.Component;
using RedeemBeenz.Models;

namespace RedeemBeenz.Tests;
public class DeliveryOptionTests
{
    [Test]
    public void EVoucher_IsAlwaysValid()
    {
        var opt = new DeliveryOption
        {
            Method = DeliveryMethod.EVoucher
        };

        Assert.That(opt.IsValid(), Is.True);
    }

    [Test]
    public void Postal_RequiresAddress()
    {
        var opt = new DeliveryOption
        {
            Method = DeliveryMethod.Postal,
            Address = string.Empty
        };

        Assert.That(opt.IsValid(), Is.False);
    }

    [Test]
    public void Postal_ValidWhenAddressProvided()
    {
        var opt = new DeliveryOption
        {
            Method = DeliveryMethod.Postal,
            Address = "10 Downing St"
        };

        Assert.That(opt.IsValid(), Is.True);
    }
}