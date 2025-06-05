using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgAppointment
{
    public int AppointmentId { get; set; }

    public string AppointmentName { get; set; } = null!;

    public int CustomerId { get; set; }

    public string AppointmentCreatedDate { get; set; } = null!;

    public int AppointmentStatusId { get; set; }

    public string InsertUser { get; set; } = null!;

    public string InsertDate { get; set; } = null!;

    public string? UpdateUser { get; set; }

    public string? UpdateDate { get; set; }

    public virtual ICollection<AlgAppointmentItem> AlgAppointmentItems { get; set; } = new List<AlgAppointmentItem>();

    public virtual AlgAppointmentStatus AppointmentStatus { get; set; } = null!;

    public virtual AlgCustomer Customer { get; set; } = null!;
}
