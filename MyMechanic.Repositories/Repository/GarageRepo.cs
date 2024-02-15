using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using MyMechanic.Entities.Models;
using MyMechanic.Repositories.Interface;
using MyMechanic.ViewModels;
using System.IO.Compression;

namespace MyMechanic.Repositories.Repository
{
    public class GarageRepo : IGarageRepo
    {
        private readonly ICommonRepo<Garage> _Garages;
        private readonly ICommonRepo<City> _Cities;
        private readonly ICommonRepo<State> _States;
        private readonly ICommonRepo<GarageAvailService> _AvailServices;
        private readonly ICommonRepo<User> _Users;
        private readonly ICommonRepo<ServiceType> _ServiceTypes;
        private readonly ICommonRepo<GarageMedia> _GarageMedia;

        public GarageRepo(ICommonRepo<Garage> Garages,
            ICommonRepo<City> cities,
            ICommonRepo<State> states,
            ICommonRepo<GarageAvailService> availServices,
            ICommonRepo<User> users,
            ICommonRepo<ServiceType> serviceTypes,
            ICommonRepo<GarageMedia> garageMedia)
        {
            _Garages = Garages;
            _Cities = cities;
            _States = states;
            _AvailServices = availServices;
            _Users = users;
            _ServiceTypes = serviceTypes;
            _GarageMedia = garageMedia;
        }
        public List<GarageModel> GetGarageDetails(long MechanicId)
        {
            if (_Garages.ExistUser(u => u.Id == MechanicId))
            {
                var garages = _Garages.GetRecordsWhere(garage => garage.User.Id == MechanicId && garage.DeletedAt == null, garage => garage.City, garage => garage.User);

                List<GarageModel> GaragesData = new List<GarageModel>();

                foreach (Garage item in garages)
                {
                    GarageModel newData = new();
                    newData.Id = item.Id;
                    newData.Name = item.GarageName;
                    newData.Address = item.Address;
                    newData.PostalCode = item.PostalCode;
                    newData.StartingTime = item.StartingTime;
                    newData.EndingTime = item.EndingTime;
                    newData.Status = item.Status;
                    newData.CityId = _Cities.GetFirstOrDefault(u => u.Id == item.City.Id, u => u.State).Id;
                    newData.City = _Cities.GetFirstOrDefault(u => u.Id == item.City.Id, u => u.State).CityName;

                    long stateId = _Cities.GetFirstOrDefault(u => u.Id == item.City.Id, u => u.State).State.Id;
                    newData.StateId = stateId;
                    newData.State = _States.GetFirstOrDefault(u => u.Id == stateId).Name;
                    newData.ExtraDescription = item.ExtraDescription;

                    List<GarageAvailService> GarageServices = _AvailServices.GetRecordsWhere(garage => garage.Garage.Id == item.Id, garage => garage.Garage, garage => garage.ServiceType).ToList();

                    List<GarageAvailServiceModel> serviceModel = new List<GarageAvailServiceModel>();

                    foreach (GarageAvailService garageService in GarageServices)
                    {
                        GarageAvailServiceModel newServiceData = new GarageAvailServiceModel();

                        newServiceData.Id = garageService.Id;
                        newServiceData.GarageId = garageService.Garage.Id;
                        newServiceData.ServiceTypeId = garageService.ServiceType.Id;
                        newServiceData.ServiceType = garageService.ServiceType.Name;
                        newServiceData.Price = garageService.Price;

                        serviceModel.Add(newServiceData);
                    }
                    newData.AvailableServices = serviceModel;

                    GaragesData.Add(newData);
                }
                return GaragesData;
            }
            return new List<GarageModel>();
        }

