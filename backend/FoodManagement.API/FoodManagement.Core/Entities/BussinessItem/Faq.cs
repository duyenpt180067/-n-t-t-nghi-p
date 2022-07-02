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
    [DisplayName("Câu hỏi thường gặp")]
    public class Faq : BaseEntity
    {
        [DisplayName("Id Faq")]
        [PrimaryKey]
        public Guid FaqId { get; set; }
        [RequiredAttr]
        [DisplayName("Mã câu hỏi thường gặp")]
        [MaxLengthAttr(20)]
        [Duplicated]
        [StartWith("FAC-")]
        [IsCode]
        [LogAudit]
        public string FaqCode { get; set; }
        public string FaqName { get; set; }
        [LogAudit]
        [DisplayName("Nội dung câu hỏi")]
        public string FaqQuestion { get; set; }
        [LogAudit]
        [DisplayName("Nội dung trả lời")]
        public string FaqAnswer { get; set; }
        public int? Priority { get; set; }
    }
}
