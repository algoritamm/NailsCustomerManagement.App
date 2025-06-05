(function (app, $) {
    function init() {
        app.scheduler = app.scheduler || {};
        app.scheduler.index = this;

        let baseUrl = window.location.pathname;

        

        function initClickEvent() {
            //Buttons
            $(document).on("click", "#btnPrevDay", function (e) {
                changeDate(-1, e);
            });

            $(document).on("click", "#btnNextDay", function (e) {
                changeDate(1, e);
            });
            //Calendar
            $('#datePicker').on("change.datetimepicker", function (e) {
                const selectedDate = e.date ? e.date.format('YYYY-MM-DD') : $('#inputDate').val();
                setDayAndDateInput(selectedDate);
                loadAppointments(selectedDate);
            });
        }

        function changeDate(offsetDays, e) {
            const selectedDate = e.date ? e.date.add(offsetDays, 'days').format('YYYY-MM-DD') : moment($('#inputDate').val()).add(offsetDays, 'days').format('YYYY-MM-DD');
            setDayAndDateInput(selectedDate);
            loadAppointments(selectedDate);
        }

        function loadAppointments(date) {
            $.get(`${baseUrl}?date=${date}`, function (data) {
                $("body").html(data);
            });
        }

        function setDayAndDateInput(dateStr) {
            const dayOfWeekSpan = document.getElementById('dayOfWeek');
            const selectedDate = new Date(dateStr);
            if (!isNaN(selectedDate)) {
                const options = { weekday: 'long' };
                const day = selectedDate.toLocaleDateString('en-US', options); // 'sr-RS'
                dayOfWeekSpan.textContent = `${day}`;
            } else {
                dayOfWeekSpan.textContent = '';
            }
        }

        this.init = function (obj) {
            // Init day of week
            const initialDate = $('#inputDate').val();
            if (initialDate) {
                setDayAndDateInput(initialDate);
            }
            //init events
            initClickEvent();
        };
    }

    return new init();
})(app, jQuery);