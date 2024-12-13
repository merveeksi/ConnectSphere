using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.ValueObjects;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateGroupCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.GroupName)
            .NotEmpty()
            .Must(BeValidGroupName)
            .WithMessage(x => $"Invalid group name: {x.GroupName}")
            .MustAsync((command, groupName, cancellationToken) => CheckIfTheGroupExists(command, cancellationToken))
            .WithMessage("The specified group name already exists.");

        RuleFor(x => x.CreatedById)
            .NotEmpty()
            .WithMessage("Creator ID is required.")
            .MustAsync(UserExists)
            .WithMessage("Creator user does not exist.");

        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("CreatedAt is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedAt cannot be in the future.");
    }
    
    private async Task<bool> CheckIfTheGroupExists(CreateGroupCommand command, CancellationToken cancellationToken)
    {
        return !await _context
            .Groups
            .AnyAsync(x => x.GroupName.Value.ToLower() == command.GroupName.Value.ToLower() && x.CreatedById == command.CreatedById, cancellationToken);
    }

    private bool BeValidGroupName(GroupName groupName)
    {
        if (groupName == null) return false;
        
        try
        {
            // GroupName zaten bir value object olduğu için
            // sadece null kontrolü yeterli olacaktır
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    private async Task<bool> UserExists(long userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AnyAsync(x => x.Id == userId, cancellationToken);
    }
}
