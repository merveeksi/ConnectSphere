using System.Web;
using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Application.Common.Models.Identity;
using ConnectSphere.Application.Common.Models.Jwt;
using ConnectSphere.Domain.Identity;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Infrastructure.Services;

public sealed class IdentityManager : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IApplicationDbContext _context;

    public IdentityManager(UserManager<ApplicationUser> userManager, IJwtService jwtService, IApplicationDbContext context)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _context = context;
    }

    // This method is used to authenticate a user
    public async Task<bool> AuthenticateAsync(IdentityAuthenticateRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return false;
        return await _userManager.CheckPasswordAsync(user, request.Password);
    }

    // public Task<IdentityRefreshTokenResponse> RefreshTokenAsync(IdentityRefreshTokenRequest request, CancellationToken cancellationToken)
    // {
    //     throw new NotImplementedException();
    // }

    // E-posta adresinin veritabanında olup olmadığını kontrol eder.
    public Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        return _userManager
            .Users
            .AnyAsync(x => x.Email == email, cancellationToken);
    }
    
    public Task<bool> CheckIfEmailVerifiedAsync(string email, CancellationToken cancellationToken)
    {
        return _userManager
            .Users
            .AnyAsync(x => x.Email == email && x.EmailConfirmed, cancellationToken);
    }

    public Task<bool> CheckSecurityStampAsync(long userId, string securityStamp, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityCreateEmailTokenResponse> CreateEmailTokenAsync(IdentityCreateEmailTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return new IdentityCreateEmailTokenResponse(token);
    }


    public async Task<IdentityLoginResponse> LoginAsync(IdentityLoginRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var roles = await _userManager.GetRolesAsync(user);
        var jwtRequest = new JwtGenerateTokenRequest(user.Id, user.Email, roles);
        var jwtResponse = _jwtService.GenerateToken(jwtRequest);
        return new IdentityLoginResponse(jwtResponse.Token, jwtResponse.ExpiresAt);
    }
    
    public async Task<IdentityVerifyEmailResponse> VerifyEmailAsync(IdentityVerifyEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var decodedToken = HttpUtility.UrlDecode(request.Token);
        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
        if (!result.Succeeded)
            CreateAndThrowValidationException(result.Errors);
        return new IdentityVerifyEmailResponse(user.Email);
    }
    
    // Yeni bir kullanıcı kaydeder.
    public async Task<IdentityRegisterResponse> RegisterAsync(IdentityRegisterRequest request, CancellationToken cancellationToken)
    {
        // Yeni bir kullanıcı kimliği oluştur.
        long userId = BitConverter
            .ToInt64(Ulid
                .NewUlid()
                .ToByteArray()
                , 0);
        
        // Yeni bir kullanıcı nesnesi oluştur.
        var user = new ApplicationUser
        {
            Id = userId,
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedByUserId = userId.ToString(),
            CreatedOn = DateTimeOffset.UtcNow,
            EmailConfirmed = false,
        };
        // Kullanıcıyı veritabanına kaydet.
        var result = await _userManager.CreateAsync(user, request.Password);
        // Kayıt işlemi başarısız olursa hata fırlat.
        if (!result.Succeeded) CreateAndThrowValidationException(result.Errors);
        // E-posta onaylama jetonu oluştur.
        var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        // Kayıt yanıtını döndür.
        return new IdentityRegisterResponse(userId, user.Email, emailToken);
    }
    
    // Doğrulama hatası oluşturur ve fırlatır.
    private void CreateAndThrowValidationException(IEnumerable<IdentityError> errors)
    {
        // Hata mesajlarını ve özelliklerini içeren yeni bir doğrulama hatası oluştur.
        var errorMessages = errors
            .Select(x => new ValidationFailure(x.Code, x.Description))
            .ToArray();
        // Doğrulama hatasını fırlat.
        throw new ValidationException(errorMessages);
    }
}