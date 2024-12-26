using ConnectSphere.Application.Common.Models.Identity;

namespace ConnectSphere.Application.Features.Auth.Commands.Login;

public sealed class AuthLoginDto
{

    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiresAt { get; set; }

    public AuthLoginDto()
    {

    }

    public AuthLoginDto(string token, DateTime expiresAt, string refreshToken, DateTime refreshTokenExpiresAt)
    {
        Token = token;
        ExpiresAt = expiresAt;
        RefreshToken = refreshToken;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;
    }

    public static AuthLoginDto FromIdentityLoginResponse(IdentityLoginResponse response)
    {
        // return new AuthLoginDto(response.Token, response.ExpiresAt, response.RefreshToken,
        //     response.RefreshTokenExpiresAt);
        return new AuthLoginDto
        {
            Token = response.Token,
            ExpiresAt = response.ExpiresAt,
        };
    }
}
