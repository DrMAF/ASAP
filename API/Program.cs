using DAL;
using ASAP;
using API.Endpoints;
using Microsoft.EntityFrameworkCore;
using Messaging;
using StockMarket.Bootstrapper;
using API.HostedServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

string defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(defaultConnection));

builder.Services.AddSwaggerGen();

builder.Services
    .ConfigureAuthorization(builder.Configuration).AddBusinessServices()
    .AddPolygonProviderServices(builder.Configuration)
    .AddMessagingServices(builder.Configuration);

builder.Services.AddHostedService<PolygonNewsUpdateService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}


app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGroup("api/users").MapAccount();
app.MapGroup("api/polygon").MapPolygon();

app.Run();
