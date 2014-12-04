using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EASNSMVC4.Models;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace EASNSMVC4.DAL
{
    public class DBEntity : DbContext
    {
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<Stakeholder> Stakeholders { get; set; }
        public DbSet<ActiveState> ActiveStates { get; set; }
        public DbSet<InventoryType> InventoryTypes { get; set; }
        public DbSet<InventoryResource> InventoryResources { get; set; }
        public DbSet<Peripheral> Peripherals { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<TechLevel> TechLevels { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        public override int SaveChanges()
        {
           string auditUser = "Anonymous";
            /*if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                auditUser = HttpContext.Current.User.Identity.Name;
            }*/

            DateTime auditDate = DateTime.Now;
            foreach(DbEntityEntry<IAuditable> entry in ChangeTracker.Entries<IAuditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = auditDate;
                    entry.Entity.CreatedBy = auditUser;
                    entry.Entity.UpdatedOn = auditDate;
                    entry.Entity.UpdatedBy = auditUser;
                }
            }

           return base.SaveChanges();
        }
    }
    
}