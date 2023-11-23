using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GenericContracts.Contracts;

public interface IUserAccessor
{
    ClaimsPrincipal User { get; }
    Guid UserId { get; }
}