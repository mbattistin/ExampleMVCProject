using ExampleMVCProject.Data;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add service to the database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("DefaultComection")));

//toast service
builder.Services.AddRazorPages().AddNToastNotifyToastr(new ToastrOptions
{
    CloseButton = true
});

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
app.UseNToastNotify();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
