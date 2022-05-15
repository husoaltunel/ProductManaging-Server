﻿using Core.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class LoginInfoDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public AccessToken AccessToken { get; set; }

    }
}
