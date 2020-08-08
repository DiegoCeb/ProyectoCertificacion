using App.ContextoPrimusDb;
using Cache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PrimusCompras
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
            services.AddControllersWithViews();

            //Contexto DB
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("PrimusDB")));

            //services.AddIdentity<IdentityUser, IdentityRole>();

            services.AddMemoryCache();

            services.AddTransient<ICache, ImplementacionCache>();

            services.Configure<ConfigKeys>(Configuration.GetSection("ConfigKeys"));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["LlaveRedis"];
            });

            //services.AddScoped<IProductosRepository, MockProductosRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions opt = new DeveloperExceptionPageOptions();
                opt.SourceCodeLineCount = 4;
                app.UseDeveloperExceptionPage(opt);
            }
            else if (env.IsProduction() || env.IsStaging())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDefaultFiles();
            //app.UseAuthentication();
            app.UseRouting();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}
