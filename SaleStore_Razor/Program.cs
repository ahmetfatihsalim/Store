using Microsoft.EntityFrameworkCore;
using SaleStore_Razor.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(); // Unlike MVC project, we dont have controllers, actions or views so rather than AddControllersWithViews we use AddRazorPages

// we add EF Core and configure which class have the implementation of DbContext 
// via a helper method we get the connection string
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


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

app.MapRazorPages(); // Routing is staright forward. We dont have controller name in our routes for there is no controller in razor project. The rout will be whatever is inside the Pages folder

app.Run();
