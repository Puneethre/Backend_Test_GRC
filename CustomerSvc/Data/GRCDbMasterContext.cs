using System;
using System.Collections.Generic;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace GRCServices.Data;

public partial class GRCDbMasterContext : DbContext
{
    public GRCDbMasterContext()
    {
    }

    public GRCDbMasterContext(DbContextOptions<GRCDbMasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivitiyNameMaster> ActivitiyNameMasters { get; set; }

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

    public virtual DbSet<TechnologiesMaster> TechnologiesMasters { get; set; }

    public virtual DbSet<UserActivityEmail> UserActivityEmails { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Server=192.168.29.128;Port=5432;Database=grc_master;Username=GRC;Password=Welcome@0668;Include Error Detail=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivitiyNameMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ACTIVITIY_NAME_MASTER_pkey");

            entity.ToTable("ACTIVITIY_NAME_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActivityName)
                .HasColumnType("character varying")
                .HasColumnName("ACTIVITY_NAME");
        });

        modelBuilder.Entity<ActivityMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ACTIVITY_MASTER_pkey");

            entity.ToTable("ACTIVITY_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActivityDescr).HasColumnName("ACTIVITY_DESCR");
            entity.Property(e => e.ActivityNameId)
                .HasColumnType("character varying")
                .HasColumnName("ACTIVITY_NAME_ID");
            entity.Property(e => e.ApproverRole).HasColumnName("APPROVER_ROLE");
            entity.Property(e => e.Auditable)
                .HasColumnType("char")
                .HasColumnName("AUDITABLE");
            entity.Property(e => e.DoerRole).HasColumnName("DOER_ROLE");
            entity.Property(e => e.Duration).HasColumnName("DURATION");
            entity.Property(e => e.FrequencyId).HasColumnName("FREQUENCY_ID");
            entity.Property(e => e.HelpRef).HasColumnName("HELP_REF");
            entity.Property(e => e.IsActive)
                .HasColumnType("character varying")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.OutputDocumentPath).HasColumnName("OUTPUT_DOCUMENT_PATH");
            entity.Property(e => e.PracticeId).HasColumnName("PRACTICE_ID");
            entity.Property(e => e.RefDocumentId).HasColumnName("REF_DOCUMENT_ID");
            entity.Property(e => e.TriggeringActivityNameId).HasColumnName("TRIGGERING_ACTIVITY_NAME_ID");

            entity.HasOne(d => d.ApproverRoleNavigation).WithMany(p => p.ActivityMasterApproverRoleNavigations)
                .HasForeignKey(d => d.ApproverRole)
                .HasConstraintName("APPROVER_ROLE_FK");

            entity.HasOne(d => d.DoerRoleNavigation).WithMany(p => p.ActivityMasterDoerRoleNavigations)
                .HasForeignKey(d => d.DoerRole)
                .HasConstraintName("DOER_ROLE_FK");

            entity.HasOne(d => d.Frequency).WithMany(p => p.ActivityMasters)
                .HasForeignKey(d => d.FrequencyId)
                .HasConstraintName("FREQUENCY_FK");

            entity.HasOne(d => d.OutputDocumentPathNavigation).WithMany(p => p.ActivityMasterOutputDocumentPathNavigations)
                .HasForeignKey(d => d.OutputDocumentPath)
                .HasConstraintName("OUTPUT_DOCUMENT_FK");

            entity.HasOne(d => d.RefDocument).WithMany(p => p.ActivityMasterRefDocuments)
                .HasForeignKey(d => d.RefDocumentId)
                .HasConstraintName("REF_DOCUMENT_FK");

            entity.HasOne(d => d.TriggeringActivityName).WithMany(p => p.ActivityMasters)
                .HasForeignKey(d => d.TriggeringActivityNameId)
                .HasConstraintName("TRIGGERING_ACTIVITY_FK");
        });

        modelBuilder.Entity<AssignmentMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ASSIGNMENT_MASTER_pkey");

            entity.ToTable("ASSIGNMENT_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActivityMasterId).HasColumnName("ACTIVITY_MASTER_ID");
            entity.Property(e => e.ApprovalDate).HasColumnName("APPROVAL_DATE");
            entity.Property(e => e.ApprovalStatus)
                .HasColumnType("char")
                .HasColumnName("APPROVAL_STATUS");
            entity.Property(e => e.ApproverCliUserId).HasColumnName("APPROVER_CLI_USER_ID");
            entity.Property(e => e.ApproverComments).HasColumnName("APPROVER_COMMENTS");
            entity.Property(e => e.AuditCheck)
                .HasColumnType("char")
                .HasColumnName("AUDIT_CHECK");
            entity.Property(e => e.DoerCliUserId).HasColumnName("DOER_CLI_USER_ID");
            entity.Property(e => e.DoerComments).HasColumnName("DOER_COMMENTS");
            entity.Property(e => e.Doerstatus)
                .HasColumnType("character varying")
                .HasColumnName("DOERSTATUS");
            entity.Property(e => e.EndDate).HasColumnName("END_DATE");
            entity.Property(e => e.EvidenceDetails).HasColumnName("EVIDENCE_DETAILS");
            entity.Property(e => e.StartDate).HasColumnName("START_DATE");

            entity.HasOne(d => d.ActivityMaster).WithMany(p => p.AssignmentMasters)
                .HasForeignKey(d => d.ActivityMasterId)
                .HasConstraintName("ACTIVITY_ID_FK");

            entity.HasOne(d => d.ApproverCliUser).WithMany(p => p.AssignmentMasterApproverCliUsers)
                .HasForeignKey(d => d.ApproverCliUserId)
                .HasConstraintName("APPROVER_CLI_USER_ID_FK");

            entity.HasOne(d => d.DoerCliUser).WithMany(p => p.AssignmentMasterDoerCliUsers)
                .HasForeignKey(d => d.DoerCliUserId)
                .HasConstraintName("DOER_CLI_USER_ID_FK");
        });

        modelBuilder.Entity<CategoryListMaster>(entity =>
        {
            entity.HasKey(e => e.ListId).HasName("CATEGORY_LIST_MASTER_pkey");

            entity.ToTable("CATEGORY_LIST_MASTER", "master_config");

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

            entity.ToTable("CATEGORY_MASTER", "master_config");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Category)
                .HasColumnType("character varying")
                .HasColumnName("CATEGORY");
            entity.Property(e => e.IsActive)
                .HasColumnType("char")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsUserEditableList)
                .HasColumnType("char")
                .HasColumnName("IS_USER_EDITABLE_LIST");
        });

        modelBuilder.Entity<ClientRoleMaster>(entity =>
        {
            entity.HasKey(e => e.CliRoleId).HasName("CLIENT_ROLE_MASTER_pkey");

            entity.ToTable("CLIENT_ROLE_MASTER", "master_config");

            entity.Property(e => e.CliRoleId)
                .ValueGeneratedNever()
                .HasColumnName("CLI_ROLE_ID");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .HasColumnName("is_ACTIVE");
            entity.Property(e => e.RoleName).HasColumnName("ROLE_NAME");
            entity.Property(e => e.RoleTypeId).HasColumnName("ROLE_TYPE_ID");

            entity.HasOne(d => d.RoleType).WithMany(p => p.ClientRoleMasters)
                .HasForeignKey(d => d.RoleTypeId)
                .HasConstraintName("ROLE_TYPE_ID_FK");
        });

        modelBuilder.Entity<ClientUserInfo>(entity =>
        {
            entity.HasKey(e => e.CliUserId).HasName("CLIENT_USER_INFO_pkey");

            entity.ToTable("CLIENT_USER_INFO", "master_config");

            entity.Property(e => e.CliUserId)
                .ValueGeneratedNever()
                .HasColumnName("CLI_USER_ID");
            entity.Property(e => e.CliRoleId).HasColumnName("CLI_ROLE_ID");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("EMAIL");
            entity.Property(e => e.IsActive)
                .HasColumnType("character varying")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("NAME");
            entity.Property(e => e.SysUserId).HasColumnName("SYS_USER_ID");

            entity.HasOne(d => d.CliRole).WithMany(p => p.ClientUserInfos)
                .HasForeignKey(d => d.CliRoleId)
                .HasConstraintName("CLI_ROLE_ID_FK");
        });

        modelBuilder.Entity<DocumentMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DOCUMENT_MASTER_pkey");

            entity.ToTable("DOCUMENT_MASTER", "master_config");

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

            entity.ToTable("DOMAIN_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DomainName)
                .HasColumnType("character varying")
                .HasColumnName("DOMAIN_NAME");
        });

        modelBuilder.Entity<EntitlementMaster>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("ENTITLEMENT_MASTER_pkey");

            entity.ToTable("ENTITLEMENT_MASTER", "master_config");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.MenuItem)
                .HasColumnType("character varying")
                .HasColumnName("MENU_ITEM");
        });

        modelBuilder.Entity<FrequencyMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FREQUENCY_MASTER_pkey");

            entity.ToTable("FREQUENCY_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Frequency)
                .HasColumnType("character varying")
                .HasColumnName("FREQUENCY");
        });

        modelBuilder.Entity<GovernanceMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GOVERNANCE_MASTER_pkey");

            entity.ToTable("GOVERNANCE_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.IsActive)
                .HasColumnType("char")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("NAME");
            entity.Property(e => e.ShortName)
                .HasColumnType("character varying")
                .HasColumnName("SHORT_NAME");
        });

        modelBuilder.Entity<ProcessMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PROCESS_MASTER_pkey");

            entity.ToTable("PROCESS_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ProcessName)
                .HasColumnType("character varying")
                .HasColumnName("PROCESS_NAME");
        });

        modelBuilder.Entity<RoleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ROLE_TYPE_pkey");

            entity.ToTable("ROLE_TYPE", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("DESCRIPTION");
        });

        modelBuilder.Entity<StandardMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("STANDARD_MASTER_pkey");

            entity.ToTable("STANDARD_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.GovrId).HasColumnName("GOVR_ID");
            entity.Property(e => e.IsActive)
                .HasColumnType("char")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.LevelNames)
                .HasColumnType("character varying")
                .HasColumnName("LEVEL_NAMES");
            entity.Property(e => e.Levels).HasColumnName("LEVELS");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("NAME");
            entity.Property(e => e.NoOfControls).HasColumnName("NO_OF_CONTROLS");
            entity.Property(e => e.ShortName).HasColumnName("SHORT_NAME");

            entity.HasOne(d => d.Govr).WithMany(p => p.StandardMasters)
                .HasForeignKey(d => d.GovrId)
                .HasConstraintName("GOVR_ID_FK");
        });

        modelBuilder.Entity<StatusMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("STATUS_MASTER_pkey");

            entity.ToTable("STATUS_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<TechnologiesMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TECHNOLOGIES_MASTER_pkey");

            entity.ToTable("TECHNOLOGIES_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.TechnologyName)
                .HasColumnType("character varying")
                .HasColumnName("TECHNOLOGY_NAME");
        });

        modelBuilder.Entity<UserActivityEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("USER_ACTIVITY_EMAIL_pkey");

            entity.ToTable("USER_ACTIVITY_EMAIL", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActivityId).HasColumnName("ACTIVITY_ID");
            entity.Property(e => e.EmailCodeToActivity)
                .HasColumnType("character varying")
                .HasColumnName("EMAIL_CODE_TO_ACTIVITY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
