using _0_Framework.Application;
using AM.Application.Contracts.Role;
using AM.Domain.RoleAgg;

namespace AM.Application
{
    public class RoleApplication: IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation=new OperationResult();
            if (_roleRepository.Exists(x=>x.Name==command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var role = new Role(command.Name);
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var role = _roleRepository.GetBy(command.Id);
            if (role == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_roleRepository.Exists(x=>x.Name==command.Name && x.Id!=command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            role.Edit(command.Name);
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<RoleViewModel> Search()
        {
            var result = _roleRepository.Search();

            return result;
        }

        public EditRole GetDetails(long id)
        {
            var result = _roleRepository.GetDetails(id);
            return result;
        }
    }
}
