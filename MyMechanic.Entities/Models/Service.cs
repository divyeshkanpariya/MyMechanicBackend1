using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Entities.Models
{
    public class Service
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public Appointment Appointment { get; set; } = null!;

        [Required]
        [StringLength(15)]
        public string Status { get; set; } = null!;

        [Required]
        public decimal Rating { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

    }
}
