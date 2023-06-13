using DreamsRentBack;
using DreamsRentBack.DAL;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.Hubs;
using DreamsRentBack.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddTransient<ChatService>();
builder.Services.AddDbContext<DreamsRentDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = "453551993493-mivjrpv95ej7oebfpa2vgj1o71a0bhq2.apps.googleusercontent.com";
    options.ClientSecret = "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";
});
builder.Services.AddAuthentication()
            .AddFacebook(options =>
            {
                options.AppId = "642690207734546";
                options.AppSecret = "597c1d3a07127015d4f5fea3c8b80cd6";
});
builder.Services.AddScoped<LayoutService>();
builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredUniqueChars = 3;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;

    opt.User.RequireUniqueEmail = true;
    opt.User.AllowedUserNameCharacters = "WERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";

    opt.Lockout.MaxFailedAccessAttempts = 5;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
}).AddDefaultTokenProviders().AddEntityFrameworkStores<DreamsRentDbContext>();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Account}/{action=Signin}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    https://localhost:7260/chatHub
    endpoints.MapHub<ChatHub>("/chatHub");

});

app.Run();
