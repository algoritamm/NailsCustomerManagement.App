(function (app, $) {
    function init() {
        app.user.login = this;

        function initClickEvents() {
            $(document).on('input', '#SelectedLanguageCountryId', function () {
                $('#LoginResult').closest('div').addClass('hidden');
                $('#LoginResult').text('');

                const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');

                var culture = new FormData();
                culture.append('cultureId', $('#SelectedLanguageCountryId').val());

                fetch('/User/SetLanguage', {
                    method: 'POST',
                    headers: {
                        'RequestVerificationToken': token
                    },
                    body: culture
                })
                    .then(response => response.json())
                    .then(data => console.log("Global language set to: ", data.Culture))
                    .catch(error => console.error('Error:', error));
            });

            $(document).on('click', '#loginBtn', function (e) {
                e.preventDefault();
                $('#LoginResult').closest('div').addClass('hidden');
                $('#LoginResult').text('');

                const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');

                if (!app.isFormValid($('#loginForm')))
                    return;

                const data = {
                    Username: $('#Username').val(),
                    Password: $('#Password').val(),
                    SelectedLanguageCountryId: $('#SelectedLanguageCountryId').val()
                };

                fetch('/User/UserLogin', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': token
                    },
                    body: JSON.stringify(data)
                })
                    .then(response => {
                        return response.text().then(message => {
                            if (!response.ok) {
                                window.location.href = `/Error/${response.status}`;
                                return;
                            }

                            if (message) {
                                $('#LoginResult').closest('div').removeClass('hidden');
                                $('#LoginResult').text(message);
                                return;
                            }

                            window.location.href = "/Home/Index";
                        });
                    })
                    .catch(error => {
                        console.error('Error:', error);
                        window.location.href = `/Error/500`;
                    });
            });


        }

        this.init = function (obj) {
            initClickEvents();
        };
    }

    return new init();
})(app, jQuery);