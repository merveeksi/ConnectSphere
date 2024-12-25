using ConnectSphere.Application.Common.Interfaces;

namespace ConnectSphere.WebApi.Services;

public sealed class EnvironmentManager : IEnvironmentService
{
    public string WebRootPath { get; }

    public EnvironmentManager(string webRootPath)
    {
        WebRootPath = webRootPath;
    }
}