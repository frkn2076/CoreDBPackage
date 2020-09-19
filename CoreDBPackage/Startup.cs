using CoreDBPackage.Notification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;

namespace CoreDBPackage {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            //Added for session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(2);
                options.Cookie.IsEssential = true;
            });
            //

            //Added for Get IP of Client
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //
#if LOCAL
            services.AddDbContextPool<AppDBContext>(options => options.UseMySQL(Configuration.GetConnectionString("LocalConnection")));
#else
            services.AddDbContextPool<AppDBContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
#endif
            services.AddSingleton<IMailSender, MailSender>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            //Added for middleware Exception Handler
            app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Added for session
            app.UseSession();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
