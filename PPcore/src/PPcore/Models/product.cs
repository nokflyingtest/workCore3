using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPcore.Models
{
    public partial class product
    {
        public string product_code { get; set; }
        public string product_type_code { get; set; }
        public string product_group_code { get; set; }
        [Display(Name = "ผลิตผล")]
        public string product_desc { get; set; }
        [Display(Name = "ลำดับที่")]
        public int rec_no { get; set; }
        public string x_status { get; set; }
        public string x_note { get; set; }
        public string x_log { get; set; }
        public Guid id { get; set; }
        public byte[] rowversion { get; set; }
    }
}
