using BCP;
using BCP.Infrastructure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbServer"));
builder.Services
    .AddContainerService(builder.Configuration)
    .AddAuthenticationService(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<AppDbContext>();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    RequestPath = "/temp"
});
app.UseCors(builder =>
{
    builder.SetIsOriginAllowed(_ => true);
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
    builder.AllowCredentials();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
