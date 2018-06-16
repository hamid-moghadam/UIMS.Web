﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UIMS.Web.DTO
{
    public class StudentPresentationInsertViewModel
    {
        [Required]
        public string StudentCode { get; set; }

        [Required]
        public string PresentationCode { get; set; }

    }
}
