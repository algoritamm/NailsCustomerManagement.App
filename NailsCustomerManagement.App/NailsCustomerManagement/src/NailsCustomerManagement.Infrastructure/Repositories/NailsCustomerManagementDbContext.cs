using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NailsCustomerManagement.Core.Entities;

namespace NailsCustomerManagement.Infrastructure.Repositories;

public partial class NailsCustomerManagementDbContext : DbContext
{
    public NailsCustomerManagementDbContext()
    {
    }

    public NailsCustomerManagementDbContext(DbContextOptions<NailsCustomerManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdmAccount> AdmAccounts { get; set; }

    public virtual DbSet<AdmAccountRole> AdmAccountRoles { get; set; }

    public virtual DbSet<AdmPermission> AdmPermissions { get; set; }

    public virtual DbSet<AdmPermissionRole> AdmPermissionRoles { get; set; }

    public virtual DbSet<AdmPermissionRoleStatus> AdmPermissionRoleStatuses { get; set; }

    public virtual DbSet<AdmRole> AdmRoles { get; set; }

    public virtual DbSet<AlgAppointment> AlgAppointments { get; set; }

    public virtual DbSet<AlgAppointmentItem> AlgAppointmentItems { get; set; }

    public virtual DbSet<AlgAppointmentItemStatus> AlgAppointmentItemStatuses { get; set; }

    public virtual DbSet<AlgAppointmentStatus> AlgAppointmentStatuses { get; set; }

    public virtual DbSet<AlgCustomer> AlgCustomers { get; set; }

    public virtual DbSet<AlgCustomerStatus> AlgCustomerStatuses { get; set; }

    public virtual DbSet<AlgDepartment> AlgDepartments { get; set; }

    public virtual DbSet<AlgJobPosition> AlgJobPositions { get; set; }

    public virtual DbSet<AlgLanguageCountry> AlgLanguageCountries { get; set; }

    public virtual DbSet<AlgPaymentType> AlgPaymentTypes { get; set; }

    public virtual DbSet<AlgServiceType> AlgServiceTypes { get; set; }

    public virtual DbSet<SysSessionLog> SysSessionLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:/Users/mjerinic/Source/Repos/NailsCustomerManagement.App/NailsCustomerManagementDev.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdmAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("adm_ACCOUNT");

            entity.HasIndex(e => e.AccountId, "IX_adm_ACCOUNT_AccountId").IsUnique();

            entity.HasIndex(e => e.Password, "IX_adm_ACCOUNT_Password").IsUnique();

            entity.HasIndex(e => e.UserCode, "IX_adm_ACCOUNT_UserCode").IsUnique();

            entity.HasIndex(e => e.UserName, "IX_adm_ACCOUNT_UserName").IsUnique();

            entity.Property(e => e.IsEnabled).HasDefaultValue(1);

            entity.HasOne(d => d.JobPosition).WithMany(p => p.AdmAccounts)
                .HasForeignKey(d => d.JobPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AdmAccountRole>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.RoleId });

            entity.ToTable("adm_ACCOUNT_ROLE");

