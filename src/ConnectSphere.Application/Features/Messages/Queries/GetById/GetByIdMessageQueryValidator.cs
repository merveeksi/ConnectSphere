using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Messages.Queries.GetById
{
    public class GetByIdMessageQueryValidator : AbstractValidator<GetByIdMessageQuery>
    {
        private readonly IApplicationDbContext _context;
        
        public GetByIdMessageQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.")
            .MustAsync(MessageExists).WithMessage("Message does not exist.");
        }

        private Task<bool> MessageExists(long id, CancellationToken cancellationToken)
        {
            return _context.Messages.AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}