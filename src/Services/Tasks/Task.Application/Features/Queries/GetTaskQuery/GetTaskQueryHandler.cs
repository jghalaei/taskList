using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using GenericContracts.Contracts;
using MediatR;
using Task.Application.Accessors;
using Task.Application.ViewModels;
using Task.Core.Entities;

namespace Task.Application.Features.Queries.GetTaskQuery
{
    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskViewModel>
    {
        private readonly IRepository<TodoTask> _repository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public GetTaskQueryHandler(IRepository<TodoTask> repository, IMapper mapper, IUserAccessor userAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<TaskViewModel> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.Id);
            ArgumentNullException.ThrowIfNull(todo);
            if (todo.UserId != _userAccessor.UserId)
                throw new InvalidDataException("You are not allowed to get this task");
            TaskViewModel vm = _mapper.Map<TaskViewModel>(todo);
            return vm;
        }
    }
}