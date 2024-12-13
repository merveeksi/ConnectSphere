using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Users.Queries.GetById
{
    public class GetByIdUserQueryValidator : AbstractValidator<GetByIdUserQuery>
    {
        private readonly IApplicationDbContext _context;

        public GetByIdUserQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User ID is required.")
                .MustAsync(CheckIfUserExists).WithMessage("User does not exist.");
        }

        private Task<bool> CheckIfUserExists(long id, CancellationToken cancellationToken)
        {
            return _context.Users.AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}