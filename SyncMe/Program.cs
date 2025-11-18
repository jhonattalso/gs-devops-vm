using Microsoft.EntityFrameworkCore;
using SyncMe.Data; // Importante

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 min de sessão
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

// CONFIGURAÇÃO DO ORACLE
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString));
builder.Services.AddScoped<SyncMe.Services.ContentService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapGet("/", () => Results.Redirect("/academy"));

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contents}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
