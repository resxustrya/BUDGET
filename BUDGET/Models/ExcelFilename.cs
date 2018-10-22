using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BUDGET
{
    public class ExcelFilename
    {
        [Key]
        public Int32 ID { get; set; }
        public String Filename { get; set; }
    }
}