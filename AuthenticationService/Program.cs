using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //var host = CreateHostBuilder(args).Build();
      //// seeding the user.
      //using (var scope = host.Services.CreateScope())
      //{
      //  var userManager = scope.ServiceProvider
      //    .GetRequiredService<UserManager<IdentityUser>>();

      //  var user = new IdentityUser("Ishan");
      //  userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
      //  // claim for id token.
      //  //userManager.AddClaimAsync(user, new Claim("rc.grandma", "big.cookie"))
      //  //  .GetAwaiter().GetResult();
      //  //// claim for access token.
      //  //userManager.AddClaimAsync(user, new Claim("rc.api.grandma", "big.api.cookie"))
      //  //  .GetAwaiter().GetResult();

      //}

      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
