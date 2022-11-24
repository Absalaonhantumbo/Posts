using Aplication.Interfaces;
using Infrastruture.Services;

namespace API.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserNamesList, UserList>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserAccessor, UserAccessor>();
        return services;
    }
}