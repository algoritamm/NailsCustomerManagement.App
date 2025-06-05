using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgAppointmentStatus
{
    public int AppointmentStatusId { get; set; }

    public string AppointmentStatusNameEng { get; set; } = null!;

    public string AppointmentStatusNameLatn { get; set; } = null!;

    public string AppointmentStatusNameCyr { get; set; } = null!;

    public int? AppointmentStatusParentId { get; set; }

    public string AppointmentStatusClass { get; set; } = null!;

    public virtual ICollection<AlgAppointment> AlgAppointments { get; set; } = new List<AlgAppointment>();

    public virtual AlgAppointmentStatus? AppointmentStatusParent { get; set; }

    public virtual ICollection<AlgAppointmentStatus> InverseAppointmentStatusParent { get; set; } = new List<AlgAppointmentStatus>();
}
