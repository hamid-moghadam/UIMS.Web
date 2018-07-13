﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIMS.Web.Models;

namespace UIMS.Web.DTO
{
    public class SettingsViewModel:BaseModel
    {
        public string AccessName { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
