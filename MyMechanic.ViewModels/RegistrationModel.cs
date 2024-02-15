using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.ViewModels
{
    public class RegistrationModel
    {
        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(15)]
        public string MobileNumber { get; set; } = null!;
        
        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = null!;



        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = null!;
    }
}
