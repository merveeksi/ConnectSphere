namespace ConnectSphere.Application.Common.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class CacheKeyPartAttribute : Attribute
{
    public bool Encode { get; set; } = true;
    public string Prefix { get; set; } = string.Empty;
}