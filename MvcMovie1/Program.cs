using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie1.Data;
using MvcMovie1.Models;
using MvcMovie2.Features.Products.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcMovie1Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovie1Context") ?? throw new InvalidOperationException("Connection string 'MvcMovie1Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorOptions(options => options.ViewLocationExpanders.Add(new MvcMovie2.Infrastructure.FeatureViewLocationExpander()));
builder.Services.AddScoped<MvcMovie2.Features.Products.Services.IProductService, MvcMovie2.Features.Products.Services.ProductService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
