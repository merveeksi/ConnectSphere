using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Notifications.Queries.GetById
{
    public class GetByIdNotificationQueryValidator : AbstractValidator<GetByIdNotificationQuery>
    {
        private readonly IApplicationDbContext _context;

        public GetByIdNotificationQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Notification ID is required.")
                .MustAsync(NotificationExists).WithMessage("Notification does not exist.");
        }

        private Task<bool> NotificationExists(long id, CancellationToken cancellationToken)
        {
            return _context.Notifications.AnyAsync(x => x.Id == id, cancellationToken);
        }

    }
}