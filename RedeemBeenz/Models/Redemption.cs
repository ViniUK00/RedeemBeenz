namespace RedeemBeenz.Models;

public class Redemption
{
    public Guid RedemptionId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid RewardId { get; set; }
    public int Quantity { get; set; }
    public RedemptionStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string RewardName { get; set; } = "";
    public int TotalCost { get; set; }
}