using AastanApis.Data;
using AasanApis.Infrastructure.Extension;
using AasanApis.Models;
using AastanApis.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using Hangfire;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureLogging(builder.Configuration, builder.Environment);
builder.Services.AddDbContext<AastanDbContext>(opt => opt.UseOracle
    (builder.Configuration["ConnectionStrings:AastanConnection"]));

builder.Services.AddHttpClient<IAastanClient, AastanClient>((sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<AastanOptions>>().Value;
    var authenticationParam =
      Convert.ToBase64String(
          Encoding.ASCII.GetBytes($"{options.AstanUserName}:{options.AstanPassword}"));
    client.BaseAddress = new Uri(options.TokenAddress, UriKind.RelativeOrAbsolute);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authenticationParam);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddAastanServices(builder.Configuration);
builder.Services.AddScoped<AastanClient>();
builder.Services.AddScoped<HangFireJobService>();
// Add Hangfire with in-memory storage
builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"وب سرویس های شاهکار آستان"));
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
app.UseAuthorization();
app.MapControllers();
app.UseHangfireDashboard();

// Configure Hangfire recurring job
var tokenJobService = app.Services.CreateScope().ServiceProvider.GetRequiredService<HangFireJobService>();
RecurringJob.AddOrUpdate("check-and-refresh-psgb-token",
    () => tokenJobService.CheckAndRefreshPSGBTokenAsync(),
    "30 * * * * *");

RecurringJob.AddOrUpdate("check-and-refresh-shahkar-token",
    () => tokenJobService.CheckAndRefreshShakarTokenAsync(),
    "30 * * * * *");

app.Run();
