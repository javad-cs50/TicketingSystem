
using Microsoft.AspNetCore.Identity;
using TicketingSystem.Api.Extensions;
using TicketingSystem.Application;
using TicketingSystem.Infrastructure;
using TicketingSystem.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers
    (configure =>configure.ReturnHttpNotAcceptable=true)
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddApiVersioningConfiguration();
builder.Services.AddOpenApi();
// Add Clean Architecture Layers
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    await IdentitySeeder.SeedAsync(roleManger);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
