using _0_Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.Infrastructure.Configuration.Permissions
{
    public class InventoryPermissonExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            var result = new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new PermissionDto(InventeryPermissions.ListInventory,"ListInventory"),
                        new PermissionDto(InventeryPermissions.SearchInventory,"SearchInventory"),
                        new PermissionDto(InventeryPermissions.CreateInventory,"CreateInventory"),
                        new PermissionDto(InventeryPermissions.EditInventory,"EditInventory"),
                        new PermissionDto(InventeryPermissions.Increase,"Increase"),
                        new PermissionDto(InventeryPermissions.Reduce,"Reduce"),
                        new PermissionDto(InventeryPermissions.OperationLog,"OperationLog")
                      }
                }
            };

            return result;
        }
    }
}
