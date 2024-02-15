using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Entities.Models
{
    public class GarageAvailService
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Garage Garage { get; set; } = null!;

        [Required]
        public ServiceType ServiceType { get; set; } = null!;

        [Required]        
        public Decimal Price { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
