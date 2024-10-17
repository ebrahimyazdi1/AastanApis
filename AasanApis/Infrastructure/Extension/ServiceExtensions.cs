﻿using AastanApis.Data.Repositories;
using AasanApis.Models;
using AastanApis.Services;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace AasanApis.Infrastructure.Extension
{
    public static class ServiceExtensions
    {

        public static void ConfigureLogging(this IServiceCollection service, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, hostEnvironment.EnvironmentName))
                .Enrich.WithProperty("Environment", hostEnvironment.EnvironmentName)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            service.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environmentName)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-{environmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }

        public static IServiceCollection AddAastanServices(this IServiceCollection services,
             IConfiguration configuration)
        {
            services.Configure<AastanOptions>(configuration.GetSection(AastanOptions.SectionName));
            //services.AddHttpClient<IAastanClient, AastanClient>((sp, client) =>
            //{
            //    try
            //    {


            //        var options = sp.GetRequiredService<IOptions<AastanOptions>>().Value;
            //        client.BaseAddress = new Uri(options.BaseAddress, UriKind.RelativeOrAbsolute);
            //        var authenticationParam =
            //            Convert.ToBase64String(
            //                Encoding.ASCII.GetBytes($"{options.UserName}:{options.Password}"));
            //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authenticationParam);
            //    }
            //    catch (Exception e)
            //    {

            //        throw;
            //    }

            //});
            //services.AddScoped<IAastanClient, AastanClient>();
            services.AddScoped<IAastanService, AastanService>();
            services.AddScoped<IAastanRepository, AastanRepository>();
            services.AddScoped<BaseLog>();
            return services;
        }

        //public static void StartBackgroundTasks(IServiceCollection services, IConfiguration configuration)
        //{
        //    var JobOptions = configuration.GetSection(TokenBackgroundJobOption.SectionName).Get<TokenBackgroundJobOption>();
        //    RecurringJob.AddOrUpdate<ISportInsuranceRepository>(x => x.GetSportToken(), JobOptions.CronExpression);
        //}
    }
}
