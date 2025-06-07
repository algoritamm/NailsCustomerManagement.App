using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AdmRole
{
    public int RoleId { get; set; }

    public string RoleCode { get; set; } = null!;

    public string RoleNameEng { get; set; } = null!;

    public string? RoleNameLatn { get; set; }

    public string? RoleNameCyr { get; set; }

    public int IsAd { get; set; }

    public string InsertUser { get; set; } = null!;

    public DateTime InsertDate { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<AdmAccountRole> AdmAccountRoles { get; set; } = new List<AdmAccountRole>();

    public virtual ICollection<AdmPermissionRole> AdmPermissionRoles { get; set; } = new List<AdmPermissionRole>();
}