        public List<GarageModel> GetGarageDataForCustomer()
        {
            List<Garage> Garages = _Garages.GetRecordsWhere(garage => garage.DeletedAt == null, garage => garage.City, garage => garage.User).ToList();
            List<GarageModel> GaragesData = new List<GarageModel>();

            foreach (Garage item in Garages)
            {
                GarageModel newData = new();
                newData.Id = item.Id;
                newData.Name = item.GarageName;
                newData.Address = item.Address;
                newData.PostalCode = item.PostalCode;
                newData.StartingTime = item.StartingTime;
                newData.EndingTime = item.EndingTime;
                newData.Status = item.Status;
                newData.CityId = _Cities.GetFirstOrDefault(u => u.Id == item.City.Id, u => u.State).Id;
                newData.City = _Cities.GetFirstOrDefault(u => u.Id == item.City.Id, u => u.State).CityName;

                long stateId = _Cities.GetFirstOrDefault(u => u.Id == item.City.Id, u => u.State).State.Id;
                newData.StateId = stateId;
                newData.State = _States.GetFirstOrDefault(u => u.Id == stateId).Name;
                newData.ExtraDescription = item.ExtraDescription;
                
                // ------ services
                List<GarageAvailService> GarageServices = _AvailServices.GetRecordsWhere(garage => garage.Garage.Id == item.Id, garage => garage.Garage, garage => garage.ServiceType).ToList();

                List<GarageAvailServiceModel> serviceModel = new List<GarageAvailServiceModel>();

                foreach (GarageAvailService garageService in GarageServices)
                {
                    GarageAvailServiceModel newServiceData = new GarageAvailServiceModel();

                    newServiceData.Id = garageService.Id;
                    newServiceData.GarageId = garageService.Garage.Id;
                    newServiceData.ServiceTypeId = garageService.ServiceType.Id;
                    newServiceData.ServiceType = garageService.ServiceType.Name;
                    newServiceData.Price = garageService.Price;

                    serviceModel.Add(newServiceData);
                }
                newData.AvailableServices = serviceModel;
                // ------- Photos
                List<GarageMedia> GarageMediaList = _GarageMedia.GetRecordsWhere(garage => garage.Garage.Id == item.Id, garage => garage.Garage).ToList();

                List<GaragePhotosModel> GarageModel = new List<GaragePhotosModel>();

                foreach (GarageMedia garageMedia in GarageMediaList)
                {
                    GaragePhotosModel media = new GaragePhotosModel();
                    media.Id = garageMedia.Id;
                    media.Name = garageMedia.Name;
                    string imagePath = Path.Combine("D:/Prj/MyMechanicBackend/Assets/GaragePhotos", garageMedia.Path);
                    if (File.Exists(imagePath))
                    {

                        byte[] zipBytes = File.ReadAllBytes(imagePath);
                        media.Data = zipBytes;
                    }
                    GarageModel.Add(media);
                }
                newData.Photos = GarageModel;
                GaragesData.Add(newData);
            }

            return GaragesData;
        }

