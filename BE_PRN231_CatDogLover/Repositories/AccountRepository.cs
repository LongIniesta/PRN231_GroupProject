using BusinessObjects;
using DataAccess;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Account AddAccount(Account Account) => AccountDAO.Instance.AddAccount(Account);

        public IEnumerable<Account> GetAll() => AccountDAO.Instance.GetAll();

        public Account RemoveAccount(int id) => AccountDAO.Instance.RemoveAccount(id);

        public Account UpdateAccount(Account Account) => AccountDAO.Instance.UpdateAccount(Account);
    }
}
