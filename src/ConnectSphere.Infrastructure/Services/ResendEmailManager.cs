using System.Web;
using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Application.Common.Models.Email;
using Resend;

namespace ConnectSphere.Infrastructure.Services;

public sealed class ResendEmailManager : IEmailService
{
    private readonly IResend _resend;
    private static string? _emailTemplate;

    public ResendEmailManager(IResend resend, IEnvironmentService environmentService)
    {
        _resend = resend;
        if (string.IsNullOrEmpty(_emailTemplate))
            _emailTemplate = File.ReadAllText(Path.Combine(environmentService.WebRootPath, "email-templates",
                "email-verification-template.html"));
    }

    public Task EmailVerificationAsync(EmailVerificationDto emailVerificationDto, CancellationToken cancellationToken)
    {
        string html = _emailTemplate;
        var emailTitle = "E-Posta Doğrulama İşlemi - ConnectSphere";
        html = html.Replace("{{title}}", emailTitle);
        html = html.Replace("{{greetings}}", $"Merhaba {emailVerificationDto.Email}");
        html = html.Replace("{{message}}", "E-Posta doğrulama işleminizi tamamlamak için aşağıdaki linke tıklayınız.");
        html = html.Replace("{{verifyButtonText}}", "E-Posta Doğrula");
        var token = HttpUtility.UrlEncode(emailVerificationDto.Token);
        var emailVerificationUrl =
            $"http://localhost:7404/auth/verify-email?email={emailVerificationDto.Email}&token={token}";
        html = html.Replace("{{verifyButtonLink}}", emailVerificationUrl);
        var message = new EmailMessage();
        message.From = "merveeksii61@gmail.com";
        message.To.Add(emailVerificationDto.Email);
        message.Subject = emailTitle;
        message.HtmlBody = html;
        return _resend.EmailSendAsync(message);
    }
}