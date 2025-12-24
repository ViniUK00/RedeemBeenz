using System;
using System.Collections.Generic;
using RedeemBeenz.Interfaces;
using RedeemBeenz.Models;

public class Catalogue : ICatalogue
{
    private List<Reward> rewards = new()
    {
        new Reward { RewardId = Guid.NewGuid(), Name = "Book", PointsCost = 100, InStock = 10 },
        new Reward { RewardId = Guid.NewGuid(), Name = "DVD", PointsCost = 150, InStock = 5 }
    };

    public List<Reward> ListAll() => rewards;
    public Reward FindById(Guid id) => rewards.Find(r => r.RewardId == id);
}