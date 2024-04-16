using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string SystemUser = "2";
        public const string SystemAdmin = "1";
        public const string SystemAnalyst = "3";
        public const string SystemTechManager = "4";
        public const string ContentUploader = "5";

        public static string GetRoleBy(long id)
        {
            switch (id)
            {
                case 2:
                    return "مدیر سیستم";
                case 3:
                    return "آنالیست سیستم";
                case 4:
                    return "مدیر فنی ";
                case 5:
                    return "مدیر محتوا";
                default:
                    return "";
            }
        }
    }
}
