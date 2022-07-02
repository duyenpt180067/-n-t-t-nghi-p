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
    [DisplayName("Câu hỏi")]
    public class Question : BaseEntity
    {
        [DisplayName("Id câu hỏi")]
        [PrimaryKey]
        public Guid QuestionId { get; set; }
        [RequiredAttr]
        [DisplayName("Phiếu đánh giá")]
        public Guid? RatingSheetId { get; set; }
        [RequiredAttr]
        [DisplayName("Mã câu hỏi")]
        [MaxLengthAttr(20)]
        [Duplicated]
        [StartWith("QC-")]
        [IsCode]
        public string QuestionCode { get; set; }
        public string QuestionName { get; set; }
        [RequiredAttr]
        [DisplayName("Nội dung câu hỏi")]
        public string QuestionContent { get; set; }
        public bool? QuestionStatus { get; set; }
        public string RatingSheetCode { get; set; }
        public string RatingSheetName { get; set; }
        public bool? RatingSheetStatus { get; set; }
    }
}
