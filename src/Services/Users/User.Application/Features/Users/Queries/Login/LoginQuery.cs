using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace User.Application.Features.Users.Queries.CheckPassword
{
    public class LoginQuery : IRequest<string>
    {
        public string Username { get; set; }
        public string GivenPassword { get; set; }

        public
         LoginQuery(string username, string givenPassword)
        {

            this.Username = username;
            GivenPassword = givenPassword;
        }
    }
}