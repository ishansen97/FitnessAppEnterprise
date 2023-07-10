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
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
          },
          RedirectUris = { "https://localhost:44372/signin-oidc" },
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

      };

    public static IEnumerable<ApiScope> GetApiScopes() =>
      new List<ApiScope>()
      {
        new ApiScope("APIWorkout"),
        new ApiScope("APIPredictions"),
      };
  }
}
