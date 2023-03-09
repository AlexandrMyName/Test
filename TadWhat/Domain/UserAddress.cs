using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TadWhat.Domain
{
    public class UserAddress
    {
        [Required]
        [MinLength(3, ErrorMessage = "Минимальное количиство символов - 3")]
        public string City { get; set; }


        [Required]
        [MinLength(2, ErrorMessage = "Минимальное количиство символов - 2")]
        public string Street { get; set; }


        [Required]
        [MinLength(1, ErrorMessage = "Минимальное количиство символов - 1")]
        
        public string House { get; set; }
    }
}
