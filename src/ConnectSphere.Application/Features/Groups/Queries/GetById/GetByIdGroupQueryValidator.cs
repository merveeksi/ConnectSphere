using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Groups.Queries.GetById;

public sealed class GetByIdGroupQueryValidator : AbstractValidator<GetByIdGroupQuery>
{
    private readonly IApplicationDbContext _context;
    
    public GetByIdGroupQueryValidator(IApplicationDbContext context)
    {
        _context = context;
        
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            // Asenkron için MustAsync kullanılır.
            .MustAsync(CheckIfGroupExists).WithMessage("Group not found.");
    }
    
    private Task<bool> CheckIfGroupExists(long id, CancellationToken cancellationToken)
    {
        return _context
            .Groups
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}