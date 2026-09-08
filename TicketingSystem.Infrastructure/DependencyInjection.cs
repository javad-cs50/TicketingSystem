using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Infrastructure.Authentication.Jwt;
using TicketingSystem.Infrastructure.Identity;
using TicketingSystem.Infrastructure.Persistence;
using TicketingSystem.Infrastructure.Persistence.Repositories;
using TicketingSystem.Infrastructure.Services;

namespace TicketingSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        //Database
        services.AddDbContext<ApplicationDbContext>(option => option.UseNpgsql(configuration.GetConnectionString("Default")));

        //Identity
        services.AddIdentityCore<ApplicationUser>
            (option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireDigit = true;
                option.Password.RequireUppercase = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
            }).AddRoles<IdentityRole<Guid>>().AddEntityFrameworkStores<ApplicationDbContext>();

        // Repositories
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();

        // Identity abstraction
        services.AddScoped<IIdentityService, IdentityService>();
        //UserService
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        //jwt
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        return services;
    }
}
