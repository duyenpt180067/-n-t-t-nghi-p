using FoodManagement.Core.Entities.FMUser;
using FoodManagement.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMUser
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public User Login(string useName, string pass);
        public string GetMD5(string pass);
    }
}
