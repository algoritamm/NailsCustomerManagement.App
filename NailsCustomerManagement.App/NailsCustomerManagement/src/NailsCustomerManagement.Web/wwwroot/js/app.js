var app = (function () {
    function init() {

        this.appointment = {
            index: {},
            viewAppointment: {}
        };

        this.user = {
            login: {}
        };

        this.scheduler = {
            index: {}
        }

        this.isModalFormValid = function (modalElement) {
            waitPendingRequest();
            if (!modalElement.find('form').valid()) {
                return false;
            }
            return true;

            function waitPendingRequest() {
                if (modalElement.find('form').validate().pendingRequest !== 0) {
                    waitPendingRequest();
                }
            }           
        }

        this.showLoader = function () {
            $('body').addClass('loading');
        };

        this.hideLoader = function () {
            $('body').removeClass('loading');
        }

        this.disableButton = function (e) {
            $(e).attr("disabled", true);
        }

        this.enableButton = function (e) {
            $(e).attr("disabled", false);
        }

        this.isFormValid = function (formElement) {
            waitPendingRequest();

            if (!formElement.valid()) {
                return false;
            }
            return true;

            function waitPendingRequest() {
                while (formElement.validate().pendingRequest !== 0) {
                    waitPendingRequest();
                }
            }
        }

        function initLoader() {
            $(document).on({
                ajaxStart: function () { $('body').addClass('loading'); },
                ajaxStop: function () { $('body').removeClass('loading'); },
                submit: function () { $('body').addClass('loading'); }
            });
        }
        
        this.init = function (obj) {
            this.sharedLocalizer = obj.sharedLocalizer;
            //Init datetime picker
            $('.datepickers').datetimepicker({
                format: 'YYYY-MM-DD',
                defaultDate: moment() // today
            });
            initLoader();
        };
    }

    return new init();
})(jQuery);