using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Task.Application.ViewModels;

namespace Task.Application.Features.Queries.GetTaskQuery;

public record GetTaskQuery(Guid Id) : IRequest<TaskViewModel>
{

}