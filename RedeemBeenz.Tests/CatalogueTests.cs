using System.Linq;
using NUnit.Framework;

namespace RedeemBeenz.Tests;

public class CatalogueTests
{
    [Test]
    public void ListAll_ReturnsItems()
    {
        var cat = new Catalogue();
        var list = cat.ListAll();
        Assert.That(list, Is.Not.Empty);
    }

    [Test]
    public void FindById_ReturnsCorrectReward()
    {
        var cat = new Catalogue();
        var first = cat.ListAll().First();

        var result = cat.FindById(first.RewardId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.RewardId, Is.EqualTo(first.RewardId));
    }
}