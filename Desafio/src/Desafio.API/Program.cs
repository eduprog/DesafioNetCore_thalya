using Desafio.API;
using Desafio.Infrastructure;
using Desafio.Identity;
using Desafio.Application;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddApplicationConfigurations(builder.Configuration);
    builder.Services.AddIdentityConfigurations(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApiConfigurations();
}

var app = builder.Build();
{
    app.UseExceptionMiddleware();
    app.AddBuilderConfiguration();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    
    app.Run();
}
