﻿// <auto-generated />
using System;
using Jabar.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jabar.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190730194854_makingvendorvirtualinitem")]
    partial class makingvendorvirtualinitem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Jabar.Models.AssemblyHistory", b =>
                {
                    b.Property<int>("AssemblyHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AssemblyDate");

                    b.Property<int>("ItemId");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Notes");

                    b.Property<int>("QtyAssembled");

                    b.HasKey("AssemblyHistoryId");

                    b.HasIndex("ItemId");

                    b.ToTable("AssemblyHistories");
                });

            modelBuilder.Entity("Jabar.Models.AssemblyRecipe", b =>
                {
                    b.Property<int>("AssemblyRecipeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemId");

                    b.HasKey("AssemblyRecipeId");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.ToTable("AssemblyRecipes");
                });

            modelBuilder.Entity("Jabar.Models.InventoryLog", b =>
                {
                    b.Property<int>("InventoryLogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemId");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<int>("NewQty");

                    b.Property<int>("PreviousQty");

                    b.Property<bool>("Reconciled");

                    b.HasKey("InventoryLogId");

                    b.HasIndex("ItemId");

                    b.ToTable("InventoryLogs");
                });

            modelBuilder.Entity("Jabar.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssemblyRecipeId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsAssembled");

                    b.Property<string>("ItemName");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<double>("ListRetailCost");

                    b.Property<int>("MaxQty");

                    b.Property<double>("MeasureAmnt");

                    b.Property<int>("MeasureID");

                    b.Property<int>("OnHandQty");

                    b.Property<int>("ReorderQty");

                    b.Property<int>("VendorId");

                    b.HasKey("ItemId");

                    b.HasIndex("VendorId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Jabar.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateDelivered");

                    b.Property<int>("ItemId");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<double>("Price");

                    b.Property<int>("PurchaseOrderId");

                    b.Property<int>("QuantityOrdered");

                    b.Property<string>("VendorSKU");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Jabar.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("PurchaseOrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOrdered");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<int>("VendorId");

                    b.Property<string>("VendorPO");

                    b.HasKey("PurchaseOrderId");

                    b.HasIndex("VendorId");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("Jabar.Models.RecievedItems", b =>
                {
                    b.Property<int>("RecievedItemsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Notes");

                    b.Property<int>("OrderItemId");

                    b.Property<int>("QuantityRecieved");

                    b.HasKey("RecievedItemsId");

                    b.HasIndex("OrderItemId");

                    b.ToTable("RecievedItems");
                });

            modelBuilder.Entity("Jabar.Models.RecipeLine", b =>
                {
                    b.Property<int>("RecipeLineId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssemblyRecipeId");

                    b.Property<int>("ItemId");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<int>("RequiredItemQty");

                    b.HasKey("RecipeLineId");

                    b.HasIndex("AssemblyRecipeId");

                    b.HasIndex("ItemId");

                    b.ToTable("RecipeLines");
                });

            modelBuilder.Entity("Jabar.Models.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("VendorAddress");

                    b.Property<string>("VendorName");

                    b.HasKey("VendorId");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Jabar.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("name");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Jabar.Models.AssemblyHistory", b =>
                {
                    b.HasOne("Jabar.Models.Item")
                        .WithMany("AssemblyHistories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jabar.Models.AssemblyRecipe", b =>
                {
                    b.HasOne("Jabar.Models.Item", "Item")
                        .WithOne("AssemblyRecipe")
                        .HasForeignKey("Jabar.Models.AssemblyRecipe", "ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jabar.Models.InventoryLog", b =>
                {
                    b.HasOne("Jabar.Models.Item")
                        .WithMany("InventoryLogs")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jabar.Models.Item", b =>
                {
                    b.HasOne("Jabar.Models.Vendor", "PreferredVendor")
                        .WithMany()
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jabar.Models.OrderItem", b =>
                {
                    b.HasOne("Jabar.Models.Item")
                        .WithMany("OrderItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Jabar.Models.PurchaseOrder")
                        .WithMany("OrderItems")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jabar.Models.PurchaseOrder", b =>
                {
                    b.HasOne("Jabar.Models.Vendor")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jabar.Models.RecievedItems", b =>
                {
                    b.HasOne("Jabar.Models.OrderItem")
                        .WithMany("RecievedItems")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jabar.Models.RecipeLine", b =>
                {
                    b.HasOne("Jabar.Models.AssemblyRecipe", "AssemblyRecipe")
                        .WithMany("RecipeLines")
                        .HasForeignKey("AssemblyRecipeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Jabar.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
