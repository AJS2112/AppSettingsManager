using AppSettingsManager;
using AppSettingsManager.Models;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

//var twilioSettings = new TwilioSettigs();
//new ConfigureFromConfigurationOptions<TwilioSettigs>(builder.Configuration.GetSection("Twilio")).Configure(twilioSettings);
//builder.Services.AddSingleton(twilioSettings);

// Add services to the container.
builder.Services.AddConfiguration<TwilioSettigs>(builder.Configuration, "Twilio");

//builder.Services.AddConfiguration<TwilioSettigs>(builder.Configuration, "Twilio"); In hierarchical data better use IConfiguration
builder.Services.Configure<SocialLoginSettings>(builder.Configuration.GetSection("SocialLoginSettings"));

builder.Services.Configure<TwilioSettigs>(builder.Configuration.GetSection("Twilio"));
builder.Services.AddControllersWithViews();

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
