﻿// <auto-generated />
using Budgeteer.Plugins.EFDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Budgeteer.Plugins.EFDB.Migrations
{
    [DbContext(typeof(ExpenseContext))]
    [Migration("20230127141338_QueryTrackingDisables")]
    partial class QueryTrackingDisables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("Budgeteer.Plugins.EFDB.ExpenseEntry", b =>
                {
                    b.Property<int>("EntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cost")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExpenseName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Interval")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("EntryId");

                    b.HasIndex("UserID");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("Budgeteer.Plugins.EFDB.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Budgeteer.Plugins.EFDB.UserSalt", b =>
                {
                    b.Property<int>("SaltId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("SaltId");

                    b.HasIndex("UserID");

                    b.ToTable("Salts");
                });

            modelBuilder.Entity("Budgeteer.Plugins.EFDB.ExpenseEntry", b =>
                {
                    b.HasOne("Budgeteer.Plugins.EFDB.User", "User")
                        .WithMany("Entries")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Budgeteer.Plugins.EFDB.UserSalt", b =>
                {
                    b.HasOne("Budgeteer.Plugins.EFDB.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Budgeteer.Plugins.EFDB.User", b =>
                {
                    b.Navigation("Entries");
                });
#pragma warning restore 612, 618
        }
    }
}
