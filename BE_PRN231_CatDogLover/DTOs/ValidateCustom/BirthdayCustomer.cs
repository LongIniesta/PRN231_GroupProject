using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ValidateCustom
{
    public class BirthdayCustomer : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = DateTime.Parse(value.ToString());
                TimeSpan age = DateTime.Today - date;
                int years = age.Days / 365;
                if (years < 16)
                {
                    return new ValidationResult("Your must be greater than 16 years old to use this app");
                }
            }
            return ValidationResult.Success;
        }
    }
}
