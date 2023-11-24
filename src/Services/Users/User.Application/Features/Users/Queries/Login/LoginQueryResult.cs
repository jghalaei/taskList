using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Application.Features.Users.Queries.Login
{
    public record LoginQueryResult(Guid UserId, string UserName, string JwtToken);
}