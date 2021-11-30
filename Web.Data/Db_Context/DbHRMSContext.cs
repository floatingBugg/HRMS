﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Web.Data.Models;

#nullable disable

namespace Web.Data.Db_Context
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=HRMS_1;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<EmsTblAcademicQualification>(entity =>
            {
                entity.HasKey(e => e.EtaqAqId)
                    .HasName("PK__ems_tbl___128BDF78D01DF2DD");

                entity.ToTable("ems_tbl_academic_qualification");

                entity.Property(e => e.EtaqAqId).HasColumnName("etaq_aq_id");

                entity.Property(e => e.EtaqCgpa).HasColumnName("etaq_cgpa");

                entity.Property(e => e.EtaqCreatedBy).HasColumnName("etaq_created_by");

                entity.Property(e => e.EtaqCreatedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etaq_created_by_date");

                entity.Property(e => e.EtaqCreatedByName).HasColumnName("etaq_created_by_name");

                entity.Property(e => e.EtaqEtedEmployeeId).HasColumnName("etaq_eted_employee_id");

                entity.Property(e => e.EtaqInstituteName).HasColumnName("etaq_institute_name");

                entity.Property(e => e.EtaqIsDelete)
                    .IsRequired()
                    .HasColumnName("etaq_is_delete")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.EtaqModifiedBy).HasColumnName("etaq_modified_by");

                entity.Property(e => e.EtaqModifiedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etaq_modified_by_date");

                entity.Property(e => e.EtaqModifiedByName).HasColumnName("etaq_modified_by_name");

                entity.Property(e => e.EtaqPassingYear)
                    .HasColumnType("datetime")
                    .HasColumnName("etaq_passing_year");

                entity.Property(e => e.EtaqQualification).HasColumnName("etaq_qualification");

                entity.Property(e => e.EtaqUploadDocuments).HasColumnName("etaq_upload_documents");

                entity.HasOne(d => d.EtaqEtedEmployee)
                    .WithMany(p => p.EmsTblAcademicQualification)
                    .HasForeignKey(d => d.EtaqEtedEmployeeId)
                    .HasConstraintName("FK__ems_tbl_a__etaq___07C12930");
            });

            modelBuilder.Entity<EmsTblEmergencyContact>(entity =>
            {
                entity.HasKey(e => e.EtecEcId)
                    .HasName("PK__ems_tbl___BBD724E96D033A97");

                entity.ToTable("ems_tbl_emergency_contact");

                entity.Property(e => e.EtecEcId).HasColumnName("etec_ec_id");

                entity.Property(e => e.EtecAddress).HasColumnName("etec_address");

                entity.Property(e => e.EtecContactNumber).HasColumnName("etec_contact_number");

                entity.Property(e => e.EtecCreatedBy).HasColumnName("etec_created_by");

                entity.Property(e => e.EtecCreatedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etec_created_by_date");

                entity.Property(e => e.EtecCreatedByName).HasColumnName("etec_created_by_name");

                entity.Property(e => e.EtecEtedEmployeeId).HasColumnName("etec_eted_employee_id");

                entity.Property(e => e.EtecFirstName).HasColumnName("etec_first_name");

                entity.Property(e => e.EtecIsDelete)
                    .IsRequired()
                    .HasColumnName("etec_is_delete")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.EtecLastName).HasColumnName("etec_last_name");

                entity.Property(e => e.EtecModifiedBy).HasColumnName("etec_modified_by");

                entity.Property(e => e.EtecModifiedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etec_modified_by_date");

                entity.Property(e => e.EtecModifiedByName).HasColumnName("etec_modified_by_name");

                entity.Property(e => e.EtecRelation).HasColumnName("etec_relation");

                entity.HasOne(d => d.EtecEtedEmployee)
                    .WithMany(p => p.EmsTblEmergencyContact)
                    .HasForeignKey(d => d.EtecEtedEmployeeId)
                    .HasConstraintName("FK__ems_tbl_e__etec___08B54D69");
            });

            modelBuilder.Entity<EmsTblEmployeeDetails>(entity =>
            {
                entity.HasKey(e => e.EtedEmployeeId)
                    .HasName("PK__ems_tbl___516C46CD6548D87B");

                entity.ToTable("ems_tbl_employee_details");

                entity.Property(e => e.EtedEmployeeId).HasColumnName("eted_employee_id");

                entity.Property(e => e.EtedAddress)
                    .HasMaxLength(50)
                    .HasColumnName("eted_address");

                entity.Property(e => e.EtedBloodGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("eted_blood_group");

                entity.Property(e => e.EtedCnic).HasColumnName("eted_cnic");

                entity.Property(e => e.EtedContactNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("eted_contact_number");

                entity.Property(e => e.EtedCreatedBy).HasColumnName("eted_created_by");

                entity.Property(e => e.EtedCreatedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("eted_created_by_date");

                entity.Property(e => e.EtedCreatedByName).HasColumnName("eted_created_by_name");

                entity.Property(e => e.EtedDob)
                    .HasColumnType("date")
                    .HasColumnName("eted_dob");

                entity.Property(e => e.EtedEmailAddress)
                    .HasMaxLength(50)
                    .HasColumnName("eted_email_address");

                entity.Property(e => e.EtedFirstName)
                    .HasMaxLength(50)
                    .HasColumnName("eted_first_name");

                entity.Property(e => e.EtedGender)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("eted_gender");

                entity.Property(e => e.EtedIsDelete)
                    .IsRequired()
                    .HasColumnName("eted_is_delete")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.EtedLastName)
                    .HasMaxLength(50)
                    .HasColumnName("eted_last_name");

                entity.Property(e => e.EtedMaritalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("eted_marital_status");

                entity.Property(e => e.EtedModifiedBy).HasColumnName("eted_modified_by");

                entity.Property(e => e.EtedModifiedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("eted_modified_by_date");

                entity.Property(e => e.EtedModifiedByName).HasColumnName("eted_modified_by_name");

                entity.Property(e => e.EtedNationality).HasColumnName("eted_nationality");

                entity.Property(e => e.EtedOfficialEmailAddress).HasColumnName("eted_official_email_address");

                entity.Property(e => e.EtedPhotograph).HasColumnName("eted_photograph");

                entity.Property(e => e.EtedReligion).HasColumnName("eted_religion");

                entity.Property(e => e.EtedStatus)
                    .IsRequired()
                    .HasColumnName("eted_status");
            });

            modelBuilder.Entity<EmsTblEmployeeProfessionalDetails>(entity =>
            {
                entity.HasKey(e => e.EtepdPdId)
                    .HasName("PK__ems_tbl___DBCDA814ABEF8833");

                entity.ToTable("ems_tbl_employee_professional_details");

                entity.Property(e => e.EtepdPdId).HasColumnName("etepd_pd_id");

                entity.Property(e => e.EtepdCreatedBy).HasColumnName("etepd_created_by");

                entity.Property(e => e.EtepdCreatedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etepd_created_by_date");

                entity.Property(e => e.EtepdCreatedByName).HasColumnName("etepd_created_by_name");

                entity.Property(e => e.EtepdDesignation).HasColumnName("etepd_designation");

                entity.Property(e => e.EtepdEtedEmployeeId).HasColumnName("etepd_eted_employee_id");

                entity.Property(e => e.EtepdIsDelete)
                    .IsRequired()
                    .HasColumnName("etepd_is_delete")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.EtepdJoiningDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etepd_joining_date");

                entity.Property(e => e.EtepdModifiedBy).HasColumnName("etepd_modified_by");

                entity.Property(e => e.EtepdModifiedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etepd_modified_by_date");

                entity.Property(e => e.EtepdModifiedByName).HasColumnName("etepd_modified_by_name");

                entity.Property(e => e.EtepdProbation).HasColumnName("etepd_probation");

                entity.Property(e => e.EtepdSalary).HasColumnName("etepd_salary");

                entity.HasOne(d => d.EtepdEtedEmployee)
                    .WithMany(p => p.EmsTblEmployeeProfessionalDetails)
                    .HasForeignKey(d => d.EtepdEtedEmployeeId)
                    .HasConstraintName("FK__ems_tbl_e__etepd__09A971A2");
            });

            modelBuilder.Entity<EmsTblHrmsUser>(entity =>
            {
                entity.HasKey(e => e.EthuUserId)
                    .HasName("PK__ems_tbl___7F2D16ACA449ABFB");

                entity.ToTable("ems_tbl_hrms_user");

                entity.Property(e => e.EthuUserId).HasColumnName("ethu_user_id");

                entity.Property(e => e.EthuCreatedBy).HasColumnName("ethu_created_by");

                entity.Property(e => e.EthuCreatedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ethu_created_by_date");

                entity.Property(e => e.EthuCreatedByName).HasColumnName("ethu_created_by_name");

                entity.Property(e => e.EthuEmailAddress).HasColumnName("ethu_email_address");

                entity.Property(e => e.EthuFullName).HasColumnName("ethu_full_name");

                entity.Property(e => e.EthuGender).HasColumnName("ethu_gender");

                entity.Property(e => e.EthuIsDelete)
                    .IsRequired()
                    .HasColumnName("ethu_is_delete")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.EthuModifiedBy).HasColumnName("ethu_modified_by");

                entity.Property(e => e.EthuModifiedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ethu_modified_by_date");

                entity.Property(e => e.EthuModifiedByName).HasColumnName("ethu_modified_by_name");

                entity.Property(e => e.EthuPassword).HasColumnName("ethu_password");

                entity.Property(e => e.EthuPhoneNumber).HasColumnName("ethu_phone_number");

                entity.Property(e => e.EthuUserName).HasColumnName("ethu_user_name");
            });

            modelBuilder.Entity<EmsTblProfessionalQualification>(entity =>
            {
                entity.HasKey(e => e.EtpqPqId)
                    .HasName("PK__ems_tbl___E07F4F7201E6B519");

                entity.ToTable("ems_tbl_professional_qualification");

                entity.Property(e => e.EtpqPqId).HasColumnName("etpq_pq_id");

                entity.Property(e => e.EtpqCertification).HasColumnName("etpq_certification");

                entity.Property(e => e.EtpqCreatedBy).HasColumnName("etpq_created_by");

                entity.Property(e => e.EtpqCreatedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etpq_created_by_date");

                entity.Property(e => e.EtpqCreatedByName).HasColumnName("etpq_created_by_name");

                entity.Property(e => e.EtpqDocuments).HasColumnName("etpq_documents");

                entity.Property(e => e.EtpqEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etpq_end_date");

                entity.Property(e => e.EtpqEtedEmployeeId).HasColumnName("etpq_eted_employee_id");

                entity.Property(e => e.EtpqInstituteName).HasColumnName("etpq_institute_name");

                entity.Property(e => e.EtpqIsDelete)
                    .IsRequired()
                    .HasColumnName("etpq_is_delete")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.EtpqModifiedBy).HasColumnName("etpq_modified_by");

                entity.Property(e => e.EtpqModifiedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etpq_modified_by_date");

                entity.Property(e => e.EtpqModifiedByName).HasColumnName("etpq_modified_by_name");

                entity.Property(e => e.EtpqStratDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etpq_strat_date");

                entity.HasOne(d => d.EtpqEtedEmployee)
                    .WithMany(p => p.EmsTblProfessionalQualification)
                    .HasForeignKey(d => d.EtpqEtedEmployeeId)
                    .HasConstraintName("FK__ems_tbl_p__etpq___0A9D95DB");
            });

            modelBuilder.Entity<EmsTblWorkingHistory>(entity =>
            {
                entity.HasKey(e => e.EtwhWhId)
                    .HasName("PK__ems_tbl___EE14BDFFF325C08A");

                entity.ToTable("ems_tbl_working_history");

                entity.Property(e => e.EtwhWhId).HasColumnName("etwh_wh_id");

                entity.Property(e => e.EtwhCompanyName).HasColumnName("etwh_company_name");

                entity.Property(e => e.EtwhCreatedBy).HasColumnName("etwh_created_by");

                entity.Property(e => e.EtwhCreatedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etwh_created_by_date");

                entity.Property(e => e.EtwhCreatedByName).HasColumnName("etwh_created_by_name");

                entity.Property(e => e.EtwhDesignation).HasColumnName("etwh_designation");

                entity.Property(e => e.EtwhDuration).HasColumnName("etwh_duration");

                entity.Property(e => e.EtwhEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etwh_end_date");

                entity.Property(e => e.EtwhEtedEmployeeId).HasColumnName("etwh_eted_employee_id");

                entity.Property(e => e.EtwhExperienceLetter).HasColumnName("etwh_experience_letter");

                entity.Property(e => e.EtwhIsDelete)
                    .IsRequired()
                    .HasColumnName("etwh_is_delete")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.EtwhModifiedBy).HasColumnName("etwh_modified_by");

                entity.Property(e => e.EtwhModifiedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etwh_modified_by_date");

                entity.Property(e => e.EtwhModifiedByName).HasColumnName("etwh_modified_by_name");

                entity.Property(e => e.EtwhStratDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etwh_strat_date");

                entity.HasOne(d => d.EtwhEtedEmployee)
                    .WithMany(p => p.EmsTblWorkingHistory)
                    .HasForeignKey(d => d.EtwhEtedEmployeeId)
                    .HasConstraintName("FK__ems_tbl_w__etwh___0B91BA14");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}