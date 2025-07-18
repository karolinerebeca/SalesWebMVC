using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Services;
using SalesWebMVC.Data;
using SalesWebMVC.Services;
using System.Configuration;
using System.Globalization;



var builder = WebApplication.CreateBuilder(args);

// Configuração do contexto de banco de dados
builder.Services.AddDbContext<SalesWebMVCContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("SalesWebMVCContext"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SalesWebMVCContext"))
    ));


builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SalesRecordService>();

var app = builder.Build();

// 🌍 Configuração de cultura pt-BR
var ptBR = new CultureInfo("pt-BR");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ptBR),
    SupportedCultures = [ptBR],
    SupportedUICultures =  [ptBR]
};

app.UseRequestLocalization(localizationOptions);

// Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    using var scope = app.Services.CreateScope();
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<SalesWebMVCContext>();
        var seedingService = new SeedingService(context);
        seedingService.Seed();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sellers}/{action=Index}/{id?}");

app.Run();
