using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AdmPermissionRole
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public int PermissionRoleStatusId { get; set; }

    public string InsertUser { get; set; } = null!;

    public DateTime InsertDate { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual AdmPermission Permission { get; set; } = null!;

    public virtual AdmPermissionRoleStatus PermissionRoleStatus { get; set; } = null!;

    public virtual AdmRole Role { get; set; } = null!;
}