        public GarageModel GetGarageDataByGarageId(long GarageId)
        {
            GarageModel garageData = new GarageModel();

            Garage garage = _Garages.GetFirstOrDefault(garage => garage.Id == GarageId, garage => garage.City, garage => garage.User);

            garageData.Id = garage.Id;
            garageData.Name = garage.GarageName;
            garageData.Address = garage.Address;
            garageData.PostalCode = garage.PostalCode;
            garageData.StartingTime = garage.StartingTime;
            garageData.EndingTime = garage.EndingTime;
            garageData.Status = garage.Status;
            garageData.CityId = _Cities.GetFirstOrDefault(u => u.Id == garage.City.Id, u => u.State).Id;
            garageData.City = _Cities.GetFirstOrDefault(u => u.Id == garage.City.Id, u => u.State).CityName;

            long stateId = _Cities.GetFirstOrDefault(u => u.Id == garage.City.Id, u => u.State).State.Id;
            garageData.StateId = stateId;
            garageData.State = _States.GetFirstOrDefault(u => u.Id == stateId).Name;
            garageData.ExtraDescription = garage.ExtraDescription;

            // ------ services
            List<GarageAvailService> GarageServices = _AvailServices.GetRecordsWhere(garage => garage.Garage.Id == GarageId, garage => garage.Garage, garage => garage.ServiceType).ToList();

            List<GarageAvailServiceModel> serviceModel = new List<GarageAvailServiceModel>();

            foreach (GarageAvailService garageService in GarageServices)
            {
                GarageAvailServiceModel newServiceData = new GarageAvailServiceModel();

                newServiceData.Id = garageService.Id;
                newServiceData.GarageId = garageService.Garage.Id;
                newServiceData.ServiceTypeId = garageService.ServiceType.Id;
                newServiceData.ServiceType = garageService.ServiceType.Name;
                newServiceData.Price = garageService.Price;

                serviceModel.Add(newServiceData);
            }
            garageData.AvailableServices = serviceModel;
            // ------- Photos
            List<GarageMedia> GarageMediaList = _GarageMedia.GetRecordsWhere(garage => garage.Garage.Id == GarageId, garage => garage.Garage).ToList();

            List<GaragePhotosModel> GarageModel = new List<GaragePhotosModel>();

            foreach (GarageMedia garageMedia in GarageMediaList)
            {
                GaragePhotosModel media = new GaragePhotosModel();
                media.Id = garageMedia.Id;
                media.Name = garageMedia.Name;
                string imagePath = Path.Combine("D:/Prj/MyMechanicBackend/Assets/GaragePhotos", garageMedia.Path);
                if (File.Exists(imagePath))
                {

                    byte[] zipBytes = File.ReadAllBytes(imagePath);
                    media.Data = zipBytes;
                }
                GarageModel.Add(media);
            }
            garageData.Photos = GarageModel;

            return garageData;
        }
        public void AddEditGarage(NewGarageModel data)
        {
            if (data.Id != 0)
            {
                EditGarage(data);
            }
            else
            {
                AddNewGarage(data);
            }
        }

        public void AddNewGarage(NewGarageModel data)
        {
            Garage model = new Garage();
            model.User = _Users.GetFirstOrDefault(u => u.Id == data.UserId);
            model.City = _Cities.GetFirstOrDefault(u => u.Id == data.City);
            model.GarageName = data.Name;
            model.Address = data.Address;
            model.PostalCode = data.PostalCode;
            model.Status = "ACTIVE";
            model.StartingTime = data.StartingTime;
            model.EndingTime = data.EndingTime;
            model.ExtraDescription = data.ExtraDescription;

            _Garages.AddNew(model);
            _Garages.Save();

            Garage CurrGarage = _Garages.GetRecordsWhere(garage => garage.User.Id == data.UserId && garage.GarageName == data.Name, garage => garage.User).ToList().Last();

            foreach (var availService in data.Charges)
            {
                if (availService != null)
                {
                    GarageAvailService newService = new GarageAvailService()
                    {
                        Garage = CurrGarage,
                        ServiceType = _ServiceTypes.GetFirstOrDefault(st => st.Id == availService.ChargeId),
                        Price = availService.Price,
                    };
                    _AvailServices.AddNew(newService);
                }
            }
            _AvailServices.Save();
        }
        public void EditGarage(NewGarageModel data)
        {
            if (_Garages.ExistUser(garage => garage.Id == data.Id && garage.User.Id == data.UserId, garage => garage.User))
            {
                Garage model = _Garages.GetFirstOrDefault(garage => garage.Id == data.Id && garage.User.Id == data.UserId, garage => garage.User);

                model.City = _Cities.GetFirstOrDefault(u => u.Id == data.City);
                model.GarageName = data.Name;
                model.Address = data.Address;
                model.PostalCode = data.PostalCode;
                model.Status = "ACTIVE";
                model.StartingTime = data.StartingTime;
                model.EndingTime = data.EndingTime;
                model.ExtraDescription = data.ExtraDescription;

                _Garages.Update(model);
                _Garages.Save();

                List<GarageAvailService> oldServices = _AvailServices.GetRecordsWhere(s => s.Garage.Id == data.Id, s => s.Garage).ToList();
                foreach (var availService in oldServices)
                {
                    _AvailServices.DeleteField(availService);
                }
                _AvailServices.Save();
                foreach (var newService in data.Charges)
                {
                    GarageAvailService newS = new GarageAvailService()
                    {
                        Garage = model,
                        ServiceType = _ServiceTypes.GetFirstOrDefault(st => st.Id == newService.ChargeId),
                        Price = newService.Price,
                    };
                    _AvailServices.AddNew(newS);
                }
                _AvailServices.Save();

            }
            else return;
        }

