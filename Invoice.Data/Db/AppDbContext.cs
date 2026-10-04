using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Invoice.Data.Entities;

namespace Invoice.Data.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ItemmasterEntity> Itemmasters { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<VendorEntity> Vendors { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<UsersEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
