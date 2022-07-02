using FoodManagement.Core.General.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodManagement.Core.General.Entities.BaseEntity;

namespace FoodManagement.Core.Entities.BussinessItem
{
    [DisplayName("Lượt thích")]
    public class Favorite : BaseEntity
    {
        [DisplayName("Id Favorite")]
        [PrimaryKey]
        public Guid FavoriteId { get; set; }
        [RequiredAttr]
        [DisplayName("Thực phẩm")]
        public Guid? FoodId { get; set; }
        [RequiredAttr]
        [DisplayName("Người dùng")]
        public Guid? UserId { get; set; }
        [LogAudit]
        [DisplayName("Tên món")]
        public string FoodName { get; set; }
        [LogAudit]
        [DisplayName("Mã món ăn")]
        public string FoodCode { get; set; }
        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
    }
}
