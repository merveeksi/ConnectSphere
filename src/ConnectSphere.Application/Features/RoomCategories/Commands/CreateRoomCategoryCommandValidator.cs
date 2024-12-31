using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;

namespace ConnectSphere.Application.Features.RoomCategories.Commands
{
    public class CreateRoomCategoryCommandValidator : AbstractValidator<CreateRoomCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateRoomCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");
        }
    }
} 