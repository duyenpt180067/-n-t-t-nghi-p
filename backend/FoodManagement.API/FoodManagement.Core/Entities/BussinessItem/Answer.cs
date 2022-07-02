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
    [DisplayName("Phiếu trả lời")]
    public class Answer : BaseEntity
    {
        [DisplayName("Id phiếu trả lời")]
        [PrimaryKey]
        public Guid AnswerId { get; set; }
        [RequiredAttr]
        [DisplayName("Phiếu đánh giá")]
        public Guid RatingSheetId { get; set; }
        [RequiredAttr]
        [DisplayName("Người dùng")]
        public Guid UserId { get; set; }
        [RequiredAttr]
        [DisplayName("Phiếu trả lời")]
        public string AnswerContent { get; set; }
    }
}
