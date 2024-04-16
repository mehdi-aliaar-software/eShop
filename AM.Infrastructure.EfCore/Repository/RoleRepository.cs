using AM.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AM.Application.Contracts.Role;
using Microsoft.EntityFrameworkCore;

namespace AM.Infrastructure.EfCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly AccountContext _context;
        public RoleRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public List<RoleViewModel> Search()
        {
            var result = _context.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();
            return result;
        }

        public EditRole GetDetails(long id)
        {
            var result = _context.Roles.Select(x => new EditRole
            {
                Id = x.Id,
                Name = x.Name,
                MappedPermissions =  MapPermissions(x.Permissions)
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);

            result.Permissions = result.MappedPermissions.Select(x =>  x.Code).ToList();
            return result;
        }

        private static List<PermissionDto> MapPermissions(IEnumerable<Permission> permissions)
        {
            var result = permissions
                .Select(x => new PermissionDto(x.Code, x.Name))
                .ToList();
            return result;
        }
    }
}
