﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UIMS.Web.DTO
{
    public class UserGetAllInputViewModel:Pagination
    {
        public string Role { get; set; } = "admin";

        public string SearchQuery { get; set; } = "";
    }
}
