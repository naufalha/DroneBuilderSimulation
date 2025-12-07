using DroneBuildSimulation.Data;
using DroneBuildSimulation.Mappings;
using DroneBuildSimulation.Repositories.Implementations;
using DroneBuildSimulation.Repositories.Interfaces;
using DroneBuildSimulation.Services.Implementations;
using DroneBuildSimulation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Services to the container.
builder.Services.AddControllersWithViews();

// 2. Database Context (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. AutoMapper
builder.Services.AddAutoMapper(typeof(ProductProfile));

// 4. Dependency Injection (Repository & Service)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();