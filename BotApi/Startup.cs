using Bot.Api.ServiceModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bot.Api
{
    public class Startup
    {
        private IHostingEnvironment _env;
        public IConfigurationRoot Configuration { get; }
        private const string CORS_POLICY_NAME = "AllowSpecificOrigin";

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsProduction() || _env.IsStaging())
            {
                // Implement a real Services
            }
            else
            {
                // Implement a fake Services
                //services.AddScoped<IFakeService, DebugFakeService>();
            }
            services.AddLogging();
            services.AddSingleton(Configuration);

            services.Configure<BotCredentials>(Configuration.GetSection("BotCredentials"));

            // We need to cache tokens, so enable MemoryCache.
            services.AddMemoryCache();

            // Add framework services.
            services.AddMvcCore()
                        .AddJsonFormatters()
                        //.AddCors(options =>
                        //{
                        //    options.AddPolicy(CORS_POLICY_NAME, builder =>
                        //    {
                        //        builder
                        //            .WithOrigins("http://localhost:4000")
                        //            //.AllowAnyOrigin()
                        //            .AllowAnyHeader()
                        //            .AllowAnyMethod()
                        //            //.AllowCredentials()
                        //            .Build();
                        //    });
                        //})
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {   // ORDER MATTERS IN HERE!
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(Configuration.GetSection("Logging").GetValue<LogLevel>("LogLevel"));

            ///* Enabling swagger file */
            //app.UseSwagger();

            ///* Enabling Swagger ui, consider doing it on Development env only */
            //app.UseSwaggerUi();

            //app.UseCors(CORS_POLICY_NAME);
            app.UseMvc(
                //config =>
                //{
                //    config.MapRoute(
                //        name: "Default",
                //        template: "api/{controller}/{action}/{id?}"//,
                //        //defaults: new { controller = "Home", action = "Index" }
                //    );
                //}
            );
        }
    }
}
