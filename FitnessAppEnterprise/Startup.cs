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
using Microsoft.AspNetCore.Http;

namespace FitnessAppEnterprise
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
      services.AddAuthentication(config =>
      {
        config.DefaultScheme = "Cookie";
        config.DefaultChallengeScheme = "oidc";
      })
        .AddCookie("Cookie")
        .AddOpenIdConnect("oidc", config =>
        {
          config.Authority = "https://localhost:44384/";
          config.ClientId = "mvc_client";
          config.ClientSecret = "mvc_client_secret";
          config.SaveTokens = true;
          config.ResponseType = "code";

          config.GetClaimsFromUserInfoEndpoint = true;
          config.Scope.Add("APIWorkout");
          config.Scope.Add("APIPredictions");
          config.Scope.Add("offline_access");
        });
      services.AddHttpClient();
      services.AddDistributedMemoryCache();
      services.AddSession();
      services.AddControllersWithViews();
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

      app.UseAuthentication();
      app.UseAuthorization();
      app.UseSession();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
