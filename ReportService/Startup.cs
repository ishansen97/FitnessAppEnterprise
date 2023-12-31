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
using ReportService.Helpers;
using ReportService.Services;
using ReportService.Services.Interfaces;

namespace ReportService
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
          config.Audience = "APIReport";
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
      services.AddScoped<IReportService, ReportHandler>();
      services.AddScoped<IRemoteService, ApiService>();
      services.AddSingleton<ModelHelper>();
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
      app.UseStaticFiles();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseSwagger();
      app.UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Report Server");
        options.RoutePrefix = string.Empty;
      });
    }
  }
}
