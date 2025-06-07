using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgDepartment
{
    public int DepartmentId { get; set; }

    public string DepartmentNameEng { get; set; } = null!;

    public string? DepartmentNameLatn { get; set; }

    public string? DepartmentNameCyr { get; set; }

    public int IsActive { get; set; }

    public string InsertUser { get; set; } = null!;

    public DateTime InsertDate { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<AlgServiceType> AlgServiceTypes { get; set; } = new List<AlgServiceType>();
}
