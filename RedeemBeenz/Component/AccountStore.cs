using RedeemBeenz.Interfaces;

namespace RedeemBeenz.Component;

using System;
using System.Collections.Generic;

public class AccountStore : IAccountStore
{
    private Dictionary<Guid, int> _balances = new();

    public int GetBalance(Guid employeeId)
        => _balances.ContainsKey(employeeId) ? _balances[employeeId] : 0;

    public void Credit(Guid employeeId, int amount, string note)
    {
        if (!_balances.ContainsKey(employeeId))
            _balances[employeeId] = 0;
        _balances[employeeId] += amount;
    }

    public void Debit(Guid employeeId, int amount, string note)
    {
        _balances[employeeId] -= amount;
        if (_balances[employeeId] < 0)
            _balances[employeeId] = 0;
    }
}
