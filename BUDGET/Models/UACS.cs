using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUDGET
{
    public class UACS
    {
        [Key]
        public Int32 ID { get; set; }
        public Int32 Line { get; set; }
        [Required]
        public String Title { get; set; }
        [Required]
        public String Code { get; set; }
        
    }
}