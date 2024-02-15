using MyMechanic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Repositories.Interface
{
    public interface IGarageRepo
    {
        public List<GarageModel> GetGarageDetails(long MechanicId);

        public void AddEditGarage(NewGarageModel data);

        public void DeleteGarage(long GarageId);

        public void UploadGaragePhotos(NewGaragePhotosModel model, long GarageId, long UserId);

        public List<GaragePhotosModel> GetGaragePhotos(long GarageId, long UserId);

        public string DeleteGaragePhoto(long GarageId, long GaragePhotoId);

        public List<GarageModel> GetGarageDataForCustomer();

        public GarageModel GetGarageDataByGarageId(long GarageId);
    }
}
