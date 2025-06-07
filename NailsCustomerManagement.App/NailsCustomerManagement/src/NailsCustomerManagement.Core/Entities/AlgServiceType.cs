using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgServiceType
{
    public int ServiceTypeId { get; set; }

    public string ServiceTypeNameEng { get; set; } = null!;

    public string? ServiceTypeNameLatn { get; set; }

    public string? ServiceTypeNameCyr { get; set; }

    public int ServicePrice { get; set; }

    public int IsActive { get; set; }

    public int DepartmentId { get; set; }

    public string InsertUser { get; set; } = null!;

    public DateTime InsertDate { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<AlgAppointmentItem> AlgAppointmentItems { get; set; } = new List<AlgAppointmentItem>();

    public virtual AlgDepartment Department { get; set; } = null!;
}
