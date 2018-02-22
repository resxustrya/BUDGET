using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace BUDGET.Models
{
    public class BudgetDB : DbContext
    {
        public BudgetDB() : base("name=BudgetDB")
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<PersonnelServices> ps { get; set; }
        public DbSet<MOOEs> mooe { get; set; }

    }
}