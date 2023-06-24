using Books;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddRazorPages();
var services = builder.Services;
services.AddDbContext<BooksDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<BooksDbContext>();
services.AddControllersWithViews();
services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions
                .ReferenceHandler = ReferenceHandler.Preserve);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});


app.MapRazorPages();
app.MapControllers();

app.Run();
