using ConnectSphere.Application.Common.Models.Email;

namespace ConnectSphere.Application.Common.Interfaces;

public interface IEmailService
{
    Task EmailVerificationAsync(EmailVerificationDto emailVerificationDto, CancellationToken cancellationToken);
}