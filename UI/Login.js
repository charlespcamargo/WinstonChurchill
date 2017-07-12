var Login = function () {
    return {
        init: function () {
            this.eventos();
        },

        eventos: function () {

            $('#btnLogin').click(function () {
                Login.logar();
            });

            $(document).keypress(function (e) {
                var keycode = (e.keyCode ? e.keyCode : e.which);
                if (keycode == '13') {
                    Login.logar();
                }
            });
        },

        logar: function () {
            if ($('#frmLogin').ehValido() == false)
                return;

            var jsonSend = $('#frmLogin').obterJson();

            function fnSuccess(data) {
                if (data && data.token_type && data.access_token) {
                    $.cookie('Authorization', data.token_type + ' ' + data.access_token);
                    window.location.href = '/pages/dashboard.aspx';
                }
            }

            App.blockUI($("#frmLogin"));

            $.ajax({
                global: false,
                type: 'POST',
                url: HelperJS.getBaseURL() + 'oauth2/token',
                cache: false,
                data: 'username=' + jsonSend.UserName + '&password=' + jsonSend.Password + '&grant_type=password&client_id=e84a2d13704647d18277966ec839d39e:CgP7NyLXtaGmyOgjj3sUMwmAlrSKqa5JyZ4P1OlfQeM',
                success: function (data) {
                    fnSuccess(data);
                    App.unblockUI($("#frmLogin"));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (jqXHR.responseJSON && jqXHR.responseJSON.error_description) {
                        HelperJS.showError(jqXHR);
                        App.unblockUI($("#frmLogin"));
                    }
                    else {
                        jqXHR.responseJSON = new Object();
                        jqXHR.status = 400;
                        jqXHR.responseJSON.Message = 'Erro ao tentar fazer o login. Contate o administrador.';
                        HelperJS.showError(jqXHR);
                        App.unblockUI($("#frmLogin"));
                    }
                }
            });
        }
    };
}();