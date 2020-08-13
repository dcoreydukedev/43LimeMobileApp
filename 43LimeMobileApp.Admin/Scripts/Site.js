$(function () {
    //Preloader Spinner
    if ($('#preloader').length) {
        $('#preloader').delay(550).fadeOut('slow', function () { $(this).remove(); });
    }
    Cookies.set("43LimeCookie", "43 Lime Cookie");
});

// Q-Tip Config
window._qtipConfig = {
    content: {
        attr: 'data-tooltip'
    },
    show: {
        event: 'mouseover'
    },
    hide: {
        event: 'mouseout'
    },
    style: {
        classes: 'qtip-blue qtip-shadow'
    }
};

// Show Toastr Notifications
window.showToastrNotification = function ($type, $msg) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": 'toast-top-full-width',
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": '500',
        "hideDuration": '1000',
        "timeOut": '5000',
        "extendedTimeOut": '1000',
        "showEasing": 'swing',
        "hideEasing": 'linear',
        "showMethod": 'fadeIn',
        "hideMethod": 'fadeOut'
    };

    if (typeof ($msg) === 'undefined') {


        if ($type === 'success') {
            toastr.success($msg).css({ "width": 'auto', "max-width": '88em' });
            return;
        } else if ($type === 'failure') {
            toastr.error($msg).css({ "width": 'auto', "max-width": '88em' });
            return;
        } else {
            //Do Nothing
            return;
        }

    };

    // Show Documents in the HTMl Folder
    window.showHTMLDocument = function ($document) {
        window.open(`/html/${$document}.htm`, '_blank', 'toolbar=no,scrollbars=yes,resizable=yes,top=0,left=0,width=900,height=450', false);
    };

}
