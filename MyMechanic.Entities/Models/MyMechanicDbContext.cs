using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Entities.Models
{
    public class MyMechanicDbContext:DbContext
    {
        public MyMechanicDbContext(DbContextOptions<MyMechanicDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<State> State { get; set; }

        public DbSet<City> City { get; set; }

        public DbSet<Garage> Garage { get; set; }

        public DbSet<ServiceType> ServiceType { get; set; }

        public DbSet<GarageAvailService> GarageAvailService { get; set; }

        public DbSet<GarageMedia> GarageMedia { get; set;}

        public DbSet<CarManufacturer> CarManufacturer { get; set; }

        public DbSet<Appointment> Appointment { get; set; }

        public DbSet<AppointmentService> AppointmentService { get; set; }

        public DbSet<Invoice> Invoice { get; set; }

        public DbSet<InvoiceItem> InvoiceItem { get; set; }
    }
}
