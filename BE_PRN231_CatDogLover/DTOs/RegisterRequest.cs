using DTOs.ValidateCustom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        [MaxLength(250)]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,})",
    ErrorMessage = "The new password must have all of these conditions: " +
    "\n-At least 8 characters" +
    "\n-Contains a lowercase, a uppercase and a special character")]
        public string Password { get; set; } = null!;
        [Required]
        public string PasswordConfirm { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        [BirthdayCustomer]
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(20)]
        public string? Phone { get; set; }
        [MaxLength(500)]
        public string? Address { get; set; }
        [MaxLength(500)]
        public string? AvatarLink { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
