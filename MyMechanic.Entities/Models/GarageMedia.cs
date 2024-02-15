using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Entities.Models
{
    public class GarageMedia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Garage Garage { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string MediaType { get; set; } = string.Empty;

        [Required]
        public string Path { get; set; } = String.Empty;

        [Required]
        public bool Default { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

    }
}
