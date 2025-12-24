namespace RedeemBeenz.Interfaces;

public interface IAccountStore
{
    int GetBalance(Guid employeeId);
    void Credit(Guid employeeId, int amount, string note);
    void Debit(Guid employeeId, int amount, string note);
}
