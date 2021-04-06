using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiniBookIdentity.Configuration;
using MiniBookIdentity.Data;
using MiniBookIdentity.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBookIdentity
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
            //Connect Db
            services.AddDbContext<MiniBookDbContext>(option =>
            option.UseSqlServer(Configuration.GetConnectionString("MiniBookConnection")));

            //Identity 
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MiniBookDbContext>()
                .AddDefaultTokenProviders(); //Provide token;

            //Config Password
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true; //Unique email in Db
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;               
            });

            #region Fix: System.InvalidOperationException: Synchronous operations are disallowed.
            //Kestrel
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // IIS
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            #endregion            

            //Config IdentityServer 4, lien quan den authorization
            services.AddIdentityServer()
                //Chung chi bao mat de sinh ra Token
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<User>();

            services.AddControllers();
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

            app.UseAuthentication();

            app.UseAuthorization();

            //automatically set culture information for requests based on information provided by the clien           
            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                //Mac dinh, cac request se la 'en' 
                DefaultRequestCulture = new RequestCulture("en"),
                //Culture ho tro cho app
                SupportedCultures = new [] {new CultureInfo("en"), new CultureInfo("vi")},
                //Culture ho tro cho UI
                SupportedUICultures = new[] { new CultureInfo("en"), new CultureInfo("vi") },
                //Chinh sua culture 
                RequestCultureProviders = new IRequestCultureProvider[] {
                    //Cung cap culture vao query
                    new QueryStringRequestCultureProvider(), 
                    //Cung cap culture vao cookie
                    new CookieRequestCultureProvider(),
                    //Cung cap culture vao header cua request
                    new AcceptLanguageHeaderRequestCultureProvider()
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                
            });
        }
    }
}
