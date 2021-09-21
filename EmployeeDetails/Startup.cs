using EmployeeDetails.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeDetails
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
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddDbContextPool<ApplicationDpclass>(Options => Options.UseSqlServer(Configuration.GetConnectionString("MyConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;


            }).AddEntityFrameworkStores<ApplicationDpclass>().AddDefaultTokenProviders();

            //services.Configure<IdentityOptions>(options =>
            //{

            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers = true;
                
              
            //});
            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            {
                options.Prompt = "select_account";
            });

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromSeconds(60);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});

            // services.AddAuthentication(Options =>
            // {
            //     Options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //     Options.DefaultChallengeScheme = MicrosoftAccountDefaults.AuthenticationScheme;
            // }).AddCookie().AddMicrosoftAccount(Options =>
            //{
            //    Options.ClientId = Configuration["Authentication:ClientId"];
            //    Options.ClientSecret = Configuration["Authentication:ClientSecret"];      
            //});


            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            {
                options.Prompt = "select_account";
            }).AddAuthentication().AddMicrosoftAccount(Options =>
                    {
                Options.ClientId = Configuration["Authentication:Microsoft:ClientId"];
                Options.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
                Options.AuthorizationEndpoint = string.Concat(Options.AuthorizationEndpoint, "?prompt=select_account");


                        //Options.CallbackPath = "/signin-microsoft";
                    }).AddFacebook(Options =>
            {
                Options.ClientId = Configuration["Authentication1:Facebook:ClientId"];
                Options.ClientSecret = Configuration["Authentication1:Facebook:ClientSecret"];
                Options.AuthorizationEndpoint = string.Concat(Options.AuthorizationEndpoint, "?prompt=select_account");



            });



           

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //        .AddCookie(item => item.LoginPath = new PathString("/Authorized/login"))
            //        // this service Use for Oauth method
            //        .AddOAuth("Microsoft-AccessToken", "Microsoft AccessToken only", option =>
            //        {
            //            option.ClientId = Configuration["Authentication:Microsoft:clientid"];
            //            option.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
            //            option.CallbackPath = new PathString("/signin-microsoft-token");
            //            option.AuthorizationEndpoint = MicrosoftAccountDefaults.AuthorizationEndpoint;
            //            option.TokenEndpoint = MicrosoftAccountDefaults.TokenEndpoint;
            //            option.Scope.Add("https://graph.microsoft.com/user.read");
            //            option.SaveTokens = true;
            //            option.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            //            option.ClaimActions.MapJsonKey(ClaimTypes.Name, "displayName");
            //            option.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "givenName");
            //            option.ClaimActions.MapJsonKey(ClaimTypes.Surname, "surname");
            //            option.ClaimActions.MapCustomJson(ClaimTypes.Email,
            //                user => user.GetString("mail") ?? user.GetString("userPrincipalName"));
            //        })
            //        .AddMicrosoftAccount(option =>
            //        {
            //            option.ClientId = Configuration["Authentication:Microsoft:clientid"];
            //            option.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
            //            option.SaveTokens = true;
            //        });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            //app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Data}/{action=Index}/{id?}");
            });
        }
    }
}


// link http://www.binaryintellect.net/articles/3d6ce8b3-cb62-42b7-bedc-5e7f2fb9d017.aspx
//https://csharp-video-tutorials.blogspot.com/2019/09/aspnet-core-google-authentication.html