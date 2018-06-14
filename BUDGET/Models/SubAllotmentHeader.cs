using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BUDGET
{
    public class SubAllotmentHeader
    {
        [Key]
        public Int32 ID { get; set; }
        public String Title { get; set; }
        public String allotment { get; set; }
        public String allotment_for { get; set; }
        public String TitleCode { get; set; }
        public String prexc { get; set; }
    }
}