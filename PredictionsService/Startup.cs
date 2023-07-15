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
using Microsoft.EntityFrameworkCore;
using PredictionsService.Context;
using PredictionsService.Helpers;
using PredictionsService.Services;
using PredictionsService.Services.Interfaces;

namespace PredictionsService
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
      services.AddDbContext<PredictionDbContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
      });

      services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", config =>
        {
          config.Authority = "https://localhost:44384";
          config.Audience = "APIPredictions";
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

      services.AddScoped<IPredictionConstantsService, PredictionConstantsService>();
      services.AddScoped<IPredictionService, PredictionService>();
      services.AddSingleton<ModelHelper>();
      
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

      // initialize the database.
      //ContextInitializer.Seed(app);
    }
  }
}
