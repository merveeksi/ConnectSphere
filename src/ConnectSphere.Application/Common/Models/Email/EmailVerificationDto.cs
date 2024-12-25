namespace ConnectSphere.Application.Common.Models.Email;

public class EmailVerificationDto
{
    public EmailVerificationDto(string email, string token)
    {
        Email = email;
        Token = token;
    }

    public EmailVerificationDto()
    {
        
    }

    public string Email { get; set; }
    public string Token { get; set; }
}