using System.Linq.Expressions;
using AutoMapper;
using GenericContracts.Contracts;
using MediatR;
using Task.Application.Accessors;
using Task.Application.ViewModels;
using Task.Core.Entities;

namespace Task.Application.Features.Queries.GetAllUserTasks
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TodoTask> _repository;
        private readonly IUserAccessor _accessor;
        public GetAllTasksQueryHandler(IRepository<TodoTask> repository, IUserAccessor accessor, IMapper mapper)
        {
            _repository = repository;
            _accessor = accessor;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskViewModel>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<TodoTask, bool>> expression = t => (t.UserId == _accessor.UserId) &&
                (request.AcceptableStates == null || request.AcceptableStates.Contains(t.Status)) &&
                (string.IsNullOrEmpty(request.SearchText) || t.Title.Contains(request.SearchText) || t.Comment.Contains(request.SearchText) &&
                (request.DueDateMin == null || t.DueDate >= request.DueDateMin) &&
                (request.DueDateMax == null || t.DueDate <= request.DueDateMax));

            var result = await _repository.GetAllAsync(expression);
            return _mapper.Map<IEnumerable<TaskViewModel>>(result);
        }
    }
}
