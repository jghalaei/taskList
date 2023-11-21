using MediatR;
using Task.Application.ViewModels;

namespace Task.Application.Features.Commands.CreateTask
{
    public record CreateTaskCommand(string Title, DateTime DueDate = default, string Comment = "") : IRequest<TaskViewModel>
    {
    }
}