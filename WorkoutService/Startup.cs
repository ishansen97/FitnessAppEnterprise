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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WorkoutService.Context;
using WorkoutService.Helpers;
using WorkoutService.Service;
using WorkoutService.Service.Interfaces;

namespace WorkoutService
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
      services.AddDbContext<WorkoutDbContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
      });
      services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", config =>
        {
          config.Authority = "https://eadfitnessauthserver.azurewebsites.net";
          config.Audience = "APIWorkout";
          config.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "https://eadfitnessauthserver.azurewebsites.net",
            ValidAudience = "APIWorkout",
          };
        });
      services.AddCors(config =>
      {
        config.AddPolicy("AllowAll", p =>
        {
          p.AllowAnyOrigin();
          p.AllowAnyMethod();
          p.AllowAnyHeader();
          //p.AllowCredentials();
        });
      });
      services.AddScoped<IWorkoutService, WorkoutHandler>();
      services.AddScoped<ICheatMealService, CheatMealService>();
      services.AddScoped<IExerciseService, ExerciseService>();
      services.AddScoped<IMealMeasurementService, MealMeasurementService>();
      services.AddSingleton<WorkoutTypeHelper>();
      services.AddSingleton<WorkoutModelHelper>();
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
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout and CheatMeal Server");
        options.RoutePrefix = string.Empty;
      });

      // initialize the app
      //AppInitializer.Seed(app);
    }
  }
}
