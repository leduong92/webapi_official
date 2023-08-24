using API.Extensions;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
// app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseSwaggerDocumentation();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// using var scope = app.Services.CreateScope();
// var services = scope.ServiceProvider;
// var context = services.GetRequiredService<TestdbContext>();
// var logger = services.GetRequiredService<ILogger<Program>>();
// try
// {
//     // await context.Database.MigrateAsync();
// }
// catch (Exception ex)
// {
//     logger.LogError(ex, "An error occured during migration");
// }

app.Run();
