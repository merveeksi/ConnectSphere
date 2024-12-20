using ConnectSphere.Application.Common.Attributes;
using MediatR;

namespace ConnectSphere.Application.Features.Messages.Queries.GetAll;

[CacheOptions(absoluteExpirationMinutes: 60, slidingExpirationMinutes: 15)]
public sealed record GetAllMessageQuery : IRequest<List<MessageGetAllDto>>
{
    [CacheKeyPart]
    public long SenderId { get; set; }

    [CacheKeyPart]
    public long? ReceiverId { get; set; }

    [CacheKeyPart]
    public string? Content { get; set; }

    [CacheKeyPart]
    public DateTime? SentAt { get; set; }

    [CacheKeyPart]
    public bool IsRead { get; set; }

    public string CacheGroup => "Messages";
    public GetAllMessageQuery(long senderId, long? receiverId, string? content, DateTime? sentAt, bool isRead)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content;
        SentAt = sentAt;
        IsRead = isRead;
    }
}