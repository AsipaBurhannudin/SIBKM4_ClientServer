using API.Repositories.Interface;
using Client.Repositories;
using Client.Repositories.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<UniversityRepository>();
builder.Services.AddScoped<EmployeeRepository>(); //kalau belum dipasang pasti akan (unable to resolve service)
builder.Services.AddScoped<AccountRepository>(); //kalau belum dipasang pasti akan (unable to resolve service)

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();