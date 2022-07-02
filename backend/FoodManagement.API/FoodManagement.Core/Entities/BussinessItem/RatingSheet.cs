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
    [DisplayName("Phiếu đánh giá")]
    public class RatingSheet : BaseEntity
    {
        [DisplayName("Id phiếu đánh giá")]
        [PrimaryKey]
        public Guid RatingSheetId { get; set; }
        [RequiredAttr]
        [DisplayName("Mã phiếu đánh giá")]
        [MaxLengthAttr(20)]
        [Duplicated]
        [StartWith("RC-")]
        public string RatingSheetCode { get; set; }
        public string RatingSheetName { get; set; }
        public bool? RatingSheetStatus { get; set; }
    }
}
