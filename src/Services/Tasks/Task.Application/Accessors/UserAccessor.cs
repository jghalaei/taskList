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
    private readonly IHttpContextAccessor _accessor;

    public UserAccessor(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    public ClaimsPrincipal User => _accessor.HttpContext.User;

    public Guid UserId => Guid.Parse(User.Identity?.Name ?? String.Empty);
}