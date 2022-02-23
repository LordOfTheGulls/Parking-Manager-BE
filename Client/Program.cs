var builder = WebApplication.CreateBuilder(args);

builder.Services
.AddAuthentication()
.AddJwtBearer("Bearer", config =>
{
    config.Authority = "https://localhost:7288/";
    config.Audience = "Client";
});

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
