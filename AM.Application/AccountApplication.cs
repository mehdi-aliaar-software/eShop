using _0_Framework.Application;
using AM.Application.Contracts.Account;
using AM.Domain.AccountAgg;
using AM.Infrastructure.EfCore.Repository;

namespace AM.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthHelper _authHelper;



        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher,
            IFileUploader fileUploader, IAuthHelper authHelper)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
        }

        public OperationResult Create(RegisterAccount command)
        {
            OperationResult operation = new OperationResult();
            if (_accountRepository.Exists(x => x.Username == command.Username ||
                                             x.Mobile == command.Mobile))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var password = _passwordHasher.Hash(command.Password);
            string path = $"profilePhotos";
            string profilePath = _fileUploader.Upload(command.ProfilePhoto, path);

            Account account = new Account(command.FullName, command.Username, password, command.RoleId,
                command.Mobile, profilePath);
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        //the same as Create:
        public OperationResult Register(RegisterAccount command)
        {
            OperationResult operation = new OperationResult();
            if (_accountRepository.Exists(x => x.Username == command.Username ||
                                               x.Mobile == command.Mobile))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var password = _passwordHasher.Hash(command.Password);
            string path = $"profilePhotos";
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
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_accountRepository.Exists(x => (x.Username == command.Username ||
                                               x.Mobile == command.Mobile) && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            string path = $"profilePhotos";
            string profilePath = _fileUploader.Upload(command.ProfilePhoto, path);

            account.Edit(command.FullName, command.Username, command.RoleId, command.Mobile,
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

        public OperationResult login(Login command)
        {
            OperationResult operation = new OperationResult();
            var account = _accountRepository.GetByUsername(command.Username);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.passwordOrUserMisMatch);
            }

            (bool Verified, bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);
            if (result.Verified == false)
            {
                return operation.Failed(ApplicationMessages.MisMatchPassword);
            }

            var authViewModel = new AuthViewModel
                (account.Id, account.RoleId, account.Username, account.FullName, account.Mobile);

            _authHelper.Signin(authViewModel);
            return operation.Succeeded();
        }

        public EditAccount GetDetails(long id)
        {
            var result = _accountRepository.GetDetails(id);
            return result;
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var result = _accountRepository.Search(searchModel);
            return result;
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }
    }
}
