using System;
using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.Enum;
using ConnectSphere.Domain.Identity;

namespace ConnectSphere.Domain.Entities
{
    public sealed class Story : EntityBase<long>
    {
        public Story(string content, string mediaUrl, StoryType type, DateTime expirationTime, long userId, ApplicationUser user)
        {
            Content = content;
            MediaUrl = mediaUrl;
            Type = type;
            ExpirationTime = expirationTime;
            UserId = userId;
            User = user;
        }

        public Story()
        {
            
        }

        public string Content { get; set; }
        public string MediaUrl { get; set; }
        public StoryType Type { get; set; }
        public DateTime ExpirationTime { get; set; }
        public long UserId { get; set; }
        public ApplicationUser User { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpirationTime; // Hikaye süresi dolmuş mu?

        public static Story Create(string content, string mediaUrl, StoryType type, DateTime expirationTime, long userId, ApplicationUser user)
        {
            return new Story(content, mediaUrl, type, expirationTime, userId, user);
        }
    }
   
} 