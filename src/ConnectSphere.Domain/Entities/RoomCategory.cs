using ConnectSphere.Domain.Common.Entities;

namespace ConnectSphere.Domain.Entities;

public sealed class RoomCategory : EntityBase<long>
{
        public RoomCategory(string name, string description, List<ChatRoom> chatRooms)
        {
                Name = name;
                Description = description;
                ChatRooms = chatRooms;
        }

        public RoomCategory()
        {
                
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<ChatRoom> ChatRooms { get; private set; }
}