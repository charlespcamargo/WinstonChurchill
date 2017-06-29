var Login = function () {
    return {
        init: function () {
            this.eventos();
        },

        eventos: function () {

            $('#btnLogin').click(function () {
                if ($('#frmLogin').ehValido() == false)
                    return;
                Login.logar();
            });

            //$('.forget-form input').keypress(function (e) {
            //    if (e.which == 13) {
            //        return false;
            //    }
            //});

            jQuery('#forget-password').click(function () {
                jQuery('.login-form').hide();
                jQuery('.forget-form').show();
            });

            jQuery('#back-btn').click(function () {
                jQuery('.login-form').show();
                jQuery('.forget-form').hide();
            });
        },

        logar: function () {
            var jsonSend = $('#frmLogin').obterJson();

            function fnSuccess(data) {
                if (data && data.token_type && data.access_token) {
                    $.cookie('Authorization', data.token_type + ' ' + data.access_token);
                    window.location.href = '/pages/dashboard.aspx';
                }
            }

            $.ajax({
                global: false,
                type: 'POST',
                url: HelperJS.getBaseURL() + 'oauth2/token',
                cache: false,
                //headers: {
                //    'Content-type': 'application/x-www-form-urlencoded',
                //    'accept': 'application/json'

                //},
                // contentType: 'application/json',
                //accept: 'application/json',
                data: 'username=' + jsonSend.UserName + '&password=' + jsonSend.Password + '&grant_type=password&client_id=e84a2d13704647d18277966ec839d39e:CgP7NyLXtaGmyOgjj3sUMwmAlrSKqa5JyZ4P1OlfQeM',
                success: function (data) {
                    fnSuccess(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR.responseJSON.error_description);
                    // HelperJS.showError(jqXHR);
                }
            });

            //HelperJS.callApi({
            //    url: "/autenticar",
            //    type: "POST",
            //    data: jsonSend,
            //    functionOnSucess: fnSuccess,
            //    functionOnError: HelperJS.showError
            //});
        }
    };
}();