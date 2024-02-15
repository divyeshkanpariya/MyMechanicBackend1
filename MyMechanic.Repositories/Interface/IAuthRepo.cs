using MyMechanic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Repositories.Interface
{
    public interface IAuthRepo
    {
        public RegistrationModel RegisterNewUser(RegistrationModel model);

        public UserInfoModel VarifyUser(LoginModel loginModel);

    }
}
