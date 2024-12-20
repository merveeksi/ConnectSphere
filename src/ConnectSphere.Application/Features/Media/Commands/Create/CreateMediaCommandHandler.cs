using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.Media.Commands.Create;

public sealed class CreateMediaCommandHandler : IRequestHandler<CreateMediaCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreateMediaCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<long> Handle(CreateMediaCommand request, CancellationToken cancellationToken)
    {
        var media = ConnectSphere.Domain.Entities.Media.Create(request.UploadedById, request.Url, request.MediaType);
        _context.Media.Add(media);
        await _context.SaveChangesAsync(cancellationToken);
        await _cacheInvalidator.InvalidateGroupAsync("Media", cancellationToken);
        return media.Id;
    }
}