using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BUDGET
{
    public class ProductListViewModel
    {
        public IEnumerable<User> userslist { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}