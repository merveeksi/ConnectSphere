using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.Medias.Commands.Create;

public sealed class CreateMediaCommandHandler : IRequestHandler<CreateMediaCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreateMediaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateMediaCommand request, CancellationToken cancellationToken)
    {
        var media = Media.Create(request.UploadedById, request.Url, request.MediaType);
        _context.Medias.Add(media);
        await _context.SaveChangesAsync(cancellationToken);
        return media.Id;
    }
} 