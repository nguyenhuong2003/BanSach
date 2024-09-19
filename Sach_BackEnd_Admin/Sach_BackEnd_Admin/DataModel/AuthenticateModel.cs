﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class AuthenticateModel
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string pass { get; set; }
    }

    public class AppSettings
    {
        public string Secret { get; set; }

    }
}
