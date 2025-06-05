using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgCustomerStatus
{
    public int CustomerStatusId { get; set; }

    public string CustomerStatusNameEng { get; set; } = null!;

    public string? CustomerStatusNameLatn { get; set; }

    public string? CustomerStatusNameCyr { get; set; }

    public int? CustomerStatusParentId { get; set; }

    public string CustomerStatusClass { get; set; } = null!;

    public virtual AlgCustomerStatus? CustomerStatusParent { get; set; }

    public virtual ICollection<AlgCustomerStatus> InverseCustomerStatusParent { get; set; } = new List<AlgCustomerStatus>();
}
