namespace ConnectSphere.Application.Common.Models.Jwt;

public sealed class JwtGenerateTokenRequest
{
    public JwtGenerateTokenRequest(long id, string email, IList<string> roles)
    {
        Id = id;
        Email = email;
        Roles = roles;
    }

    public long Id { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
}