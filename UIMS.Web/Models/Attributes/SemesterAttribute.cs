﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UIMS.Web.Extentions;

namespace UIMS.Web.Models.Attributes
{
    public class SemesterAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            if (value.ToString().IsSemester())
                return ValidationResult.Success;

            return new ValidationResult("نیمسال تحصیلی وارد شده صحیح نیست");
        }
    }
}
