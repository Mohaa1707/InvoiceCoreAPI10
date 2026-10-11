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
        public DbSet<PurchaseOrderEntity> PurchaseOrders => Set<PurchaseOrderEntity>();
        public DbSet<PurchaseOrderDetailEntity> PurchaseOrderDetails => Set<PurchaseOrderDetailEntity>();
        public DbSet<ReceiptEntity> Receipts { get; set; }
        public DbSet<ReceiptDetailEntity> ReceiptDetails { get; set; }
        public DbSet<SalesInvoiceEntity> SalesInvoices { get; set; }
        public DbSet<SalesInvoiceDetailEntity> SalesInvoiceDetails { get; set; }
        public DbSet<ItemStockEntity> ItemStocks { get; set; }

        public DbSet<CategoryEntity> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PurchaseOrderDetailEntity>()
            .HasOne<PurchaseOrderEntity>()
            .WithMany(p => p.Details)
            .HasForeignKey(d => d.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
