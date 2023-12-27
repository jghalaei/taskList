using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using Microsoft.AspNetCore.Http;

namespace Task.Application.Accessors;


public class UserAccessor : IUserAccessor
{


    public UserAccessor(IHttpContextAccessor accessor)
    {

        User = accessor.HttpContext?.User ?? throw new InvalidDataException("User not found");
    }
    public ClaimsPrincipal User { get; private set; }

    public Guid UserId => Guid.Parse(User.Identity?.Name ?? String.Empty);
}