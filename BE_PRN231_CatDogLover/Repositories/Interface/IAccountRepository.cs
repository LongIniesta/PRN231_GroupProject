using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using DTOs.Account;

namespace Repositories.Interface
{
    public interface IAccountRepository
    {
        Account AddAccount(Account account);
        Task<Account> RemoveAccount(int id);
        Task<Account> GetAccountById(int id);
        Task<Account> UpdateAccount(Account account);
        IEnumerable<Account> GetAll();
        IQueryable<Account> Search(AccountSearchRequest searchRequest);
        Task BanAccountAsync(int id, string reason);
        Task UnbanAccountAsync(int id);
        Task ResetPasswordAsync(int id, string currentPassword, string newPassword);
        Task ForgetPasswordAsync(int id);

    }
}
