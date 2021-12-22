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

        public virtual DbSet<EmsTblAcademicQualificationVM> EmsTblAcademicQualification { get; set; }
        public virtual DbSet<EmsTblEmergencyContactVM> EmsTblEmergencyContact { get; set; }
        public virtual DbSet<EmsTblEmployeeDetailsVM> EmsTblEmployeeDetails { get; set; }
        public virtual DbSet<EmsTblEmployeeProfessionalDetailsVM> EmsTblEmployeeProfessionalDetails { get; set; }
        public virtual DbSet<EmsTblHrmsUserVM> EmsTblHrmsUser { get; set; }
        public virtual DbSet<EmsTblProfessionalQualificationVM> EmsTblProfessionalQualification { get; set; }
        public virtual DbSet<EmsTblWorkingHistoryVM> EmsTblWorkingHistory { get; set; }
        public virtual DbSet<ImsAssetsVM> ImsAssets { get; set; }
        public virtual DbSet<ImsAssetsCategoryVM> ImsAssetsCategory { get; set; }
        public virtual DbSet<ImsAssignVM> ImsAssign { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SQL5105.site4now.net;Initial Catalog=db_a7d47e_hrms;Persist Security Info=True;User ID=db_a7d47e_hrms_admin;Password=123hamza");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<EmsTblAcademicQualificationVM>(entity =>
            {
                entity.HasKey(e => e.EtaqAqId)
                    .HasName("PK__ems_tbl___128BDF78D5779BCF");

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
                    .HasColumnName("etaq_is_delete")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.EtaqModifiedBy).HasColumnName("etaq_modified_by");

                entity.Property(e => e.EtaqModifiedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("etaq_modified_by_date");

                entity.Property(e => e.EtaqModifiedByName).HasColumnName("etaq_modified_by_name");

                entity.Property(e => e.EtaqPassingYear).HasColumnName("etaq_passing_year");

                entity.Property(e => e.EtaqQualification).HasColumnName("etaq_qualification");

                entity.Property(e => e.EtaqUploadDocuments).HasColumnName("etaq_upload_documents");

                entity.HasOne(d => d.EtaqEtedEmployee)
                    .WithMany(p => p.EmsTblAcademicQualification)
                    .HasForeignKey(d => d.EtaqEtedEmployeeId)
                    .HasConstraintName("FK__ems_tbl_a__etaq___2DE6D218");
            });

            modelBuilder.Entity<EmsTblEmergencyContactVM>(entity =>
            {
                entity.HasKey(e => e.EtecEcId)
                    .HasName("PK__ems_tbl___BBD724E953E48F71");

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
                    .HasConstraintName("FK__ems_tbl_e__etec___2EDAF651");
            });

            modelBuilder.Entity<EmsTblEmployeeDetailsVM>(entity =>
            {
                entity.HasKey(e => e.EtedEmployeeId)
                    .HasName("PK__ems_tbl___516C46CDF5E224DC");

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
                    .HasColumnType("datetime")
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

                entity.Property(e => e.EtedStatus).HasColumnName("eted_status");
            });

            modelBuilder.Entity<EmsTblEmployeeProfessionalDetailsVM>(entity =>
            {
                entity.HasKey(e => e.EtepdPdId)
                    .HasName("PK__ems_tbl___DBCDA814B7C6BA80");

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
                    .HasConstraintName("FK__ems_tbl_e__etepd__2FCF1A8A");
            });

            modelBuilder.Entity<EmsTblHrmsUserVM>(entity =>
            {
                entity.HasKey(e => e.EthuUserId)
                    .HasName("PK__ems_tbl___7F2D16AC75DE39F2");

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

            modelBuilder.Entity<EmsTblProfessionalQualificationVM>(entity =>
            {
                entity.HasKey(e => e.EtpqPqId)
                    .HasName("PK__ems_tbl___E07F4F72941A86C1");

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
                    .HasConstraintName("FK__ems_tbl_p__etpq___30C33EC3");
            });

            modelBuilder.Entity<EmsTblWorkingHistoryVM>(entity =>
            {
                entity.HasKey(e => e.EtwhWhId)
                    .HasName("PK__ems_tbl___EE14BDFF5C21C3CE");

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
                    .HasConstraintName("FK__ems_tbl_w__etwh___31B762FC");
            });

            modelBuilder.Entity<ImsAssetsVM>(entity =>
            {
                entity.HasKey(e => e.ItaAssetId)
                    .HasName("PK__ims_asse__B51DD0C3668BD2FE");

                entity.ToTable("ims_assets");

                entity.Property(e => e.ItaAssetId).HasColumnName("ita_asset_id");

                entity.Property(e => e.ItaAssetName).HasColumnName("ita_asset_name");

                entity.Property(e => e.ItaAssignQuantity).HasColumnName("ita_assign_quantity");

                entity.Property(e => e.ItaCompanyName).HasColumnName("ita_company_name");

                entity.Property(e => e.ItaCondition).HasColumnName("ita_condition");

                entity.Property(e => e.ItaCost).HasColumnName("ita_cost");

                entity.Property(e => e.ItaCreatedBy).HasColumnName("ita_created_by");

                entity.Property(e => e.ItaCreatedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ita_created_by_date");

                entity.Property(e => e.ItaCreatedByName).HasColumnName("ita_created_by_name");

                entity.Property(e => e.ItaGeneration).HasColumnName("ita_generation");

                entity.Property(e => e.ItaHardriveType).HasColumnName("ita_Hardrive_type");

                entity.Property(e => e.ItaIsDelete).HasColumnName("ita_is_delete");

                entity.Property(e => e.ItaModel).HasColumnName("ita_model");

                entity.Property(e => e.ItaModifiedBy).HasColumnName("ita_modified_by");

                entity.Property(e => e.ItaModifiedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ita_modified_by_date");

                entity.Property(e => e.ItaModifiedByName).HasColumnName("ita_modified_by_name");

                entity.Property(e => e.ItaProcessor).HasColumnName("ita_processor");

                entity.Property(e => e.ItaPurchaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ita_purchase_date");

                entity.Property(e => e.ItaQuantity).HasColumnName("ita_quantity");

                entity.Property(e => e.ItaRam).HasColumnName("ita_ram");

                entity.Property(e => e.ItaRemaining).HasColumnName("ita_remaining");

                entity.Property(e => e.ItaSerialNo).HasColumnName("ita_serial_no");

                entity.Property(e => e.ItaSize).HasColumnName("ita_size");

                entity.Property(e => e.ItaStorage).HasColumnName("ita_storage");

                entity.Property(e => e.ItaType).HasColumnName("ita_type");

                entity.Property(e => e.ItacCategoryId).HasColumnName("itac_category_id");

                entity.HasOne(d => d.ItacCategory)
                    .WithMany(p => p.ImsAssets)
                    .HasForeignKey(d => d.ItacCategoryId)
                    .HasConstraintName("FK__ims_asset__itac___1F2E9E6D");
            });

            modelBuilder.Entity<ImsAssetsCategoryVM>(entity =>
            {
                entity.HasKey(e => e.ItacCategoryId)
                    .HasName("PK__ims_asse__30819A4323C3B1C9");

                entity.ToTable("ims_assets_category");

                entity.Property(e => e.ItacCategoryId).HasColumnName("itac_category_id");

                entity.Property(e => e.ItacCategoryName).HasColumnName("itac_category_name");

                entity.Property(e => e.ItacCreatedBy).HasColumnName("itac_created_by");

                entity.Property(e => e.ItacCreatedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("itac_created_by_date");

                entity.Property(e => e.ItacCreatedByName).HasColumnName("itac_created_by_name");

                entity.Property(e => e.ItacIsDelete).HasColumnName("itac_is_delete");

                entity.Property(e => e.ItacModifiedBy).HasColumnName("itac_modified_by");

                entity.Property(e => e.ItacModifiedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("itac_modified_by_date");

                entity.Property(e => e.ItacModifiedByName).HasColumnName("itac_modified_by_name");
            });

            modelBuilder.Entity<ImsAssignVM>(entity =>
            {
                entity.HasKey(e => e.ItasAssignId)
                    .HasName("PK__ims_assi__9BC07BF33898EEFF");

                entity.ToTable("ims_assign");

                entity.Property(e => e.ItasAssignId).HasColumnName("itas_assign_id");

                entity.Property(e => e.ItasAssignedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("itas_assigned_date");

                entity.Property(e => e.ItasCreatedBy).HasColumnName("itas_created_by");

                entity.Property(e => e.ItasCreatedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("itas_created_by_date");

                entity.Property(e => e.ItasCreatedByName).HasColumnName("itas_created_by_name");

                entity.Property(e => e.ItasEtedEmployeeId).HasColumnName("itas_eted_employee_id");

                entity.Property(e => e.ItasIsDelete).HasColumnName("itas_is_delete");

                entity.Property(e => e.ItasItaAssetId).HasColumnName("itas_ita_asset_id");

                entity.Property(e => e.ItasItacCategoryId).HasColumnName("itas_itac_category_id");

                entity.Property(e => e.ItasModifiedBy).HasColumnName("itas_modified_by");

                entity.Property(e => e.ItasModifiedByDate)
                    .HasColumnType("datetime")
                    .HasColumnName("itas_modified_by_date");

                entity.Property(e => e.ItasModifiedByName).HasColumnName("itas_modified_by_name");

                entity.Property(e => e.ItasQuantity).HasColumnName("itas_quantity");

                entity.HasOne(d => d.ItasEtedEmployee)
                    .WithMany(p => p.ImsAssign)
                    .HasForeignKey(d => d.ItasEtedEmployeeId)
                    .HasConstraintName("FK__ims_assig__itas___22FF2F51");

                entity.HasOne(d => d.ItasItaAsset)
                    .WithMany(p => p.ImsAssign)
                    .HasForeignKey(d => d.ItasItaAssetId)
                    .HasConstraintName("FK__ims_assig__itas___220B0B18");

                entity.HasOne(d => d.ItasItacCategory)
                    .WithMany(p => p.ImsAssign)
                    .HasForeignKey(d => d.ItasItacCategoryId)
                    .HasConstraintName("FK__ims_assig__itas___23F3538A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}