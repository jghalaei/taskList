using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using User.Application.Features.Users.Queries;

namespace User.Application.Features.Users.Commands.EditUser
{
    public class EditUserCommand : IRequest<UserVm>
    {
        public EditUserCommand(Guid id, string userName, string name, string email)
        {
            this.Name = name;
            this.Email = email;
            UserName = userName;
        }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}