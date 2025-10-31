using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Otus.HA.AppServices.Users.Handlers;
using Otus.HA.AppServices.Users.Repositories;

namespace Otus.HA.AppServices.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services) =>
        services.AddUsersAppServices();
    
    private static IServiceCollection AddUsersAppServices(this IServiceCollection services) =>
        services
            .AddScoped<IUsersHandler, UsersHandler>()
            .AddScoped<IUsersRepository, UsersRepository>();
}