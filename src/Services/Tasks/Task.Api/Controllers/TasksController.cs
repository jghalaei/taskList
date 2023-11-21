using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Features.Commands.ChangeTaskStatus;
using Task.Application.Features.Commands.CreateTask;
using Task.Application.Features.Commands.DeleteTaskCommand;
using Task.Application.Features.Commands.EditTaskCommand;
using Task.Application.Features.Queries.GetAllUserTasks;
using Task.Application.Features.Queries.GetTaskQuery;
using Task.Application.ViewModels;
using Task.Core.Entities;

namespace Task.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TasksController : ControllerBase
    {
        readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{Id}", Name = "GetTask")]
        public async Task<IActionResult> GetTask(Guid Id)
        {
            var request = new GetTaskQuery(Id);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks([FromQuery] GetAllTasksQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Location");
            return CreatedAtRoute("GetTask", new { Id = result.Id },result);
        }
        [HttpPut]
        public async Task<IActionResult> ChangeTaskStatus([FromBody] ChangeTaskStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> EditTask([FromBody] EditTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return AcceptedAtRoute("GetTask", new { Id = result });
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTask(Guid Id)
        {
            var request = new DeleteTaskCommand(Id);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

    }
}