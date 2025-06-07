using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgAppointmentItem
{
    public int AppointmentItemId { get; set; }

    public string AppointmentItemName { get; set; } = null!;

    public int AppointmentItemStatusId { get; set; }

    public int CustomerId { get; set; }

    public DateTime AppointmentItemCretedDate { get; set; }

    public int ServiceTypeId { get; set; }

    public int AccountId { get; set; }

    public DateTime AppointmentItemDate { get; set; }

    public TimeSpan AppointmentItemTime { get; set; }

    public int AppointmentId { get; set; }

    public string? AmountPaid { get; set; }

    public int? PaymentTypeId { get; set; }

    public string InsertUser { get; set; } = null!;

    public DateTime? InsertDate { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual AdmAccount Account { get; set; } = null!;

    public virtual AlgAppointment Appointment { get; set; } = null!;

    public virtual AlgAppointmentItemStatus AppointmentItemStatus { get; set; } = null!;

    public virtual AlgCustomer Customer { get; set; } = null!;

    public virtual AlgPaymentType? PaymentType { get; set; }

    public virtual AlgServiceType ServiceType { get; set; } = null!;
}
