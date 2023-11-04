using System.Linq.Expressions;
using GenericContracts.Contracts;
using MediatR;
using Task.Application.Features.Queries.GetAllUserTasks;
using Task.Core.Entities;

namespace Task.Application.Features.Queries.GetOpenTasks;

public class GetOpenTasksQueryHandler : IRequestHandler<GetOpenTasksQuery, IEnumerable<TodoTask>>
{
    private readonly IRepository<TodoTask> _repository;

    public GetOpenTasksQueryHandler(IRepository<TodoTask> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TodoTask>> Handle(GetOpenTasksQuery request, CancellationToken cancellationToken)
    {
        var acceptableStates = new List<ETodoTaskStatus>() { ETodoTaskStatus.Created, ETodoTaskStatus.InProgress };
        Expression<Func<TodoTask, bool>> expression = t => t.UserId == request.UserId
            && acceptableStates.Contains(t.Status)
            && (string.IsNullOrEmpty(request.SearchText) || t.Title.Contains(request.SearchText) || t.Comment.Contains(request.SearchText)
            && (request.DueDateMin == null || t.DueDate >= request.DueDateMin)
            && (request.DueDateMax == null || t.DueDate <= request.DueDateMax));

        var result = await _repository.GetAllAsync(expression);
        return result;
    }
}
