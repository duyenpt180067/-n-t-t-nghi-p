using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.General.Entities
{
    public class ServiceResult
    {
        /// <summary>
        /// trang thai service: true-thanh cong, false: that bai
        /// </summary>
        public bool Success { get; set; } = false;
        public string ResultCode { get; set; } = string.Empty;
        public object Data { get; set; }
        public string UserMgs { get; set; } = string.Empty;

        public ServiceResult(bool success, string resultCode, object data, string userMgs)
        {
            Success = success;
            ResultCode = resultCode;
            Data = data;
            UserMgs = userMgs;

        }
        public ServiceResult(){}
    }
}
