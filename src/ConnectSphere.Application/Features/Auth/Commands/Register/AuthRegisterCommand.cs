using ConnectSphere.Application.Common.Models.Identity;
using MediatR;

namespace ConnectSphere.Application.Features.Auth.Commands.Register;

public sealed class AuthRegisterCommand : IRequest<ResponseDto<AuthRegisterDto>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Role { get; set; }
    
    public IdentityRegisterRequest ToIdentityRegisterRequest()
    {
        return new IdentityRegisterRequest(Email, Password, FirstName, LastName, Role);
    }
}