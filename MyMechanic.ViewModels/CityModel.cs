using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.ViewModels
{
    public class CityModel
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public long StateId { get; set; }

        public string StateName { get; set; } = string.Empty;
    }
}
