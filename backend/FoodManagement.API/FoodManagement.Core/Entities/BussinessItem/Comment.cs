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
    [DisplayName("Bình luận")]
    public class Comment : BaseEntity
    {
        [DisplayName("Id Bình luận")]
        [PrimaryKey]
        public Guid CommentId { get; set; }
        [RequiredAttr]
        [DisplayName("Người dùng")]
        public Guid? UserId { get; set; }
        public Guid? FoodId { get; set; }
        public Guid? OrderId { get; set; }
        [LogAudit]
        [DisplayName("Bình luận")]
        public string CommentContent { get; set; }
        public bool? CommentStatus { get; set; }
        [LogAudit]
        [DisplayName("Số sao")]
        public int? CommentStar { get; set; }
        [LogAudit]
        [DisplayName("Người dùng")]
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public bool? IsAdmin { get; set; }
        [LogAudit]
        [DisplayName("Tên món")]
        public string FoodName { get; set; }
        [LogAudit]
        [DisplayName("Mã món ăn")]
        public string FoodCode { get; set; }
    }
}
