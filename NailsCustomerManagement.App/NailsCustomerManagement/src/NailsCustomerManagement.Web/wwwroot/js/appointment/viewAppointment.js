(function (app, $) {
    function init() {
        app.appointment.viewAppointment = this;
        function initClickEvent() {
            $(document).on('click', '#btnFilterDismiss', function () {

                showFilterLoader(function () {
                    $('#statusFilter').multiselect('selectAll', false);
                    $('#statusFilter').multiselect('updateButtonText');

                    $('#appointmentItemsTable tbody tr').show();
                });

            });

            $(document).on('click', '#btnFilterApply', function () {
                showFilterLoader(function () {
                    const selected = $("#statusFilter").val() || [];
                    $('#appointmentItemsTable tbody tr').each(function () {
                        const statusId = ($(this).data('status-id') || '').toString();
                        if (selected.includes(statusId)) {
                            $(this).show();
                        } else {
                            $(this).hide();
                        }
                    });
                });
            });

            //Open Model
            $(document).on("click", ".btnInsertAppointment", function () {
                const id = $("#AppointmentId").val();
                $("#insertAppointmentDiv").load('/Appointment/_InsertAppointment', { AppointmentId: id }, function () {
                    $('#insertAppointmentModal').modal('toggle');
                    $('#insertAppointmentModal').find("form").each(function () {
                        $(this).validate().resetForm();
                        $.data($(this)[0], 'validator', false);
                    });
                    $.validator.unobtrusive.parse($('#insertAppointmentModal').find("form"));
                });
            });

            $("#insertAppointmentModal").on('click', "#btnConfirmeInsertAppointment", function () {
                const modalInsertAppointment = $("#insertAppointmentModal")

                if (!app.isModalFormValid($(modalInsertAppointment)))
                    return;

                $.ajax({
                    type: "POST",
                    url: "/Appointment/InsertAppointmentItem",
                    headers: {
                        'RequestVerificationToken': token
                    },
                    data: formData,
                    success: function (response) {
                        $(modalInsertAppointment).model('toggle');
                    },
                    error: function (xhr, desc, err) {
                        console.error("ERROR: ", err);
                        $(modalInsertAppointment).model('toggle');
                    }
                });
            });


        }

        function showFilterLoader(callback) {
            $('#loading-indicator').show();

            setTimeout(function () {
                $('#loading-indicator').hide();
                if (typeof callback === 'function') callback();
            }, 2000); // 2 sec
        }

        this.init = function (obj) {
            initClickEvent();
        };
    }

    return new init();
})(app, jQuery);