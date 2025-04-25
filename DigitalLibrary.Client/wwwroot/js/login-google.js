window.googleLogin = {
    promptLogin: function (clientId, dotNetHelper) {
        google.accounts.id.initialize({
            client_id: clientId,
            callback: function (response) {
                dotNetHelper.invokeMethodAsync("OnGoogleLoginSuccess", response.credential);
            }
        });

        google.accounts.id.prompt();
    }
};