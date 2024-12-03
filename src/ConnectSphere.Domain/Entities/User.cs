using System.Text.RegularExpressions;
using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.Enums;

namespace ConnectSphere.Domain.Entities
{
    public sealed class User : EntityBase<long>
    {
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string ProfilePictureUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }

    // Navigations
    public ICollection<Message> Messages { get; set; }
    public ICollection<Group> Groups { get; set; }
    }
    
}