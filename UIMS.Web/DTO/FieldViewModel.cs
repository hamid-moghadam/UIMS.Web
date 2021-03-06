﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIMS.Web.Models;

namespace UIMS.Web.DTO
{
    public class FieldViewModel:BaseModel
    {
        public string Name { get; set; }

        public DegreeViewModel Degree { get; set; }

        public GroupManagerViewModel GroupManager { get; set; }

        public string FullName => $"{Name} | {Degree.Name}";

    }
}
