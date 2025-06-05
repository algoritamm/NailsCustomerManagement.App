using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgPaymentType
{
    public int PaymnetTypeId { get; set; }

    public string PaymnetTypeNameEng { get; set; } = null!;

    public string? PaymnetTypeNameLatn { get; set; }

    public string? PaymnetTypeNameCyr { get; set; }

    public virtual ICollection<AlgAppointmentItem> AlgAppointmentItems { get; set; } = new List<AlgAppointmentItem>();
}
