using Microsoft.EntityFrameworkCore;
using SaleStore.Data;
using SaleStore.DataAccess.Repository;
using SaleStore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SaleStore.Utility.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ALL services must be added BEFORE the build

builder.Services.AddControllersWithViews();

// we add EF Core and configure which class have the implementation of DbContext 
// via a helper method we get the connection string
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// When we add Identity to project, this line also adds identity tables
// Implement IEmailSender or shit brakes because we no longer use AddDefaultIdentity. We added custom implementation
// AddDefaultTokenProviders is included in AddDefaultIdentity but not in AddIdentity. We need it for Emailconfirmation tokens -> var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
builder.Services.AddIdentity<IdentityUser, IdentityRole>(/*options => options.SignIn.RequireConfirmedAccount = true check if user's email is valid*/).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Should be added after identity. No identity, no authorization
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
}); // to properly route to related pages in case of an unauthorized access attemt

builder.Services.AddRazorPages(); // To show Identity pages. Identity Pages are Razor pages

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Index");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Check if user credentials is valid
app.UseAuthorization(); // If user is valid, authorize the user. Can//Can't access some pages

app.MapRazorPages(); // To show Identity pages. Identity Pages are Razor pages
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
