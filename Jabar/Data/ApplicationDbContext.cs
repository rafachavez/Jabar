using System;
using System.Collections.Generic;
using System.Text;
using Jabar.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jabar.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<AssemblyHistory> AssemblyHistories { get; set; }
        public DbSet<AssemblyRecipe> AssemblyRecipes { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<RecievedItems> RecievedItems { get; set; }
        public DbSet<RecipeLine> RecipeLines { get; set; }
        public DbSet<Vendor> Vendors { get; set; }   
        public DbSet<ApplicationUser> ApplicationUser { get; set; }


    }
}
