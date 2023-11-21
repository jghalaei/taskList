using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using User.Application.Features.Users.Queries;

namespace User.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserVm>
    {
        public CreateUserCommand(string name, string userName, string email, string password)
        {
            Name = name;
            UserName = userName;
            Email = email;
            Password = password;
        }

        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}