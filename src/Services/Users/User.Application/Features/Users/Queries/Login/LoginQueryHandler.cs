using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using GenericContracts.Contracts;
using MediatR;
using User.Application.Features.Users.Queries.Login;
using User.Application.PasswordHash;
using User.Application.Services;
using User.Core.Entities;

namespace User.Application.Features.Users.Queries.CheckPassword
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResult>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly JwtService _jwtService;
        public LoginQueryHandler(IRepository<AppUser> repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<LoginQueryResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            AppUser? user = await _repository.GetOneAsync(u => u.UserName == request.Username);
            if (user == null)
                throw new InvalidDataException("User not found");
            bool isLoggedIn = PasswordHasher.VerifyPassword(request.GivenPassword, user.Salt, user.Password);
            if (!isLoggedIn)
                throw new UnauthorizedAccessException("Wrong UserName or password");
            var jwtToken=_jwtService.GenerateToken(user.Id, user.UserName);
            return new LoginQueryResult(user.Id, user.UserName, jwtToken);
        }
    }
}