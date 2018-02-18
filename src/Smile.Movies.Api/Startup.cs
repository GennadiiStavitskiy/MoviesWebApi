using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Smile.Movies.Backend;
using Smile.Movies.Backend.Interfaces;
using Smile.Movies.Backend.SortingStrategies;
using Smile.Movies.Shared;
using Swashbuckle.AspNetCore.Swagger;

namespace Smile.Movies.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });

            services.AddMvc();

            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("localhost:53451"));
            //});

            services.AddMemoryCache();

            services.AddTransient<IDataService, DataService>();
            services.AddTransient<ISearchingService, SearchingService>();
            services.AddTransient<ISortingService, SortingService>();
            services.AddTransient<ISortingStrategyFactory, SortingStrategyFactory>();
            services.AddTransient<IDataSourceAdapter, DataSourceAdapter>();
            services.AddTransient<ICachingService, CachingService>();

            services.Configure<MemoryCacheSettings>(Configuration.GetSection("MemoryCacheSettings"));

            services.Configure<CorsOptionsSettings>(Configuration.GetSection("CorsOptionsSettings"));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<CorsOptionsSettings> corsSettings)
        {
            var origins = corsSettings.Value.CorsOptionOrigin;

            //app.UseCors(options => options.WithOrigins(origins).AllowAnyMethod());
            app.UseCors("AllowAllHeaders");

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smile Movies API V1"); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
        }
    }
}