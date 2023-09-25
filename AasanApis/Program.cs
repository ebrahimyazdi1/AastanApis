using AasanApis.Data;
using AasanApis.Infrastructure.Extension;
using AasanApis.Models;
using AasanApis.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;

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
          Encoding.ASCII.GetBytes($"{options.UserName}:{options.Password}"));
    client.BaseAddress = new Uri(options.TokenAddress, UriKind.RelativeOrAbsolute);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authenticationParam);

});
//builder.Services.AddHttpClient<IAastanClient, AastanClient>();




builder.Services.AddHttpContextAccessor();
builder.Services.AddAastanServices(builder.Configuration);
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

app.Run();
