using CoreDBPackage.Hubs;
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
                options.IdleTimeout = TimeSpan.FromMinutes(10);
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

            //SignalR
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);
            });
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder => {
                builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("https://localhost:5001").AllowCredentials();
            }));

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

            //For Policy
            app.UseCors("CorsPolicy");

            //Added for session
            app.UseSession();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("eventHub"); // path will look like this https://localhost:44379/chatsocket 
            });
        }
    }
}
