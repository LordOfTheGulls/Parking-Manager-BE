using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PM_AUTH;
using PM_AUTH.AuthorizationRequirements;
using PM_DAL;
using PM_DAL.Entity;

var builder = WebApplication.CreateBuilder(args);

var CustomCORS = "Clients_CORS_Policy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CustomCORS,
        builder =>
        {
            builder.WithOrigins("http://localhost:7156")
            .WithMethods("GET", "POST")
            .AllowAnyHeader();

            builder
            .WithOrigins("http://localhost:4200")
            .WithMethods("GET", "POST", "PUT");
        });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

/*builder.Services.TryAddScoped<SignInManager<IdentityUser>>();*/

builder.Services.AddDbContext<PMDBContext>(config =>
{
    //config.UseInMemoryDatabase("Memory");
    config.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"), x => x.MigrationsAssembly("PM-DAL"));
});

builder.Services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

/*builder.Services.AddIdentity<IdentityUser, IdentityRole>();*/
/*builder.Services.AddDbContext<PMDBContext>(config =>
{
    // for in memory database  
    config.UseInMemoryDatabase("MemoryDb");
});*/

builder.Services.AddIdentity<User, Role>()
.AddEntityFrameworkStores<PMDBContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "IdentityServer.Cookie";
    config.LoginPath = "/Auth/Login";
});

builder.Services.AddIdentityServer(o =>
{
    o.UserInteraction.LoginUrl = "https://localhost:7288/Auth/Login";
})
.AddAspNetIdentity<User>()
.AddInMemoryIdentityResources(Config.GetResources())
.AddInMemoryApiScopes(Config.GetApis())
.AddInMemoryClients(Config.GetClients())
.AddDeveloperSigningCredential()
.AddOperationalStore(options =>
{
    options.ConfigureDbContext = o => o.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"), x => x.MigrationsAssembly("PM-DAL"));
});
//.AddTestUsers(Config.GetUsers());
//On Production ->.AddSigningCredentials(); 

builder.Services.AddControllersWithViews();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions { 
    MinimumSameSitePolicy = SameSiteMode.None 
});

app.UseRouting();

app.UseCors(CustomCORS);

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();