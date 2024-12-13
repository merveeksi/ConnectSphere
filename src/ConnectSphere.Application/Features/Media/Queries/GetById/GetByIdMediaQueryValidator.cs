using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Media.Queries.GetById
{
    public sealed class GetByIdMediaQueryValidator : AbstractValidator<GetByIdMediaQuery>
    {
        private readonly IApplicationDbContext _context;

        public GetByIdMediaQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(CheckIfMediaExists).WithMessage("Media not found.");
        }

        private Task<bool> CheckIfMediaExists(long id, CancellationToken cancellationToken)
        {
            return _context.Media.AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}