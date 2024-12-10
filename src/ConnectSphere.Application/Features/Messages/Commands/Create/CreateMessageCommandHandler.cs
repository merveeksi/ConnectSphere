using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.Messages.Commands.Create;

public sealed class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreateMessageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = Message.Create(request.SenderId, request.ReceiverId, request.Content);
        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        return message.Id;
    }
} 