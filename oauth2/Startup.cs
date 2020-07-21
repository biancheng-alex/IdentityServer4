using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace oauth2
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
            services.AddMvc();
            services.AddIdentityServer()
                 .AddDeveloperSigningCredential()
                 .AddInMemoryIdentityResources(Config.GetIdentityResources())
                 .AddInMemoryApiResources(Config.GetSoluction()).AddTestUsers(IdentityServer4.Quickstart.UI.TestUsers.Users)
                 .AddInMemoryClients(Config.GetClients());


            services.AddControllersWithViews();

            services.AddAuthentication();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseHsts();
            app.UseHttpsRedirection();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //启用IdentityServer
            app.UseIdentityServer();
            //for QuickStart-UI 启用静态文件
           
            //app.UseMvc();
            //app.UseMvcWithDefaultRoute(); //这里带有默认的路由
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
