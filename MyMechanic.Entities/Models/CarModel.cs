using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Entities.Models
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public CarManufacturer CarManufacturer { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = null!;
    }
}