        public void DeleteGarage(long GarageId)
        {
            if(_Garages.ExistUser(g => g.Id == GarageId))
            {
                Garage garage = _Garages.GetFirstOrDefault(g => g.Id == GarageId);
                garage.DeletedAt = DateTime.Now;
                _Garages.Update(garage);
                _Garages.Save();
            }

        }

        public void UploadGaragePhotos(NewGaragePhotosModel model, long GarageId, long UserId)
        {
            if (!_Users.ExistUser(u => u.Id == UserId && u.Role == "MECHANIC"))
            {
                return;
            }
            if (!_Garages.ExistUser(g => g.Id == GarageId && g.User.Id == UserId, g => g.User)) return;
            foreach (var file in model.Files)
            {
                // Access file properties: file.FileName, file.ContentType, file.Length, etc.
                var fileName = GarageId.ToString() + "_" + UserId.ToString() + "_" + Guid.NewGuid().ToString() + "_" + file.FileName;
                // Example: Save the file to disk
                var filePath = Path.Combine("D:\\Prj\\MyMechanicBackend\\Assets\\GaragePhotos", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                GarageMedia newMedia = new()
                {
                    Garage = _Garages.GetFirstOrDefault(g => g.Id == GarageId),
                    Name = file.FileName,
                    MediaType = file.ContentType.Substring(file.ContentType.IndexOf('/')+1),
                    Default = false,
                    Path = fileName
                };
                _GarageMedia.AddNew(newMedia);
                ;
            }
            _GarageMedia.Save();

        }

        public List<GaragePhotosModel> GetGaragePhotos(long GarageId, long UserId)
        {
            List<GarageMedia> imagesData = _GarageMedia.GetRecordsWhere(g => g.Garage.Id == GarageId,g => g.Garage).ToList();

            List<GaragePhotosModel> data = new List<GaragePhotosModel>();

            foreach (var imageInfo in imagesData)
            {
                GaragePhotosModel photo = new GaragePhotosModel();
                string imagePath = Path.Combine("D:/Prj/MyMechanicBackend/Assets/GaragePhotos", imageInfo.Path);
                if (System.IO.File.Exists(imagePath))
                {
               
                    byte[] zipBytes = System.IO.File.ReadAllBytes(imagePath);
                    photo.Data = zipBytes;
                }
                photo.Name = imageInfo.Name;
                photo.Id = imageInfo.Id;
                data.Add(photo);

            }
            

            return data;
        }

        public string DeleteGaragePhoto(long GarageId, long GaragePhotoId)
        {
            GarageMedia imageInfo = _GarageMedia.GetFirstOrDefault(g => g.Id == GaragePhotoId && g.Garage.Id == GarageId, g => g.Garage);
            if (imageInfo == null) return "Image Does not Exiest";
        
            string imagePath = Path.Combine("D:/Prj/MyMechanicBackend/Assets/GaragePhotos", imageInfo.Path);

            if(File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            _GarageMedia.DeleteField(imageInfo);
            _GarageMedia.Save();
            return "Image Deleted Successfully";
        }

    }
}
