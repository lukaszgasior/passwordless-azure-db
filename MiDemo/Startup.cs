namespace MiDemo
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MiDemo.Data;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            var managedIdentityInterceptor = new MiConnectionInterceptor(appSettings.ManagedIdentityTenantId);
            services.AddDbContext<MiDbContext>(o =>
                o.UseSqlServer(appSettings.SqlConnectionString).AddInterceptors(managedIdentityInterceptor));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
