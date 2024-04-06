using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Contracts.Comment
{
    public class AddComment
    {
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Message { get;  set; }
        public long ProductId { get;  set; }
    }
}
