using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgAppointment
{
    public int AppointmentId { get; set; }

    public string AppointmentName { get; set; } = null!;

    public int CustomerId { get; set; }

    public DateTime AppointmentCreatedDate { get; set; }

    public int AppointmentStatusId { get; set; }
    public int AccountId { get; set; }

    public string InsertUser { get; set; } = null!;

    public DateTime InsertDate { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<AlgAppointmentItem> AlgAppointmentItems { get; set; } = new List<AlgAppointmentItem>();

    public virtual AlgAppointmentStatus AppointmentStatus { get; set; } = null!;

    public virtual AlgCustomer Customer { get; set; } = null!;
}
