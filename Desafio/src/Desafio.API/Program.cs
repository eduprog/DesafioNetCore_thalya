using Desafio.API;
using Desafio.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    // Usa Serviços Registrados
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseAuthentication();

    app.UseDbMigrationHelper();

    app.Run();

}
