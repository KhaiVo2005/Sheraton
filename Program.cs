using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;
using Sheraton.Helpers;
using Sheraton.Models.ViewModel;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SheratonContextConnection") ?? throw new InvalidOperationException("Connection string 'SheratonContextConnection' not found.");;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddScoped<PdfRenderHelper>();

builder.Services.AddDbContext<SheratonDbContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:SheratonConnection"]);
});

builder.Services.Configure<VnpayOptions>(
    builder.Configuration.GetSection("Vnpay")
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}
app.Run();
