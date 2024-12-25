using ConnectSphere.Application.Common.Models.Jwt;

namespace ConnectSphere.Application.Common.Interfaces;

public interface IJwtService
{ 
    JwtGenerateTokenResponse GenerateToken(JwtGenerateTokenRequest request);
}