using System;
using System.Collections.Generic;
using GRCServices.Models;
using GRCServices.Models;
using Microsoft.EntityFrameworkCore;

namespace GRCServices.Data;

public partial class GRCDbContext : DbContext
{
    public GRCDbContext()
    {
    }

    public GRCDbContext(DbContextOptions<GRCDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivitiesMaster> ActivitiesMasters { get; set; }

    public virtual DbSet<ActivityMaster> ActivityMasters { get; set; }

    public virtual DbSet<AssignmentMaster> AssignmentMasters { get; set; }

    public virtual DbSet<CategoryListMaster> CategoryListMasters { get; set; }

    public virtual DbSet<CategoryMaster> CategoryMasters { get; set; }

    public virtual DbSet<ClientRoleMaster> ClientRoleMasters { get; set; }

    public virtual DbSet<ClientUserInfo> ClientUserInfos { get; set; }

    public virtual DbSet<DocumentMaster> DocumentMasters { get; set; }

    public virtual DbSet<DomainMaster> DomainMasters { get; set; }

    public virtual DbSet<EntitlementMaster> EntitlementMasters { get; set; }

    public virtual DbSet<FrequencyMaster> FrequencyMasters { get; set; }

    public virtual DbSet<GovernanceMaster> GovernanceMasters { get; set; }

    public virtual DbSet<ProcessMaster> ProcessMasters { get; set; }

    public virtual DbSet<RoleType> RoleTypes { get; set; }

    public virtual DbSet<StandardMaster> StandardMasters { get; set; }

    public virtual DbSet<StatusMaster> StatusMasters { get; set; }

    public virtual DbSet<SysCustomerInfo> SysCustomerInfos { get; set; }

    public virtual DbSet<SysLicenseMaster> SysLicenseMasters { get; set; }

    public virtual DbSet<SysRoleMaster> SysRoleMasters { get; set; }

    public virtual DbSet<SysUserLogin> SysUserLogins { get; set; }

    public virtual DbSet<TechnologiesMaster> TechnologiesMasters { get; set; }

    public virtual DbSet<UserActivityEmail> UserActivityEmails { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Server=192.168.29.128;Port=5432;Database=grc_master;Username=GRC;Password=Welcome@0668;Include Error Detail=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivitiesMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ACTIVITIES_MASTER_pkey");

            entity.ToTable("ACTIVITIES_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActivityName)
                .HasColumnType("character varying")
                .HasColumnName("ACTIVITY_NAME");
        });

        modelBuilder.Entity<ActivityMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ActivityId_PK");

            entity.ToTable("ACTIVITY_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active)
                .HasColumnType("char")
                .HasColumnName("ACTIVE");
            entity.Property(e => e.ActivityDescr).HasColumnName("ACTIVITY_DESCR");
            entity.Property(e => e.ActivityName)
                .HasColumnType("character varying")
                .HasColumnName("ACTIVITY_NAME");
            entity.Property(e => e.ApproverRole).HasColumnName("APPROVER_ROLE");
            entity.Property(e => e.Auditable)
                .HasColumnType("char")
                .HasColumnName("AUDITABLE");
            entity.Property(e => e.DoerRole).HasColumnName("DOER_ROLE");
            entity.Property(e => e.Duration).HasColumnName("DURATION");
            entity.Property(e => e.Frequency).HasColumnName("FREQUENCY");
            entity.Property(e => e.HelpRef).HasColumnName("HELP_REF");
            entity.Property(e => e.OutputDocument).HasColumnName("OUTPUT_DOCUMENT");
            entity.Property(e => e.PracticeId).HasColumnName("PRACTICE_ID");
            entity.Property(e => e.RefDocument).HasColumnName("REF_DOCUMENT");
            entity.Property(e => e.TriggeringActivity).HasColumnName("TRIGGERING_ACTIVITY");

            entity.HasOne(d => d.ApproverRoleNavigation).WithMany(p => p.ActivityMasterApproverRoleNavigations)
                .HasForeignKey(d => d.ApproverRole)
                .HasConstraintName("APPROVER_ROLE_FK");

            entity.HasOne(d => d.DoerRoleNavigation).WithMany(p => p.ActivityMasterDoerRoleNavigations)
                .HasForeignKey(d => d.DoerRole)
                .HasConstraintName("DOER_ROLE_FK");

            entity.HasOne(d => d.FrequencyNavigation).WithMany(p => p.ActivityMasters)
                .HasForeignKey(d => d.Frequency)
                .HasConstraintName("FREQUENCY_FK");

            entity.HasOne(d => d.OutputDocumentNavigation).WithMany(p => p.ActivityMasterOutputDocumentNavigations)
                .HasForeignKey(d => d.OutputDocument)
                .HasConstraintName("OUTPUT_DOCUMENT_FK");

            entity.HasOne(d => d.RefDocumentNavigation).WithMany(p => p.ActivityMasterRefDocumentNavigations)
                .HasForeignKey(d => d.RefDocument)
                .HasConstraintName("REF_DOCUMENT_FK");

            entity.HasOne(d => d.TriggeringActivityNavigation).WithMany(p => p.ActivityMasters)
                .HasForeignKey(d => d.TriggeringActivity)
                .HasConstraintName("TRIGGERING_ACTIVITY_FK");
        });

