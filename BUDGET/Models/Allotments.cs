using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUDGET
{
    public class Allotments
    {
        [Key]
        public Int32 ID { get; set; }
        public String Title { get; set; }
        public String Code { get; set; }
        public String year { get; set; }
        public Int32 active { get; set; }
        public String Code2 { get; set; }
        public Boolean previous { get; set; }
        
    }
}