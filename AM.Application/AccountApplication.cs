using _0_Framework.Application;
using AM.Application.Contracts.Account;
using AM.Domain.AccountAgg;
using AM.Infrastructure.EfCore.Repository;

namespace AM.Application
{
    public class AccountApplication: IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, 
            IFileUploader fileUploader)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateAccount command)
        {
            OperationResult operation = new OperationResult();
            if (_accountRepository.Exists(x=>x.Username==command.Username || 
                                             x.Mobile==command.Mobile))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var password = _passwordHasher.Hash(command.Password);
            string path = $"profilePictures";
            string profilePath = _fileUploader.Upload(command.ProfilePhoto, path);

            Account account = new Account(command.FullName, command.Username, password, command.RoleId, 
                command.Mobile, profilePath);
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            OperationResult operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Id);
            if (account==null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_accountRepository.Exists(x => (x.Username == command.Username ||
                                               x.Mobile == command.Mobile) && x.Id!=command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            string path = $"profilePictures";
            string profilePath = _fileUploader.Upload(command.ProfilePhoto, path);

            account.Edit(command.FullName,command.Username, command.RoleId,command.Mobile,
                profilePath);

            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            OperationResult operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Id);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (command.Password != command.RePassword)
            {
                return operation.Failed(ApplicationMessages.MisMatchPassword);
            }

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditAccount GetDetails(long id)
        {
            var result=_accountRepository.GetDetails(id);
            return result;
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var result = _accountRepository.Search(searchModel);
            return result;
        }
    }
}
