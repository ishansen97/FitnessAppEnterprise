using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.Configurations;
using AuthenticationService.Data.Models;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService.Data
{
  public static class AppDbInitializer
  {
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
      using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
      {
        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
        serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        configurationDbContext.Database.Migrate();

        AddIdentityServerData(configurationDbContext);


        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        context.Database.Migrate();

        AddBusinessData(context, serviceScope).GetAwaiter().GetResult();
      }
    }

    public static async Task AddBusinessData(AppDbContext context, IServiceScope serviceScope)
    {
      #region Initializing users

      var user1 = context.Users.Where(user => user.UserName == "IshanSen").FirstOrDefault();
      var user2 = context.Users.Where(user => user.UserName == "JohnWick").FirstOrDefault();

      context.Users.RemoveRange(user1, user2);
      await context.SaveChangesAsync();

      var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

      var user1ByEmail = await userManager.FindByEmailAsync("ishan@gmail.com");
      if (user1ByEmail == null)
      {
        user1ByEmail = new AppUser()
        {
          Email = "ishan@gmail.com",
          FirstName = "Ishan",
          LastName = "Seneviratne",
          UserName = "IshanSen",
          Age = 25,
          Height = 175,
          Weight = 68
        };
      }

      await userManager.CreateAsync(user1ByEmail, "user@123");
      //await userManager.AddClaimsAsync(user1ByEmail, new Claim[]
      //{
      //  new Claim(JwtClaimTypes.Name, user1ByEmail.FirstName),
      //  new Claim(JwtClaimTypes.Id, user1ByEmail.Id),
      //});

      var user2ByEmail = await userManager.FindByEmailAsync("johnWick@gmail.com");
      if (user2ByEmail == null)
      {
        user2ByEmail = new AppUser()
        {
          Email = "johnWick@gmail.com",
          FirstName = "John",
          LastName = "Wick",
          UserName = "JohnWick",
          Age = 45,
          Height = 185,
          Weight = 78
        };
      }

      await userManager.CreateAsync(user2ByEmail, "user@123");
      //await userManager.AddClaimsAsync(user2ByEmail, new Claim[]
      //{
      //  new Claim(JwtClaimTypes.Name, user2ByEmail.FirstName),
      //  new Claim(JwtClaimTypes.Id, user2ByEmail.Id),
      //});

      /*else
      {
        // update (one-time)
        var user1 = context.Users.Where(user => user.UserName == "IshanSen").FirstOrDefault();
        var user2 = context.Users.Where(user => user.UserName == "JohnWick").FirstOrDefault();

        user1.FirstName = "Ishan";
        user1.LastName = "Seneviratne";
        user1.Age = 25;
        user1.Height = 175;
        user1.Weight = 68;

        user2.FirstName = "John";
        user2.LastName = "Wick";
        user2.Age = 45;
        user2.Height = 185;
        user2.Weight = 78;


      }*/

      #endregion

    }

    private static void AddIdentityServerData(ConfigurationDbContext configurationDbContext)
    {
      if (!configurationDbContext.Clients.Any())
      {
        foreach (var client in IdentityServerConfiguration.GetClients())
        {
          configurationDbContext.Clients.Add(client.ToEntity());
        }
      }

      configurationDbContext.SaveChanges();

      if (!configurationDbContext.IdentityResources.Any())
      {
        foreach (var identityResource in IdentityServerConfiguration.GetIdentityResources())
        {
          configurationDbContext.IdentityResources.Add(identityResource.ToEntity());
        }
      }

      configurationDbContext.SaveChanges();

      if (!configurationDbContext.ApiResources.Any())
      {
        foreach (var apiResource in IdentityServerConfiguration.GetApiResources())
        {
          configurationDbContext.ApiResources.Add(apiResource.ToEntity());
        }
      }

      configurationDbContext.SaveChanges();

      if (!configurationDbContext.ApiScopes.Any())
      {
        foreach (var apiScope in IdentityServerConfiguration.GetApiScopes())
        {
          configurationDbContext.ApiScopes.Add(apiScope.ToEntity());
        }
      }

      configurationDbContext.SaveChanges();
    }
  }
}
