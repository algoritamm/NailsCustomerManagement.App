using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgCustomer
{
    public int CustomerId { get; set; }

    public string CustomerFirstName { get; set; } = null!;

    public string CustomerLastName { get; set; } = null!;

    public string CustomerTelephoneNo { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public int CustomerStatusId { get; set; }

    public string InsertUser { get; set; } = null!;

    public string InsertDate { get; set; } = null!;

    public string? UpdateUser { get; set; }

    public string? UpdateDate { get; set; }

    public virtual ICollection<AlgAppointmentItem> AlgAppointmentItems { get; set; } = new List<AlgAppointmentItem>();
    public virtual ICollection<AlgAppointment> AlgAppointments { get; set; } = new List<AlgAppointment>();
}
