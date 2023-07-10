using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Data.Models
{
    public class AppUser : IdentityUser
    {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public int Age { get; set; }

      public int Height { get; set; }

      public double Weight { get; set; }
    }
}
