using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AdmAccount
{
    public int AccountId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserCode { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public string? TelephoneNo { get; set; }

    public int IsEnabled { get; set; }

    public int JobPositionId { get; set; }

    public int DepartmentId { get; set; }

    public string InsertUser { get; set; } = null!;

    public DateTime InsertDate { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<AdmAccountRole> AdmAccountRoles { get; set; } = new List<AdmAccountRole>();

    public virtual ICollection<AlgAppointmentItem> AlgAppointmentItems { get; set; } = new List<AlgAppointmentItem>();

    public virtual AlgJobPosition JobPosition { get; set; } = null!;
}
