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
    [DisplayName("Blog")]
    public class Blog : BaseEntity
    {
        [DisplayName("Id Blog")]
        [PrimaryKey]
        public Guid BlogId { get; set; }
        [RequiredAttr]
        [DisplayName("Mã Blog")]
        [MaxLengthAttr(20)]
        [Duplicated]
        [StartWith("BC-")]
        [IsCode]
        [LogAudit]
        public string BlogCode { get; set; }
        [LogAudit]
        [DisplayName("Chủ đề")]
        public string BlogTopic { get; set; }
        [LogAudit]
        [DisplayName("Link hình ảnh")]
        public string BlogImage { get; set; }
        [LogAudit]
        [DisplayName("Tiêu đề")]
        public string BlogTitle { get; set; }
        [LogAudit]
        [DisplayName("Mở đầu")]
        public string BlogIntro { get; set; }
        [LogAudit]
        [DisplayName("Trích dẫn")]
        public string BlogQuote{ get; set; }
        [LogAudit]
        [DisplayName("Từ in đậm")]
        public string BlogHighlight { get; set; }
        [LogAudit]
        [DisplayName("Nội dung chính")]
        public string BlogContent { get; set; }
        [LogAudit]
        [DisplayName("Nội dung khác")]
        public string BlogOther { get; set; }
        [LogAudit]
        [DisplayName("Kết luận")]
        public string BlogSummary { get; set; }
    }
}
