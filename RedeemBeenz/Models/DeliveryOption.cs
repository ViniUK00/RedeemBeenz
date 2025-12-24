namespace RedeemBeenz.Models;

public class DeliveryOption
{
    public DeliveryMethod Method { get; set; }
    public string Address { get; set; } = "";

    public bool IsValid()
    {
        return Method == DeliveryMethod.EVoucher
               || !string.IsNullOrWhiteSpace(Address);
    }

}
