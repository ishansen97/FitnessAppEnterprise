using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Configurations;
using AuthenticationService.Data;
using AuthenticationService.Data.Models;
using AuthenticationService.Helpers;
using AuthenticationService.Services.Implementations;
using AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace AuthenticationService
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      ConnectionString = Configuration.GetConnectionString("DefaultConnectionString");
    }

    public IConfiguration Configuration { get; }

    public string ConnectionString { get; set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();
      services.AddDbContext<AppDbContext>(config =>
      {
        //config.UseInMemoryDatabase("Memory");
        config.UseSqlServer(ConnectionString);
      });

      // AddIdentity registers the services
      services.AddIdentity<AppUser, IdentityRole>(config => {
          config.Password.RequiredLength = 6;
          config.Password.RequireDigit = false;
          config.Password.RequireNonAlphanumeric = false;
          config.Password.RequireUppercase = false;
          //config.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

      // this line is used to add authentication, for Identity
      services.ConfigureApplicationCookie(config =>
      {
        config.Cookie.Name = "IdentityServer.Cookie";
        config.LoginPath = "/Auth/Login";
      });

      // configuring the IdentityServer service.
      // Api resources - the endpoints that needed to be secured.
      // clients - client applications that needs to be authorized to access Api resources.
      services.AddIdentityServer()
        .AddAspNetIdentity<AppUser>()
        .AddConfigurationStore(options =>
        {
          options.ConfigureDbContext = db => db.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("AuthenticationService"));
        })
        .AddOperationalStore(options =>
        {
          options.ConfigureDbContext = db => db.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("AuthenticationService"));
        })
        .AddDeveloperSigningCredential(); // change the signing credential when moving to Azure.

      services.AddScoped<IUserService, UserService>();
      services.AddSingleton<ModelHelper>();
      services.AddControllers();
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
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseIdentityServer();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapControllers();
      });

      // this is to initialize the database.
      //AppDbInitializer.Seed(app);
    }
  }
}
