using MediatR;

namespace ConnectSphere.Application.Features.Messages.Queries.GetAll;

public sealed record GetAllMessageQuery : IRequest<List<MessageGetAllDto>>
{
    public long SenderId { get; set; }
    public long? ReceiverId { get; set; }
    public string? Content { get; set; }
    public DateTime? SentAt { get; set; }
    public bool IsRead { get; set; }
}