using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPcore.Models
{
    public partial class mem_worklist
    {
        [Display(Name = "ลำดับที่")]
        public int rec_no { get; set; }
        public string member_code { get; set; }
        [Display(Name = "ชื่อสถานที่ทำงาน")]
        public string company_name_th { get; set; }
        [Display(Name = "ชื่อสถานที่ทำงาน (ภาษาอังกฤษ)")]
        public string company_name_eng { get; set; }
        [Display(Name = "ตำแหน่งงาน")]
        public string position_name_th { get; set; }
        [Display(Name = "ตำแหน่งงาน (ภาษาอังกฤษ)")]
        public string position_name_eng { get; set; }
        [Display(Name = "ปีที่ทำงาน")]
        public string work_year { get; set; }
        [Display(Name = "ที่อยู่สถานที่ทำงาน")]
        public string office_address { get; set; }
        public string x_status { get; set; }
        public string x_note { get; set; }
        public string x_log { get; set; }
        public Guid id { get; set; }
        public byte[] rowversion { get; set; }
    }
}
