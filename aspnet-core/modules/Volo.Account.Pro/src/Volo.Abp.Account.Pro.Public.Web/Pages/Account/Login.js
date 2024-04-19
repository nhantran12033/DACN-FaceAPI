$(function () {
    $("#PasswordVisibilityButton").click(function (e) {
        let button = $(this);
        let passwordInput = $('#password-input');
        if (!passwordInput) {
            return;
        }

        if (passwordInput.attr("type") === "password") {
            passwordInput.attr("type", "text");
        }
        else {
            passwordInput.attr("type", "password");
        }

        let icon = $("#PasswordVisibilityButton");
        if (icon) {
            icon.toggleClass("fa-eye-slash").toggleClass("fa-eye");
        }
    });

    // CAPS LOCK CONTROL
    const password = document.getElementById('password-input');
    const passwordMsg = document.getElementById('capslockicon'); 
    password.addEventListener('keyup', e => {
        if(typeof e.getModifierState === 'function'){
            passwordMsg.style = e.getModifierState('CapsLock') ? 'display: inline' : 'display: none';
        }        
    });

});
