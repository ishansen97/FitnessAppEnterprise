using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthenticationService.Configurations
{
  public static class IdentityServerConfiguration
  {
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
      new List<IdentityResource>()
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
      };

    public static IEnumerable<Client> GetClients() =>
      new List<Client>()
      {
        new Client
        {
          ClientId = "mvc_client",
          ClientSecrets = { new Secret("mvc_client_secret".ToSha256()) },
          AllowedGrantTypes = GrantTypes.Code,
          AllowedScopes =
          {
            "APIWorkout", 
            "APIPredictions",
            "APIWeeklyView",
            "APIReport",
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
          },
          RedirectUris = { "https://eadfitnessmvc.azurewebsites.net/signin-oidc" },
          PostLogoutRedirectUris = { "https://eadfitnessmvc.azurewebsites.net/FirstPage" },
          AllowOfflineAccess = true
        }
      };

    public static IEnumerable<ApiResource> GetApiResources() =>
      new List<ApiResource>()
      {
        new ApiResource
        {
          Name = "APIWorkout",
          Scopes = {"APIWorkout"}
        },
        new ApiResource
        {
          Name = "APIPredictions",
          Scopes = { "APIPredictions" }
        },
        new ApiResource
        {
          Name = "APIWeeklyView",
          Scopes = { "APIWeeklyView" }
        },
        new ApiResource
        {
          Name = "APIReport",
          Scopes = { "APIReport" }
        }
      };

    public static IEnumerable<ApiScope> GetApiScopes() =>
      new List<ApiScope>()
      {
        new ApiScope("APIWorkout"),
        new ApiScope("APIPredictions"),
        new ApiScope("APIWeeklyView"),
        new ApiScope("APIReport"),
      };
  }
}
