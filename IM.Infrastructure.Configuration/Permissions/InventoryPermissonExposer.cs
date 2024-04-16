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
                        new PermissionDto(50,"ListInventory"),
                        new PermissionDto(51,"SearchInventory"),
                        new PermissionDto(52,"CreateInventory"),
                        new PermissionDto(53,"EditInventory"),

                    }

                }
            };

            return result;
        }
    }
}
