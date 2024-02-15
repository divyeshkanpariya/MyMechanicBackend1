using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Entities.Models
{
    public class Garage
    {
        [Key]
        public long Id { get; set; }

        public User User { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string GarageName { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [StringLength(6)]
        public string PostalCode { get; set; } = null!;

        public City City { get; set; } = null!; 


        [Required]
        public TimeSpan StartingTime { get; set; }

        [Required]
        public TimeSpan EndingTime { get; set; }

        public string ExtraDescription { get; set; } = null!;

        [Required]

        [StringLength(10)]

        public string Status { get; set; } = "ACTIVE";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<GarageAvailService> GarageAvailServices { get; set; } = null!;

        public ICollection<GarageMedia> GarageMedias { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; } = null!;
    }
}
