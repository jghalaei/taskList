using GenericContracts.EventBusMessages;
using MediatR;

namespace Task.Application.Features.Commands.ChangeTaskStatus
{
    public record ChangeTaskStatusCommand(Guid Id, ETodoTaskStatus Status) : IRequest<Guid>;
}