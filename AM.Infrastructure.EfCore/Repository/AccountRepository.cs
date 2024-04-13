using AM.Domain.AccountAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AM.Application.Contracts.Account;
using Microsoft.EntityFrameworkCore;

namespace AM.Infrastructure.EfCore.Repository
{
    public class AccountRepository: RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context):base(context)
        {
            _context = context;
        }


        public EditAccount GetDetails(long id)
        {
            var result = _context.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                FullName=x.FullName,
                Mobile = x.Mobile,
                RoleId = x.RoleId,
                Username = x.Username
            }).FirstOrDefault(x => x.Id == id);


            return result;
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context
                .Accounts
                .Include(x=>x.Role)
                .Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Mobile = x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                Role = x.Role.Name,
                RoleId = x.RoleId,
                Username = x.Username,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
            {
                query = query.Where(x => x.FullName.Contains(searchModel.FullName));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Username))
            {
                query = query.Where(x => x.Username.Contains(searchModel.Username));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            {
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));
            }

            if (searchModel.RoleId>0)
            {
                query = query.Where(x => x.RoleId==searchModel.RoleId);
            }

            var result = query.OrderByDescending(x => x.Id).ToList();
            return result;
        }
    }
}