        modelBuilder.Entity<AssignmentMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Assignment_Id_PK");

            entity.ToTable("ASSIGNMENT_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActivityId).HasColumnName("ACTIVITY_ID");
            entity.Property(e => e.ApprovalDate).HasColumnName("APPROVAL_DATE");
            entity.Property(e => e.ApprovalStatus)
                .HasColumnType("char")
                .HasColumnName("APPROVAL_STATUS");
            entity.Property(e => e.ApproverComments).HasColumnName("APPROVER_COMMENTS");
            entity.Property(e => e.ApproverId).HasColumnName("APPROVER_ID");
            entity.Property(e => e.AuditCheck)
                .HasColumnType("char")
                .HasColumnName("AUDIT_CHECK");
            entity.Property(e => e.DoerComments).HasColumnName("DOER_COMMENTS");
            entity.Property(e => e.Doerstatus)
                .HasColumnType("character varying")
                .HasColumnName("DOERSTATUS");
            entity.Property(e => e.EndDate).HasColumnName("END_DATE");
            entity.Property(e => e.EvidenceDetails).HasColumnName("EVIDENCE_DETAILS");
            entity.Property(e => e.StartDate).HasColumnName("START_DATE");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.Activity).WithMany(p => p.AssignmentMasters)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("Activity_FK");

            entity.HasOne(d => d.Approver).WithMany(p => p.AssignmentMasterApprovers)
                .HasForeignKey(d => d.ApproverId)
                .HasConstraintName("Approved_By_FK");

