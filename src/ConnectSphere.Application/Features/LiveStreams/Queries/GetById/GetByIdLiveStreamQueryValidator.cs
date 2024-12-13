using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.LiveStreams.Queries.GetById;

public sealed class GetByIdLiveStreamQueryValidator : AbstractValidator<GetByIdLiveStreamQuery>
{
    private readonly IApplicationDbContext _context;

    public GetByIdLiveStreamQueryValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(CheckIfLiveStreamExists).WithMessage("Live stream not found.");
    }

    private Task<bool> CheckIfLiveStreamExists(long id, CancellationToken cancellationToken)
    {
        return _context
            .LiveStreams
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}