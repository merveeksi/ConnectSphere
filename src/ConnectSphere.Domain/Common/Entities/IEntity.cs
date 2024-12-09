namespace ConnectSphere.Domain.Common.Entities;

public interface IEntity<TKey> where TKey : struct
{
    TKey Id { get; set; } //guid, int, long
}