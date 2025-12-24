using RedeemBeenz.Models;

namespace RedeemBeenz.Interfaces;

public interface ICatalogue
{
    List<Reward> ListAll();
    Reward FindById(Guid id);
}