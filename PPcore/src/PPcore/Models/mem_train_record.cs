using System;
using System.ComponentModel.DataAnnotations;

namespace PPcore.Models
{
    public partial class mem_train_record
    {
        [Display(Name = "รหัสหลักสูตรอบรม")]
        public string course_code { get; set; }
        public string member_code { get; set; }
        [Display(Name = "ระดับเกรด")]
        public string course_grade { get; set; }
        public string x_status { get; set; }
        public string x_note { get; set; }
        public string x_log { get; set; }
        public Guid id { get; set; }
    }
}
