using BlazorAppC2Corridor.Areas.Identity;
using BlazorAppC2Corridor.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using BlazorAppC2Corridor.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Features;
using MudBlazor.Services;
using ModifyLayoutExample.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContextFactory<ApplicationDbContext>();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsPrincipalFactory>();
builder.Services.AddMudServices();
builder.Services.AddScoped<ViewTailoringService>();



var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "wwwroot", "uploads")),
    RequestPath = "/uploads",
    OnPrepareResponse = ctx =>
    {
        const int cacheDurationInSeconds = 60 * 60 * 24 * 365; // 1 year
        ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age={cacheDurationInSeconds}";
    }
});
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapWhen(
    context => context.Request.Path.StartsWithSegments("/api/Upload"),
    appBuilder => appBuilder.Use(next => context =>
    {
        context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = 10 * 1024 * 1024; // Set the maximum allowed file size to 10 MB
        return next(context);
    }));


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    CreateInitialRolesAndUsersAsync(userManager, roleManager)
    .Wait();
}
app.Run();

async Task CreateInitialRolesAndUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    try
    {
        if (!await roleManager.RoleExistsAsync(Roles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        }
        if (!await roleManager.RoleExistsAsync(Roles.SmallScreen))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SmallScreen));
        }
        if (!await roleManager.RoleExistsAsync(Roles.BigScreen))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.BigScreen));
        }
        var user = new ApplicationUser();
        user.UserName = "admin@bolton.ac.uk";
        user.Email = user.UserName;
        user.Firstname = "Adam";
        user.Lastname = "Test";
        string password = "Pa$$w0rd!";
        if (await userManager.FindByNameAsync(user.UserName) == null)
        {
            var createSuccess = await userManager.CreateAsync(user, password);
            if (createSuccess.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Admin);
                await userManager.SetLockoutEnabledAsync(user, false);
            }
            else
            {
                throw new Exception(createSuccess.Errors.FirstOrDefault().ToString());
            }
        }
    }
    catch (Exception e)
    {
        throw new Exception(e.Message);
    }
}