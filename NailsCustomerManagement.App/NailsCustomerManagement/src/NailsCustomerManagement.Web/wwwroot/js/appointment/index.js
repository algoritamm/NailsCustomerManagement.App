(function (app, $) {
    function init() {
        app.appointment.index = this;

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