using Desafio.API;
using Desafio.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddVersioning();
    builder.Services.AddSwagger();
    builder.Services.AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    /*app.UseCors(builder => builder
        .SetIsOriginAllowed(orign => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());*/
    app.MapControllers();
    app.UseDbMigrationHelper();

    app.Run();
}
