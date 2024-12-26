using ConnectSphere.Application.Features.Auth.Commands.Login;
using ConnectSphere.Application.Features.Auth.Commands.Register;
using ConnectSphere.Application.Features.Auth.Commands.ResedEmail.VerificationEmail;
using ConnectSphere.Application.Features.Auth.Commands.VerifyEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConnectSphere.WebApi.Controllers;

public class AuthController : ApiControllerBase
{
    public AuthController(ISender mediator) : base(mediator)
    {
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthLoginCommand command, CancellationToken cancellationToken)
        => Ok(await Mediatr.Send(command, cancellationToken));

    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthRegisterCommand command, CancellationToken cancellationToken)
        => Ok(await Mediatr.Send(command, cancellationToken));
    
    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail(AuthVerifyEmailCommand command, CancellationToken cancellationToken)
        => Ok(await Mediatr.Send(command, cancellationToken));
    [HttpPost("resend-email-verification")]
    public async Task<IActionResult> ResendEmailVerification(AuthReSendEmailVerificationEmailCommand command, CancellationToken cancellationToken)
        => Ok(await Mediatr.Send(command, cancellationToken));
}
