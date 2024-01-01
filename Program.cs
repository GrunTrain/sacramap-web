using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using sacremap.Data;
using sacremap.Models;
using sacremap.Services.ChurchService;
using sacremap.Services.VisitationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
         options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        })
    );
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<IChurchService, ChurchService>();
builder.Services.AddScoped<IVisitationService, VisitationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.MapIdentityApi<User>().RequireCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireCors("AllowAllOrigins");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Mod" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    string email = "admin@example.com";
    string password = "SuperHardAdminPasswordJustForTests123!";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new User { Email = email, UserName = email };
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
    DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await DataSeeder.SeedData(context);
    scope.Dispose();

}


app.Run();
