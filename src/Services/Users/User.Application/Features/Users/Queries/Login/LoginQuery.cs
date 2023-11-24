using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using User.Application.Features.Users.Queries.Login;

namespace User.Application.Features.Users.Queries.CheckPassword
{
    public class LoginQuery : IRequest<LoginQueryResult>
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