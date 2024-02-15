using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.ViewModels
{
    public class NewGaragePhotosModel
    {

        public List<IFormFile> Files { get; set; }
    }
}
