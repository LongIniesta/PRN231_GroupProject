using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ValidateCustom
{
    public class TypePost : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                String s = value.ToString();
                if ( s != "product" && s != "service" && s != "gift") return new ValidationResult("Post type is invalid");
            }
            return ValidationResult.Success;
        }
    }
}
