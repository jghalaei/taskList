using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Application.Features.Users.Commands.CreateUser;
using User.Application.Features.Users.Queries.CheckPassword;
using User.Application.Features.Users.Queries.GetUserByUserName;
using User.Core.Entities;

namespace Users.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<AppUser> _logger;
        public UserController(IMediator mediator, ILogger<AppUser> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("singup")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{userName}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(string userName)
        {
            var query = new GetUserByUserNameQuery(userName);
            var user = await _mediator.Send(query);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

    }
}