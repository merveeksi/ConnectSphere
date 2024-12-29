using ConnectSphere.Domain.Common.Entities;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class Event : EntityBase<long>
{
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    
    // İlişkiler
    public ICollection<Post> Posts { get; set; }

    public Event()
    {
        Posts = new List<Post>();
    }

    public Event(long id, string eventName, DateTime eventDate, string location, string description)
    {
        Id = id;
        EventName = eventName;
        EventDate = eventDate;
        Location = location;
        Description = description;
    }

    public static Event Create(string eventName, string description, string location, DateTime eventDate)
    {
        if (string.IsNullOrEmpty(eventName)) 
            throw new ArgumentNullException(nameof(eventName));
        if (string.IsNullOrEmpty(location)) 
            throw new ArgumentNullException(nameof(location));

        var eventEntity = new Event
        {
            Id = TsidCreator.GetTsid().ToLong(),
            EventName = eventName,
            EventDate = eventDate,
            Location = location,
            Description = description
        };

        return eventEntity;
    }
} 