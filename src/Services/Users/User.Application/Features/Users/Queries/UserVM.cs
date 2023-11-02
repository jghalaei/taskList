using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Core.Entities;

namespace User.Application.Features.Users.Queries
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public UserVm(Guid Id, string userName, string email)
        {
            this.Id = Id;
            UserName = userName;
            Email = email;
        }
        public static UserVm MapToUserVm(AppUser user)
        {
            return new UserVm(user.Id, user.UserName, user.Email);
        }
    }
}
