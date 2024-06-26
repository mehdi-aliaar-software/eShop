﻿using AM.Application.Contracts.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace AM.Domain.RoleAgg
{
    public interface IRoleRepository:IRepository<long,Role>
    {
        List<RoleViewModel> Search();
        EditRole GetDetails(long id);
    }
}
