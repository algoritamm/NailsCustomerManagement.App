using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AdmAccountRole
{
    public int AccountId { get; set; }

    public int RoleId { get; set; }

    public string InsertUser { get; set; } = null!;

    public string InsertDate { get; set; } = null!;

    public string? UpdateUser { get; set; }

    public string? UpdateDate { get; set; }

    public virtual AdmAccount Account { get; set; } = null!;

    public virtual AdmRole Role { get; set; } = null!;
}
