using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace playWmvc.Models
{
    public class User
    {
        public string Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "your message is too long")]
        [Display(Name = "Enter your name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(10, ErrorMessage = "Your Phone Number Is longer Than 10 Digits Long")]
        [MinLength(10, ErrorMessage ="Your Phone Number Is Less Than 10 Digits Long")]
        public string PhoneNumber { get; set; }
        [Required]
        public bool HasGlasses { get; set; }
        public GenderType Gender { get; set; }
    }

    public enum GenderType
    {
        Male,
        Female
    }
}