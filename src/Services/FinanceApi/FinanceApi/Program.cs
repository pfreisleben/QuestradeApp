using FinanceApi.Extensions;
using FinanceApi.Middlewares;
using Serilog;
using Shared.Logs.Configurations;

var builder = WebApplication.CreateBuilder(args);
try
{
    Log.Logger = LogExtensions.ConfigureStructuralLogWithSerilog(builder.Configuration);
    builder.Logging.AddSerilog(Log.Logger);

    Log.Information("Iniciando a aplicação");
    
    builder.Services.AddServices(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    var app = builder.Build();
    
    await app.InitializeDatabase();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Finance Api"); });
    }
    

    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    app.UseMiddleware<SerilogRequestLoggerMiddleware>();
    app.UseMiddleware<UnauthorizedTokenMiddleware>();
    app.UseStaticFiles();

    app.UseCors(x => x.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal("Erro fatal na aplicação => {ExMessage}", ex.Message);
}
finally
{
    await Log.CloseAndFlushAsync();
}