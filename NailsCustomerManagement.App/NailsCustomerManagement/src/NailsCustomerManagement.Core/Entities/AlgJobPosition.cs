using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgJobPosition
{
    public int JobPositionId { get; set; }

    public string JobPositionNameEng { get; set; } = null!;

    public string? JobPositionNameLatn { get; set; }

    public string? JobPositionNameCyr { get; set; }

    public int IsActive { get; set; }

    public string InsertUser { get; set; } = null!;

    public DateTime InsertDate { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<AdmAccount> AdmAccounts { get; set; } = new List<AdmAccount>();
}
