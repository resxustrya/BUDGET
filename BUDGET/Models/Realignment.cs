using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BUDGET
{
    public class Realignment
    {
        [Key]
        public Int32 ID { get; set; }
        public String uacs_from { get; set; }
        public String uacs_to { get; set; }
        public Double amount { get; set; }
        public String fundsource { get; set; }
    }
}