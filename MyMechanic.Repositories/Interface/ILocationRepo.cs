using MyMechanic.Entities.Models;
using MyMechanic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Repositories.Interface
{
    public interface ILocationRepo
    {
        public List<CityModel> GetAllCities();

        public List<State> GetAllStates();

        public List<ServiceType> GetAllServiceTypes();
    }
}
