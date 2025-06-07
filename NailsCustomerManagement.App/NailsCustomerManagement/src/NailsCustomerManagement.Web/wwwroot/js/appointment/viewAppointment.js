(function (app, $) {
    function init() {
        app.appointment.viewAppointment = this;

        const divInsertAppointment = $("#insertAppointmentDiv");
        const divUpdateAppointmentItem = $('#updateAppointmentItemDiv');
        const divDeleteAppointmentItem = $('#deleteAppointmentItemDiv');

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

            // Open modal - insert
            $(document).on("click", ".btnInsertAppointment", function () {
                const id = $("#AppointmentId").val();

                if (divInsertAppointment.html().length == 0) {
                    $.ajax({
                        type: 'GET',
                        url: '/Appointment/_InsertAppointment',
                        data: { AppointmentId: id },
                        success: function (result) {
                            divInsertAppointment.append(result);
                            const $model = $('#insertAppointmentModal');

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
                                clearValidation: true
                            });
                        },
                        error: function (xhr, desc, err) {
                            $('#insertAppointmentModal').modal('hide');
                            console.log(err, desc)
                        }
                    });
                }
                else {
                    $('#insertAppointmentModal').SetModalInputs({
                        clearInputs: true,
                        clearValidation: true
                    });

                    const now = moment();
                    $('#insertAppointmentModal').find('#InsertAppointment_ItemDate').val(now.format('YYYY-MM-DD'));
                    $('#insertAppointmentModal').find('#InsertAppointment_ItemTime').val(now.format('HH:mm'));

                    $('#insertAppointmentModal').modal('toggle');
                }
            });

            $(document).on('click', "#btnConfirmeInsertAppointment", function () {
                const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');

                if (!app.isModalFormValid($("#insertAppointmentModal")))
                    return;
            
                const formData = {
                    AppointmentId: $("#AppointmentId").val(),
                    CustomerId: $("#CustomerId").val(),
                    SelectedServiceTypeId: $("#InsertAppointment_SelectedServiceTypeId").val(),
                    SelectedPaymentTypeId: $("#InsertAppointment_SelectedPaymentTypeId").val(),
                    ItemDate: $("#InsertAppointment_ItemDate").val(),
                    ItemTime: $("#InsertAppointment_ItemTime").val(),
                    AppointmentName: $("#InsertAppointment_AppointmentName").val()
                }

                $.ajax({
                    type: "POST",
                    url: "/Appointment/InsertAppointmentItem",
                    headers: {
                        'RequestVerificationToken': token
                    },
                    data: formData,
                    success: function (response) {
                        $("#insertAppointmentModal").modal('hide');
                        $('#loading-indicator').show();
                        setTimeout(function () {
                            $('#loading-indicator').hide();
                            window.location.reload();
                        }, 2000);
                    },
                    error: function (xhr, desc, err) {
                        console.error("ERROR: ", err);
                        $("#insertAppointmentModal").modal('hide');
                    }
                });
            });

            // Open modal - update
            $(document).on('click', '#updateAppointmentItemBtn', function () {
                let itemId = $(this).data('item-id');

                $.ajax({
                    type: "GET",
                    url: "/Appointment/_UpdateAppointment",
                    data: { AppointmentItemId: itemId },
                    success: function (result) {
                        if (divUpdateAppointmentItem.html().length != 0) divUpdateAppointmentItem.html("");

                        divUpdateAppointmentItem.append(result);
                        const $model = $('#updateAppointmentItemModal');

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
                            clearValidation: true
                        });
                    },
                    error: function (xhr, desc, err) {
                        console.error("ERROR: ", err);
                        $("#updateAppointmentItemModal").modal('hide');
                    }
                });
            });

            $(document).on('click', "#btnConfirmeUpdateAppointment", function () {
                const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');

                if (!app.isModalFormValid($("#updateAppointmentItemModal")))
                    return;

                const formData = {
                    AppointmentItemId: $("#AppointmentItemId").val(),
                    SelectedStatusId: $("#SelectedStatusId").val(),
                    AmountPaid: $("#AmountPaid").val(),
                    SelectedPaymentTypeId: $("#SelectedPaymentTypeId").val(),
                    ItemDate: $("#ItemDate").val(),
                    ItemTime: $("#ItemTime").val(),
                    AppointmentName: $("#AppointmentName").val()
                }

                $.ajax({
                    type: "POST",
                    url: "/Appointment/UpdateAppointmentItem",
                    headers: {
                        'RequestVerificationToken': token
                    },
                    data: formData,
                    success: function (response) {
                        $("#updateAppointmentItemModal").modal('hide');
                        $('#loading-indicator').show();
                        setTimeout(function () {
                            $('#loading-indicator').hide();
                            window.location.reload();
                        }, 2000);
                    },
                    error: function (xhr, desc, err) {
                        console.error("ERROR: ", err);
                        $("#updateAppointmentItemModal").modal('hide');
                    }
                });
            });

            // Open modal - delete
            $(document).on('click', '#deleteAppointmentItemBtn', function () {
                let itemId = $(this).data('item-id');

                $.ajax({
                    type: "GET",
                    url: "/Appointment/_DeleteModal?id=" + itemId,
                    success: function (result) {

                        divDeleteAppointmentItem.html("");
                        divDeleteAppointmentItem.append(result);

                        $('#deleteModal').modal('show'); 
                    },
                    error: function (xhr, desc, err) {
                        console.error("ERROR: ", err);
                    }
                });
            });

            $(document).on('click', '#btnConfirmDelete', function () {
                let id = $(this).data('id');
                const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');

                $.ajax({
                    type: "POST",
                    url: "/Appointment/DeleteAppointmantItem?id=" + id,
                    headers: {
                        'RequestVerificationToken': token
                    },
                    success: function (result) {
                        $('#deleteModal').modal('hide');
                        $('#loading-indicator').show();
                        setTimeout(function () {
                            $('#loading-indicator').hide();
                            window.location.reload();
                        }, 2000);
                    },
                    error: function (xhr, desc, err) {
                        $('#deleteModal').modal('hide');
                        console.error("ERROR: ", err);
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