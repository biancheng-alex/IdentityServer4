using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace Oauth2Server
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

            services.AddMvcCore().AddAuthorization().AddNewtonsoftJson();
            //      services.AddAuthentication("Bearer")
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = "http://localhost:5000";
            //    options.RequireHttpsMetadata = false;

            //    options.ApiName = "clientsecret";
            //});
            services.AddAuthentication(Configuration["Identity:Scheme"])
                        .AddIdentityServerAuthentication(options =>
                        {
                            options.RequireHttpsMetadata = false; //是否需要https
                            options.Authority = $"http://{Configuration["Identity:IP"]}:{Configuration["Identity:Port"]}";  //IdentityServer授权路径
                            options.ApiName = Configuration["Service:Name"];  //需要授权的服务名称
                        });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();    //认证服务
            app.UseAuthorization();     //使用授权服务

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
