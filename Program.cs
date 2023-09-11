using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SymphonicSeats2.Models;
using Microsoft.Identity.Client; /////////


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SymphonicSeats2IdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SymphonicSeats2IdentityDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add SignalR to application
builder.Services.AddSignalR();
builder.Services.AddTransient<SymphonicSeats2.Models.CollectionItemRepository>();
// registers and makes aviable all interactions for the database on disk
builder.Services.AddDbContext<SymphonicSeats2.Models.CollectionContext>(
    options => options.UseSqlite("Data Source=SymphonicSeats2.db")
);

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    /* .AddRoles<IdentityRole>()       */                 // new addition
    .AddEntityFrameworkStores<CollectionContext>();

// Used for OpenApi usage
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Adds authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Adds Swagger UI to build mode
// Can mve it outside of the code block to be available in development
app.UseSwagger();
app.UseSwaggerUI();

app.Use(async (context, next) =>
{
    // Get the exception
    var error = context.Features.Get<IExceptionHandlerFeature>();
    if (error != null)
    {
        // Go get the logger
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        // And log the error to the screen
        logger.LogError(error.Error, error.Error.Message);
    }
    await next();
}
);



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
// Map voitng hub to the address "~/voting"
app.MapHub<SymphonicSeats2.VotingHub>("/voting");

// new addition for role additions
/* using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService < RoleManager<IdentityRole>();

    var roles = new[] { "Admin", "Member" };

    foreach (var role in roles)
    {

    }
} */

app.Run();
