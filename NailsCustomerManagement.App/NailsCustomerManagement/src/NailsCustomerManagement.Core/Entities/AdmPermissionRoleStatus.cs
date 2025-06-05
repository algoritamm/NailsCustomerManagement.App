using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AdmPermissionRoleStatus
{
    public int PermissionRoleStatusId { get; set; }

    public string PermissionRoleStatusEng { get; set; } = null!;

    public string? PermissionRoleStatusLatn { get; set; }

    public string? PermissionRoleStatusCyr { get; set; }

    public virtual ICollection<AdmPermissionRole> AdmPermissionRoles { get; set; } = new List<AdmPermissionRole>();
}
