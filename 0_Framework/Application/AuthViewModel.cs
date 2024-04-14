namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }

        public AuthViewModel(long id, long roleId, string username, string fullname, string mobile)
        {
            Id = id;
            RoleId = roleId;
            Username = username;
            Fullname = fullname;
            Mobile = mobile;
        }
    }
}