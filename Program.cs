using IdentityAuthenticationandAuthorization.Context;
using IdentityAuthenticationandAuthorization.Interfaces;
using IdentityAuthenticationandAuthorization.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IdentityDatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityDatabaseContext>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapIdentityApi<IdentityUser>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
