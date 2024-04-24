using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Initializer;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); 

var connection = builder.Configuration["SQLServerConnection:SQLServerConnectionString"];
builder.Services.AddDbContext<SQLServerContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<SQLServerContext>()
    .AddDefaultTokenProviders();

var builders = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
})
    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddAspNetIdentity<ApplicationUser>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builders.AddDeveloperSigningCredential();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//var initializer = app.Services.GetRequiredService<IDbInitializer>();

app.Use(async (context, next) =>
{
    var initializer = context.RequestServices.GetRequiredService<IDbInitializer>();
    initializer.Initializer();
    await next();
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

//initializer.Initializer();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