            entity.HasOne(d => d.User).WithMany(p => p.AssignmentMasterUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("User_Id_FK");
        });

        modelBuilder.Entity<CategoryListMaster>(entity =>
        {
            entity.HasKey(e => e.ListId).HasName("CATEGORY_LIST_MASTER_pkey");

            entity.ToTable("CATEGORY_LIST_MASTER", "GRC_MASTER");

            entity.Property(e => e.ListId)
                .ValueGeneratedNever()
                .HasColumnName("LIST_ID");
            entity.Property(e => e.Active)
                .HasColumnType("char")
                .HasColumnName("ACTIVE");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Code)
                .HasColumnType("character varying")
                .HasColumnName("CODE");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

            entity.HasOne(d => d.Category).WithMany(p => p.CategoryListMasters)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("CATEGORY_ID_FK");
        });

        modelBuilder.Entity<CategoryMaster>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("CATEGORY_MASTER_pkey");

            entity.ToTable("CATEGORY_MASTER", "GRC_MASTER");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Active)
                .HasColumnType("char")
                .HasColumnName("ACTIVE");
            entity.Property(e => e.Category)
                .HasColumnType("character varying")
                .HasColumnName("CATEGORY");
            entity.Property(e => e.Editable)
                .HasColumnType("char")
                .HasColumnName("EDITABLE");
        });

        modelBuilder.Entity<ClientRoleMaster>(entity =>
        {
            entity.HasKey(e => e.CliRoleId).HasName("client_role_master_pkey");

            entity.ToTable("CLIENT_ROLE_MASTER", "GRC_MASTER");

            entity.Property(e => e.CliRoleId)
                .ValueGeneratedNever()
                .HasColumnName("CLI_ROLE_ID");
            entity.Property(e => e.Active)
                .HasMaxLength(1)
                .HasColumnName("ACTIVE");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.RoleName).HasColumnName("ROLE_NAME");
            entity.Property(e => e.RoleTypeId).HasColumnName("ROLE_TYPE_ID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ClientRoleMasters)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("CREATED_BY_FK");

            entity.HasOne(d => d.RoleType).WithMany(p => p.ClientRoleMasters)
                .HasForeignKey(d => d.RoleTypeId)
                .HasConstraintName("ROLE_TYPE_ID_FK");
        });

        modelBuilder.Entity<ClientUserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CLIENT_USER_INFO_pkey");

            entity.ToTable("CLIENT_USER_INFO", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CliRoleId).HasColumnName("CLI_ROLE_ID");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("EMAIL");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("NAME");
            entity.Property(e => e.Status)
                .HasColumnType("char")
                .HasColumnName("STATUS");
            entity.Property(e => e.SysUserId).HasColumnName("SYS_USER_ID");

            entity.HasOne(d => d.CliRole).WithMany(p => p.ClientUserInfos)
                .HasForeignKey(d => d.CliRoleId)
                .HasConstraintName("CLI_ROLE_ID_FK");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ClientUserInfoCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("CREATED_BY_FK");

            entity.HasOne(d => d.Customer).WithMany(p => p.ClientUserInfos)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("CUSTOMER_ID_FK");

            entity.HasOne(d => d.SysUser).WithMany(p => p.ClientUserInfoSysUsers)
                .HasForeignKey(d => d.SysUserId)
                .HasConstraintName("SYS_USER_ID_FK");
        });

        modelBuilder.Entity<DocumentMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Document_ID_PK");

            entity.ToTable("DOCUMENT_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Document)
                .HasColumnType("character varying")
                .HasColumnName("DOCUMENT");
        });

        modelBuilder.Entity<DomainMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DOMAIN_MASTER_pkey");

            entity.ToTable("DOMAIN_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DomainName)
                .HasColumnType("character varying")
                .HasColumnName("DOMAIN_NAME");
        });

        modelBuilder.Entity<EntitlementMaster>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Entitlement_PK");

            entity.ToTable("ENTITLEMENT_MASTER", "GRC_MASTER");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.MenuItem)
                .HasColumnType("character varying")
                .HasColumnName("MENU_ITEM");

            entity.HasOne(d => d.Role).WithOne(p => p.EntitlementMaster)
                .HasForeignKey<EntitlementMaster>(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Role_Id_FK");
        });

        modelBuilder.Entity<FrequencyMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Frequency_Master_pkey");

            entity.ToTable("FREQUENCY_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Frequency)
                .HasColumnType("character varying")
                .HasColumnName("FREQUENCY");
        });

        modelBuilder.Entity<GovernanceMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GovId_PK");

            entity.ToTable("GOVERNANCE_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Abbr)
                .HasColumnType("character varying")
                .HasColumnName("ABBR");
            entity.Property(e => e.Active)
                .HasColumnType("char")
                .HasColumnName("ACTIVE");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<ProcessMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PROCESS_MASTER_pkey");

            entity.ToTable("PROCESS_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ProcessName)
                .HasColumnType("character varying")
                .HasColumnName("PROCESS_NAME");
        });

        modelBuilder.Entity<RoleType>(entity =>
        {
            entity.HasKey(e => e.RoleTypeId).HasName("role_type_pky");

            entity.ToTable("ROLE_TYPE", "GRC_MASTER");

            entity.Property(e => e.RoleTypeId)
                .ValueGeneratedNever()
                .HasColumnName("ROLE_TYPE_ID");
            entity.Property(e => e.RoleTypeDescription)
                .HasColumnType("character varying")
                .HasColumnName("ROLE_TYPE_DESCRIPTION");
        });

        modelBuilder.Entity<StandardMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StdId_PK");

            entity.ToTable("STANDARD_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active)
                .HasColumnType("char")
                .HasColumnName("ACTIVE");
            entity.Property(e => e.GovrId).HasColumnName("GOVR_ID");
            entity.Property(e => e.LevelNames)
                .HasColumnType("character varying")
                .HasColumnName("LEVEL_NAMES");
            entity.Property(e => e.Levels).HasColumnName("LEVELS");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("NAME");
            entity.Property(e => e.NoOfControls).HasColumnName("NO_OF_CONTROLS");
            entity.Property(e => e.StdAbbr).HasColumnName("STD_ABBR");

            entity.HasOne(d => d.Govr).WithMany(p => p.StandardMasters)
                .HasForeignKey(d => d.GovrId)
                .HasConstraintName("GOVR_ID_FK");
        });

        modelBuilder.Entity<StatusMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Status_Master_pkey");

            entity.ToTable("STATUS_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<SysCustomerInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Customer_Id_PK");

            entity.ToTable("SYS_CUSTOMER_INFO", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active)
                .HasColumnType("char")
                .HasColumnName("ACTIVE");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("ADDRESS");
            entity.Property(e => e.City)
                .HasColumnType("character varying")
                .HasColumnName("CITY");
            entity.Property(e => e.ContactEmail)
                .HasColumnType("character varying")
                .HasColumnName("CONTACT_EMAIL");
            entity.Property(e => e.ContactName)
                .HasColumnType("character varying")
                .HasColumnName("CONTACT_NAME");
            entity.Property(e => e.ContactPhone)
                .HasColumnType("character varying")
                .HasColumnName("CONTACT_PHONE");
            entity.Property(e => e.Country)
                .HasColumnType("character varying")
                .HasColumnName("COUNTRY");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.CustomerName)
                .HasColumnType("character varying")
                .HasColumnName("CUSTOMER_NAME");
            entity.Property(e => e.DbMapString)
                .HasColumnType("character varying")
                .HasColumnName("DB_MAP_STRING");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.State)
                .HasColumnType("character varying")
                .HasColumnName("STATE");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SysCustomerInfos)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("CREATED_BY_FK");
        });

        modelBuilder.Entity<SysLicenseMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Lisence_Master_PK");

            entity.ToTable("SYS_LICENSE_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active)
                .HasColumnType("char")
                .HasColumnName("ACTIVE");
            entity.Property(e => e.Approved)
                .HasColumnType("char")
                .HasColumnName("APPROVED");
            entity.Property(e => e.ApprovedBy).HasColumnName("APPROVED_BY");
            entity.Property(e => e.ApprovedDate).HasColumnName("APPROVED_DATE");
            entity.Property(e => e.ContractDocuments)
                .HasColumnType("character varying")
                .HasColumnName("CONTRACT_DOCUMENTS");
            entity.Property(e => e.ContractPeriodInMonths).HasColumnName("CONTRACT_PERIOD_IN_MONTHS");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.EditedBy).HasColumnName("EDITED_BY");
            entity.Property(e => e.EditedDate).HasColumnName("EDITED_DATE");
            entity.Property(e => e.EndDate).HasColumnName("END_DATE");
            entity.Property(e => e.Remarks).HasColumnName("REMARKS");
            entity.Property(e => e.StandardId).HasColumnName("STANDARD_ID");
            entity.Property(e => e.StartOrRenewalDate).HasColumnName("START_OR_RENEWAL_DATE");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.SysLicenseMasterApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("APPROVED_BY_FK");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SysLicenseMasterCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("CREATED_BY_FK");

            entity.HasOne(d => d.Customer).WithMany(p => p.SysLicenseMasters)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("CUSTOMER_ID_FK");

            entity.HasOne(d => d.EditedByNavigation).WithMany(p => p.SysLicenseMasterEditedByNavigations)
                .HasForeignKey(d => d.EditedBy)
                .HasConstraintName("EDITED_BY_FK");

            entity.HasOne(d => d.Standard).WithMany(p => p.SysLicenseMasters)
                .HasForeignKey(d => d.StandardId)
                .HasConstraintName("STANDARD_ID_FK");
        });

        modelBuilder.Entity<SysRoleMaster>(entity =>
        {
            entity.HasKey(e => e.SysRoleId).HasName("sys_role_master_pkey");

            entity.ToTable("SYS_ROLE_MASTER", "GRC_MASTER");

            entity.Property(e => e.SysRoleId)
                .ValueGeneratedNever()
                .HasColumnName("SYS_ROLE_ID");
            entity.Property(e => e.Active)
                .HasColumnType("char")
                .HasColumnName("ACTIVE");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.RoleName).HasColumnName("ROLE_NAME");
            entity.Property(e => e.RoleTypeId).HasColumnName("ROLE_TYPE_ID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SysRoleMasters)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("Created_By_FK");

            entity.HasOne(d => d.RoleType).WithMany(p => p.SysRoleMasters)
                .HasForeignKey(d => d.RoleTypeId)
                .HasConstraintName("Role_Type_Id_FK");
        });

        modelBuilder.Entity<SysUserLogin>(entity =>
        {
            entity.HasKey(e => e.SysUserId).HasName("Sys_User_Id_PK");

            entity.ToTable("SYS_USER_LOGIN", "GRC_MASTER");

            entity.Property(e => e.SysUserId)
                .ValueGeneratedNever()
                .HasColumnName("SYS_USER_ID");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDatetime).HasColumnName("CREATED_DATETIME");
            entity.Property(e => e.LastloginDatetime).HasColumnName("LASTLOGIN_DATETIME");
            entity.Property(e => e.LoginEmailId)
                .HasColumnType("character varying")
                .HasColumnName("LOGIN_EMAIL_ID");
            entity.Property(e => e.LoginUserId)
                .HasColumnType("character varying")
                .HasColumnName("LOGIN_USER_ID");
            entity.Property(e => e.Mfacode)
                .HasColumnType("character varying")
                .HasColumnName("MFACODE");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("NAME");
            entity.Property(e => e.Newuser).HasColumnName("NEWUSER");
            entity.Property(e => e.NoofloginAttempts).HasColumnName("NOOFLOGIN_ATTEMPTS");
            entity.Property(e => e.Phoneno)
                .HasColumnType("character varying")
                .HasColumnName("PHONENO");
            entity.Property(e => e.Pwd)
                .HasColumnType("character varying")
                .HasColumnName("PWD");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("STATUS");
            entity.Property(e => e.SysRoleId).HasColumnName("SYS_ROLE_ID");
            entity.Property(e => e.Uid)
                .HasColumnType("character varying")
                .HasColumnName("UID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("Created_By_FK");

            entity.HasOne(d => d.NewuserNavigation).WithMany(p => p.SysUserLogins)
                .HasForeignKey(d => d.Newuser)
                .HasConstraintName("Role_Type_Id_FK");

            entity.HasOne(d => d.SysRole).WithMany(p => p.SysUserLogins)
                .HasForeignKey(d => d.SysRoleId)
                .HasConstraintName("SYS_ROLE_ID_FK");

            entity.HasMany(d => d.Customers).WithMany(p => p.SysUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "SysCustomerToUserMap",
                    r => r.HasOne<SysCustomerInfo>().WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Customer_Id_FK"),
                    l => l.HasOne<SysUserLogin>().WithMany()
                        .HasForeignKey("SysUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Sys_User_Id_FK"),
                    j =>
                    {
                        j.HasKey("SysUserId", "CustomerId").HasName("SysUserCust_PK");
                        j.ToTable("SYS_CUSTOMER_TO_USER_MAP", "GRC_MASTER");
                        j.IndexerProperty<int>("SysUserId").HasColumnName("SYS_USER_ID");
                        j.IndexerProperty<int>("CustomerId").HasColumnName("CUSTOMER_ID");
                    });
        });

        modelBuilder.Entity<TechnologiesMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TECHNOLOGIES_MASTER_pkey");

            entity.ToTable("TECHNOLOGIES_MASTER", "GRC_MASTER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.TechnologyName)
                .HasColumnType("character varying")
                .HasColumnName("TECHNOLOGY_NAME");
        });

        modelBuilder.Entity<UserActivityEmail>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("USER_ACTIVITY_EMAIL_pkey");

            entity.ToTable("USER_ACTIVITY_EMAIL", "GRC_MASTER");

            entity.Property(e => e.ActivityId)
                .ValueGeneratedNever()
                .HasColumnName("ACTIVITY_ID");
            entity.Property(e => e.EmailCodeToActivity)
                .HasColumnType("character varying")
                .HasColumnName("EMAIL_CODE_TO_ACTIVITY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
