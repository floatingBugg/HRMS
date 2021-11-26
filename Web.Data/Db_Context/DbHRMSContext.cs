﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Web.DLL.Models;

#nullable disable

namespace Web.DLL.Db_Context
{
    public partial class DbHRMSContext : DbContext
    {
        public DbHRMSContext()
        {
        }

        public DbHRMSContext(DbContextOptions<DbHRMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmsTblAcademicQualification> EmsTblAcademicQualification { get; set; }
        public virtual DbSet<EmsTblEmergencyContact> EmsTblEmergencyContact { get; set; }
        public virtual DbSet<EmsTblEmployeeDetails> EmsTblEmployeeDetails { get; set; }
        public virtual DbSet<EmsTblEmployeeProfessionalDetails> EmsTblEmployeeProfessionalDetails { get; set; }
        public virtual DbSet<EmsTblHrmsUser> EmsTblHrmsUser { get; set; }
        public virtual DbSet<EmsTblProfessionalQualification> EmsTblProfessionalQualification { get; set; }
        public virtual DbSet<EmsTblWorkingHistory> EmsTblWorkingHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                /*optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EMS;Integrated Security=True");*/

                optionsBuilder.UseSqlServer("Data Source=SQL5102.site4now.net,1433;Initial Catalog=db_a7cdcd_emsdatabase;User Id=db_a7cdcd_emsdatabase_admin;Password=hamza123; ");

                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-L6KGPJ5MSSQLSERVER01; Initial Catalog = EMS; Integrated Security = True");


            

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<EmsTblAcademicQualification>(entity =>
            {
                entity.HasKey(e => e.EtaqAqId)
                    .HasName("PK__ems_tbl___128BDF782D6D082B");

                entity.HasOne(d => d.EtedEmployee)
                    .WithMany(p => p.EmsTblAcademicQualification)
                    .HasForeignKey(d => d.EtedEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ems_tbl_a__eted___03F0984C");
            });

            modelBuilder.Entity<EmsTblEmergencyContact>(entity =>
            {
                entity.HasKey(e => e.EtecEcId)
                    .HasName("PK__ems_tbl___BBD724E9A7C366B2");

                entity.HasOne(d => d.EtedEmployee)
                    .WithMany(p => p.EmsTblEmergencyContact)
                    .HasForeignKey(d => d.EtedEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ems_tbl_e__eted___04E4BC85");
            });

            modelBuilder.Entity<EmsTblEmployeeDetails>(entity =>
            {
                entity.Property(e => e.EtedBloodGroup).IsUnicode(false);

                entity.Property(e => e.EtedContactNumber).IsUnicode(false);

                entity.Property(e => e.EtedGender).IsUnicode(false);

                entity.Property(e => e.EtedMaritalStatus).IsUnicode(false);
            });

            modelBuilder.Entity<EmsTblEmployeeProfessionalDetails>(entity =>
            {
                entity.HasKey(e => e.EtepdPdId)
                    .HasName("PK__ems_tbl___DBCDA814E006BE80");

                entity.HasOne(d => d.EtedEmployee)
                    .WithMany(p => p.EmsTblEmployeeProfessionalDetails)
                    .HasForeignKey(d => d.EtedEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ems_tbl_e__eted___05D8E0BE");
            });

            modelBuilder.Entity<EmsTblHrmsUser>(entity =>
            {
                entity.HasKey(e => e.EthuUserId)
                    .HasName("PK__ems_tbl___7F2D16AC2966A7C8");
            });

            modelBuilder.Entity<EmsTblProfessionalQualification>(entity =>
            {
                entity.HasKey(e => e.EtpqPqId)
                    .HasName("PK__ems_tbl___E07F4F725E4154DB");

                entity.HasOne(d => d.EtedEmployee)
                    .WithMany(p => p.EmsTblProfessionalQualification)
                    .HasForeignKey(d => d.EtedEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ems_tbl_p__eted___06CD04F7");
            });

            modelBuilder.Entity<EmsTblWorkingHistory>(entity =>
            {
                entity.HasKey(e => e.EtwhWhId)
                    .HasName("PK__ems_tbl___EE14BDFFDC39157E");

                entity.HasOne(d => d.EtedEmployee)
                    .WithMany(p => p.EmsTblWorkingHistory)
                    .HasForeignKey(d => d.EtedEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ems_tbl_w__eted___07C12930");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}