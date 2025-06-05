using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgAppointmentItemStatus
{
    public int AppointmentItemStatusId { get; set; }

    public string AppointmentItemStatusNameEng { get; set; } = null!;

    public string? AppointmentItemStatusNameLatn { get; set; }

    public string? AppointmentItemStatusNameCyr { get; set; }

    public int? AppointmentItemStatusParentId { get; set; }

    public string AppointmentItemStatusClass { get; set; } = null!;

    public virtual ICollection<AlgAppointmentItem> AlgAppointmentItems { get; set; } = new List<AlgAppointmentItem>();

    public virtual AlgAppointmentItemStatus? AppointmentItemStatusParent { get; set; }

    public virtual ICollection<AlgAppointmentItemStatus> InverseAppointmentItemStatusParent { get; set; } = new List<AlgAppointmentItemStatus>();
}