            entity.HasOne(d => d.Account).WithMany(p => p.AdmAccountRoles)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Role).WithMany(p => p.AdmAccountRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AdmPermission>(entity =>
        {
            entity.HasKey(e => e.PermissionId);

            entity.ToTable("adm_PERMISSION");

            entity.HasIndex(e => e.PermissionId, "IX_adm_PERMISSION_PermissionId").IsUnique();

            entity.HasIndex(e => e.PermissionKey, "IX_adm_PERMISSION_PermissionKey").IsUnique();

            entity.Property(e => e.PermissionId).ValueGeneratedNever();
        });

        modelBuilder.Entity<AdmPermissionRole>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId });

            entity.ToTable("adm_PERMISSION_ROLE");

            entity.HasOne(d => d.Permission).WithMany(p => p.AdmPermissionRoles)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PermissionRoleStatus).WithMany(p => p.AdmPermissionRoles)
                .HasForeignKey(d => d.PermissionRoleStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Role).WithMany(p => p.AdmPermissionRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AdmPermissionRoleStatus>(entity =>
        {
            entity.HasKey(e => e.PermissionRoleStatusId);

            entity.ToTable("adm_PERMISSION_ROLE_STATUS");

            entity.Property(e => e.PermissionRoleStatusId).ValueGeneratedNever();
        });

        modelBuilder.Entity<AdmRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("adm_ROLE");

            entity.HasIndex(e => e.RoleCode, "IX_adm_ROLE_RoleCode").IsUnique();

            entity.HasIndex(e => e.RoleId, "IX_adm_ROLE_RoleId").IsUnique();

            entity.Property(e => e.IsAd).HasColumnName("IsAD");
        });

        modelBuilder.Entity<AlgAppointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId);

            entity.ToTable("alg_APPOINTMENT");

            entity.HasIndex(e => e.AppointmentId, "IX_alg_APPOINTMENT_AppointmentId").IsUnique();

            entity.HasOne(d => d.AppointmentStatus).WithMany(p => p.AlgAppointments)
                .HasForeignKey(d => d.AppointmentStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Customer).WithMany(p => p.AlgAppointments)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AlgAppointmentItem>(entity =>
        {
            entity.HasKey(e => e.AppointmentItemId);

            entity.ToTable("alg_APPOINTMENT_ITEM");

            entity.HasIndex(e => e.AppointmentItemId, "IX_alg_APPOINTMENT_ITEM_AppointmentItemId").IsUnique();

            entity.HasOne(d => d.Account).WithMany(p => p.AlgAppointmentItems)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Appointment).WithMany(p => p.AlgAppointmentItems)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.AppointmentItemStatus).WithMany(p => p.AlgAppointmentItems)
                .HasForeignKey(d => d.AppointmentItemStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Customer).WithMany(p => p.AlgAppointmentItems)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PaymentType).WithMany(p => p.AlgAppointmentItems).HasForeignKey(d => d.PaymentTypeId);

            entity.HasOne(d => d.ServiceType).WithMany(p => p.AlgAppointmentItems)
                .HasForeignKey(d => d.ServiceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AlgAppointmentItemStatus>(entity =>
        {
            entity.HasKey(e => e.AppointmentItemStatusId);

            entity.ToTable("alg_APPOINTMENT_ITEM_STATUS");

            entity.HasIndex(e => e.AppointmentItemStatusId, "IX_alg_APPOINTMENT_ITEM_STATUS_AppointmentItemStatusId").IsUnique();

            entity.Property(e => e.AppointmentItemStatusId).ValueGeneratedNever();

            entity.HasOne(d => d.AppointmentItemStatusParent).WithMany(p => p.InverseAppointmentItemStatusParent).HasForeignKey(d => d.AppointmentItemStatusParentId);
        });

        modelBuilder.Entity<AlgAppointmentStatus>(entity =>
        {
            entity.HasKey(e => e.AppointmentStatusId);

            entity.ToTable("alg_APPOINTMENT_STATUS");

            entity.HasIndex(e => e.AppointmentStatusId, "IX_alg_APPOINTMENT_STATUS_AppointmentStatusId").IsUnique();

            entity.Property(e => e.AppointmentStatusId).ValueGeneratedNever();

            entity.HasOne(d => d.AppointmentStatusParent).WithMany(p => p.InverseAppointmentStatusParent).HasForeignKey(d => d.AppointmentStatusParentId);
        });

        modelBuilder.Entity<AlgCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.ToTable("alg_CUSTOMER");

            entity.HasIndex(e => e.CustomerId, "IX_alg_CUSTOMER_CustomerId").IsUnique();
        });

        modelBuilder.Entity<AlgCustomerStatus>(entity =>
        {
            entity.HasKey(e => e.CustomerStatusId);

            entity.ToTable("alg_CUSTOMER_STATUS");

            entity.HasIndex(e => e.CustomerStatusId, "IX_alg_CUSTOMER_STATUS_CustomerStatusId").IsUnique();

            entity.Property(e => e.CustomerStatusId).ValueGeneratedNever();

            entity.HasOne(d => d.CustomerStatusParent).WithMany(p => p.InverseCustomerStatusParent).HasForeignKey(d => d.CustomerStatusParentId);
        });

        modelBuilder.Entity<AlgDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId);

            entity.ToTable("alg_DEPARTMENT");

            entity.HasIndex(e => e.DepartmentId, "IX_alg_DEPARTMENT_DepartmentId").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(1);
        });

        modelBuilder.Entity<AlgJobPosition>(entity =>
        {
            entity.HasKey(e => e.JobPositionId);

            entity.ToTable("alg_JOB_POSITION");

            entity.HasIndex(e => e.JobPositionId, "IX_alg_JOB_POSITION_JobPositionId").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(1);
        });

        modelBuilder.Entity<AlgLanguageCountry>(entity =>
        {
            entity.HasKey(e => e.LanguageCountryId);

            entity.ToTable("alg_LANGUAGE_COUNTRY");

            entity.HasIndex(e => e.LanguageCountryId, "IX_alg_LANGUAGE_COUNTRY_LanguageCountryId").IsUnique();
        });

        modelBuilder.Entity<AlgPaymentType>(entity =>
        {
            entity.HasKey(e => e.PaymnetTypeId);

            entity.ToTable("alg_PAYMENT_TYPE");

            entity.HasIndex(e => e.PaymnetTypeId, "IX_alg_PAYMENT_TYPE_PaymnetTypeId").IsUnique();

            entity.Property(e => e.PaymnetTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<AlgServiceType>(entity =>
        {
            entity.HasKey(e => e.ServiceTypeId);

            entity.ToTable("alg_SERVICE_TYPE");

            entity.HasIndex(e => e.ServiceTypeId, "IX_alg_SERVICE_TYPE_ServiceTypeId").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(1);

            entity.HasOne(d => d.Department).WithMany(p => p.AlgServiceTypes)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SysSessionLog>(entity =>
        {
            entity.HasKey(e => e.SessionId);
            entity.ToTable("sys_SESSION_LOG");
            entity.HasIndex(e => e.SessionId, "IX_sys_SESSION_LOG_SessionId").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
