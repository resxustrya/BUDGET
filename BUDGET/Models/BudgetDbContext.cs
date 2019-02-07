using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace BUDGET
{
    public class BudgetDB : DbContext
    {
        public BudgetDB() : base("name=BudgetDB")
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<PersonnelServices> ps { get; set; }
        public DbSet<UACS> uacs { get; set; }
        public DbSet<YearBudget> yearbudget { get; set; }
        public DbSet<PREXC> prexc { get; set; }
        public DbSet<Allotments> allotments { get; set; }
        public DbSet<ORS> ors { get; set; }
        public DbSet<FundSourceHdr> fsh { get; set; }
        public DbSet<FundSourceAmount> fsa { get; set; }
        public DbSet<ORS_EXPENSE_CODES> ors_expense_codes { get; set; }
        public DbSet<ors_head_request> ors_head_request { get; set; }
        public DbSet<Realignment> realignment { get; set; }
        public DbSet<Notifications> notifications { get; set; }
        public DbSet<OrsDateEntry> ors_date_entry { get; set; }
        public void AddNotifications(String action, String module,String user,String year, DateTime DateAdded, String status)
        {
            Notifications n = new Notifications();
            n.Action = action;
            n.Module = module;
            n.User = user;
            n.Year = year;
            n.DateAdded = DateAdded;
            n.status = status;
            this.notifications.Add(n);
            this.SaveChanges();
            
        }

    }
}