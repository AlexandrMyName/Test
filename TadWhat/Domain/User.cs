using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TadWhat.Domain
{
    public class User
    {
        public Guid Id { get; set; }


        [Required]
        [MinLength(3,ErrorMessage = "Минимальное количиство символов - 3")]
        public string Name { get; set; }


        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [MinLength(5, ErrorMessage = "Минимальное количиство символов - 5")]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
