using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IAccountRepository
    {
        Account AddAccount(Account Account);
        Account RemoveAccount(int id);
        Account UpdateAccount(Account Account);
        IEnumerable<Account> GetAll();
    }
}
