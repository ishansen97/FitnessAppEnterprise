﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Model
{
    public class LoginModel
    {
      public string UserName { get; set; }

      [DataType(DataType.Password)]
      public string Password { get; set; }

      public string ReturnUrl { get; set; }
    }
}
