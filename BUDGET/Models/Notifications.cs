using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BUDGET
{
    public class Notifications
    {
        public Int32 ID { get; set; }
        public String Module { get; set; }
        public String Action { get; set; }
        public String User { get; set; }
        public String Message { get; set; }
        public DateTime DateAdded { get; set; }
        public String Year { get; set; }
    }
}