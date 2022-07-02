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
    [DisplayName("Slide")]
    public class Slider : BaseEntity
    {
        [DisplayName("Id Slider")]
        [PrimaryKey]
        public Guid SliderId { get; set; }
        [RequiredAttr]
        [DisplayName("Mã Slider")]
        [MaxLengthAttr(20)]
        [Duplicated]
        [StartWith("SC-")]
        [IsCode]
        [LogAudit]
        public string SliderCode { get; set; }
        [LogAudit]
        [DisplayName("Link hình ảnh")]
        public string SliderImage { get; set; }
        [LogAudit]
        [DisplayName("Link đến")]
        public string SliderLink { get; set; }
        public bool SliderStatus { get; set; }
        [LogAudit]
        [DisplayName("Nội dung slide")]
        public string SliderContent { get; set; }
        [LogAudit]
        [DisplayName("Tiêu đề")]
        public string SliderTitle { get; set; }
        [LogAudit]
        [DisplayName("Tên slide")]
        public string SliderName { get; set; }
        public int? Priority { get; set; }
    }
}
