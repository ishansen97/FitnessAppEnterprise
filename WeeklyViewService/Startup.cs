using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeeklyViewService.Helpers;
using WeeklyViewService.Services;
using WeeklyViewService.Services.Interfaces;

namespace WeeklyViewService
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
      services.AddControllers();
      services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", config =>
        {
          config.Authority = "https://eadfitnessauthserver.azurewebsites.net";
          config.Audience = "APIWeeklyView";
        });

      services.AddCors(setup =>
      {
        setup.AddPolicy("AllowAll", p =>
        {
          p.AllowAnyOrigin();
          p.AllowAnyMethod();
          p.AllowAnyHeader();
          //p.AllowCredentials();
        });
      });

      services.AddHttpContextAccessor();
      services.AddHttpClient();
      services.AddSingleton<DateHelper>();
      services.AddSingleton<ModelHelper>();
      services.AddScoped<IWeeklyViewService, WeeklyViewHandler>();
      services.AddScoped<IRemoteService, ApiService>();
      services.AddSwaggerGen();
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

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseSwagger();
      app.UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Weekly View Server");
        options.RoutePrefix = string.Empty;
      });
    }
  }
}
