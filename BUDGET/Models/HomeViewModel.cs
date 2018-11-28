using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BUDGET
{
    public class HomeViewModel
    {
        public IEnumerable<Notifications> notifications { get; set; }
        public IEnumerable<Allotments> allotments { get; set; }
    }
}