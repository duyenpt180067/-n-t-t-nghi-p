using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Interface.IService.General
{
    public interface IStorageService
    {
        public string UploadFile(IFormFile file);
        public List<string> UploadMultipleFile(List<IFormFile> file);
    }
}
