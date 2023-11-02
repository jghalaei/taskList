using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace User.Application.Features.Users.Queries.GetUserByUserName
{
    public class GetUserByUserNameQuery : IRequest<UserVm?>
    {
        public string UserName { get; set; }

        public GetUserByUserNameQuery(string userName)
        {
            UserName = userName;
        }


    }
}