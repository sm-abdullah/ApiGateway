using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RateLimit;
using RateLimit.Middleware;
using RateLimit.Models;

namespace ApiGateway
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
            // configure ip rate limiting middleware
            services.Configure<RateLimitSettings>(Configuration.GetSection("RateLimitSettings"));
            services.Configure<RateLimitPolicies>(Configuration.GetSection("RateLimitPolicies"));
            services.AddSingleton<IRateLimitSettingManager, RateLimitSettingManager>();
            services.AddSingleton<IRulesManager, RulesManager>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddSingleton<IRateLimitDataStore<RequestCounter>, RateLimitDataStore<RequestCounter>>();
            services.AddSingleton<IRateLimit, FixedWindow>(); // register dependency with RateLimit Implementation

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiGateway", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiGateway v1"));
            }

            app.UseRateLimitMiddleware();  // use rate limit middleware
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
