using FoodManagement.Core.Entities.FMUser;
using FoodManagement.Core.General.Entities;
using FoodManagement.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Interfaces.Infrastructure.IService.FMUser
{
    public interface IUserService : IBaseService<User>
    {
        public ServiceResult Login(string userName, string pass);
        public ServiceResult UpdateUser(string userName, User user);
    }
}
