﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIMS.Web.DTO
{
    public class FilterInputViewModel:Pagination
    {
        public string[] Filters { get; set; }
    }
}
