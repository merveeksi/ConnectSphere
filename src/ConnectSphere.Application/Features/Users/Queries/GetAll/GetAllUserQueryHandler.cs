using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Users.Queries.GetAll;

public sealed class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserGetAllDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllUserQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<UserGetAllDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        return _context
            .Users
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => UserGetAllDto.Create(x))
            .ToListAsync(cancellationToken);
    }
}