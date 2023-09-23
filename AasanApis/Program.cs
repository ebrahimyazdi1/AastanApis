using AasanApis.Data;
using AasanApis.Infrastructure.Extension;
using AasanApis.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureLogging(builder.Configuration, builder.Environment);
builder.Services.AddDbContext<AastanDbContext>(opt => opt.UseOracle
    (builder.Configuration["ConnectionStrings:AastanConnection"]));
builder.Services.AddHttpClient<IAastanClient, AastanClient>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAastanServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
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
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"وب سرویس های آستان"));
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
app.UseAuthorization();
app.MapControllers();

app.Run();
