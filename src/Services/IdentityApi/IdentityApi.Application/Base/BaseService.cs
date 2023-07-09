using Shared.Logs.Services;

namespace IdentityApi.Application.Base;

public abstract class BaseService
{
    protected readonly ILogServices _logServices;

    protected BaseService(ILogServices logServices)
    {
        _logServices = logServices;
    }
}