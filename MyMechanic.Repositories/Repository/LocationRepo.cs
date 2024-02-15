using MyMechanic.Entities.Models;
using MyMechanic.Repositories.Interface;
using MyMechanic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Repositories.Repository
{
    public class LocationRepo : ILocationRepo
    {
        private readonly ICommonRepo<City> _Cities;
        private readonly ICommonRepo<State> _States;
        private readonly ICommonRepo<ServiceType> _ServiceTypes;

        public LocationRepo(ICommonRepo<City> cities,
            ICommonRepo<State> States,
            ICommonRepo<ServiceType> ServiceTypes)
        {
            _Cities = cities;
            _States = States;
            _ServiceTypes = ServiceTypes;
        }
        public List<CityModel> GetAllCities()
        {
            List<City> list = _Cities.GetAll(city => city.State).ToList();
            List<CityModel> cities= new List<CityModel>();

            foreach (City city in list)
            {
                CityModel newCity = new CityModel();
                newCity.Id = city.Id;
                newCity.Name = city.CityName;
                newCity.StateId = city.State.Id;
                newCity.StateName = city.State.Name;

                cities.Add(newCity);
            }
            return cities;
        }
        public List<State> GetAllStates()
        {
            return _States.GetAll().ToList();
        }

        public List<ServiceType> GetAllServiceTypes()
        {
            return _ServiceTypes.GetAll().ToList();
        }
    }
}
