namespace RedeemBeenz.Models;

public class Reward
{
    public Guid RewardId { get; set; }
    public string Name { get; set; }
    public int PointsCost { get; set; }
    public int InStock { get; set; }
}
