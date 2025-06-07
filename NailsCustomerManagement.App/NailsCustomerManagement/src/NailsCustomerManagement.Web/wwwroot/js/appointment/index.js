(function (app, $) {
    function init() {
        app.appointment.index = this;

        const divInsertNewCustomer = $('#insertNewCustomerDiv');
        const divUpdateAppointment = $('#updateAppointmentDiv');
        function initClickEvent()
        {
            $(document).on('click', '#btnFilterDismiss', function () {

                showFilterLoader(function () {
                    $('#statusFilter').multiselect('selectAll', false);
                    $('#statusFilter').multiselect('updateButtonText');

                    $('#appointmentsTable tbody tr').show();
                });

            });

            $(document).on('click', '#btnFilterApply', function () {
                showFilterLoader(function () {
                    const selected = $("#statusFilter").val() || [];
                    $('#appointmentsTable tbody tr').each(function () {
                        const statusId = ($(this).data('status-id') || '').toString();
                        if (selected.includes(statusId)) {
                            $(this).show();
                        } else {
                            $(this).hide();
                        }
                    });
                });
            });

            // Open modal - insert new customer appointment
            $(document).on('click', '#btnInsertCustomer', function () {
                $.ajax({
                    type: "GET",
                    url: "/Appointment/_InsertNewCustomerAppointment",
                    success: function (result) {
                        if (divInsertNewCustomer.html().length != 0) divInsertNewCustomer.html("");

                        divInsertNewCustomer.append(result);
                        const $model = $('#insertNewCustomerModal');

                        $model.modal('show');

                        $model.find("form").each(function () {
                            $(this).validate().resetForm();
                            $.data($(this)[0], 'validator', false);
                        });
                        $.validator.unobtrusive.parse($model.find("form"));

                        // Init datetime picker
                        $('.datepickers').datetimepicker({
                            format: 'YYYY-MM-DD',
                            defaultDate: moment() // today
                        });
                        // Init time picker
                        $('.timepickers').datetimepicker({
                            format: 'HH:mm',
                            stepping: 15,
                            icons: {
                                time: 'far fa-clock',
                                up: 'fas fa-arrow-up',
                                down: 'fas fa-arrow-down'
                            }
                        });

                        $model.SetModalInputs({
                            clearInputs: true,
                            clearValidation: true
                        });

                        const now = moment();
                        $model.find('#InsertAppointment_ItemDate').val(now.format('YYYY-MM-DD'));
                        $model.find('#InsertAppointment_ItemTime').val(now.format('HH:mm'));
                    },
                    error: function (xhr, desc, err) {
                        console.error("ERROR: ", err);
                        $("#insertNewCustomerModal").modal('hide');
                    }
                });
            });

            $(document).on('click', '#btnConfirmeInsertAppointment', function () {
                const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');

                if (!app.isModalFormValid($("#insertNewCustomerModal")))
                    return;

                $.ajax({
                    type: "POST",
                    url: "/Appointment/InsertNewCustomerAppointment",
                    headers: {
                        'RequestVerificationToken': token
                    },
                    data: $("#insertNewCustomerModal").find('form').serializeArray(),
                    success: function (response) {
                        $("#insertNewCustomerModal").modal('hide');
                        $('#loading-indicator').show();
                        setTimeout(function () {
                            $('#loading-indicator').hide();
                            window.location.reload();
                        }, 2000);
                    },
                    error: function (xhr, desc, err) {
                        console.error("ERROR: ", err);
                        $("#insertNewCustomerModal").modal('hide');
                    }
                });
            });

            // Open modal - update appointment status
            $(document).on('click', '#updateAppointmentBtn', function () {
                var id = $(this).data("item-id");
                $.ajax({
                    type: "GET",
                    url: "/Appointment/_UpdateAppointmentStatus?appointmentId=" + id,
                    success: function (result) {
                        if (divUpdateAppointment.html().length != 0) divUpdateAppointment.html("");

                        divUpdateAppointment.append(result);
                        const $model = $('#updateStatusModal');

                        $model.modal('show');

                        $model.find("form").each(function () {
                            $(this).validate().resetForm();
                            $.data($(this)[0], 'validator', false);
                        });
                        $.validator.unobtrusive.parse($model.find("form"));

                        $model.SetModalInputs({
                            clearValidation: true
                        });

                    },
                    error: function (xhr, desc, err) {
                        console.error("ERROR: ", err);
                        $("#updateStatusModal").modal('hide');
                    }
                });
            });

            $(document).on('click', '#btnConfirmeUpdateStatus', function () {
                const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');
                var id = $('#AppointmentId').val();
                var status = $('#SelectedStatusId').val();

                if (!app.isModalFormValid($("#updateStatusModal")))
                    return;

                $.ajax({
                    type: "POST",
                    url: "/Appointment/UpdateAppointmentStatus?appointmentId=" + id + "&statusId=" + status,
                    headers: {
                        'RequestVerificationToken': token
                    },
                    success: function (response) {
                        $("#updateStatusModal").modal('hide');
                        $('#loading-indicator').show();
                        setTimeout(function () {
                            $('#loading-indicator').hide();
                            window.location.reload();
                        }, 1000);
                    },
                    error: function (xhr, desc, err) {
                        console.error("ERROR: ", err);
                        $("#updateStatusModal").modal('hide');
                    }
                });
            });

        }

        function setupMultiselect() {
            const $statusFilter = $('#statusFilter');

            if (!$statusFilter.length) return;

            $statusFilter.multiselect({
                includeSelectAllOption: true,
                enableFiltering: true,
                maxHeight: 200,
                buttonWidth: '100%',
                nonSelectedText: 'Select statuses'
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
            $(document).ready(function () {
                setupMultiselect();
            });
            initClickEvent();
        };
    }

    return new init();
})(app, jQuery);