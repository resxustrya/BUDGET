using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BUDGET.Models
{
    public class ors_head_request
    {
        [Key]
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String Position { get; set; }
    }
}