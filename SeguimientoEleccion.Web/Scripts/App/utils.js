var APP = APP || {};
APP.utils = function () {
    var ns = {};

    var msgTarget = {};
    msgTarget = $('.messageOutput');
    function showMessage(message, params) {
        var msgText = message.msg,
            msgStatus = message.status || 'info';
        if (Object.prototype.toString.call(params).indexOf('Array')>0) {
            for (var i = 0; i < params.length; i++) {
                msgText = msgText.replace(new RegExp('\\{' + i + '\\}'), params[i]);
            }
        }
        msgTarget.each(function (index, element) {
            var target = $(element);
            target.html(msgText);
            target.addClass('alert');
            target[0].className = msgTarget[0].className.replace(/\balert-.*?\b/g, '');
            target.addClass('alert-' + msgStatus);
        });
    };
    ns.showMessage = showMessage;
    
    ns.is_touch_device = Modernizr.touch && !isCurve9300();

    function isCurve9300() {
        //algunos curve se reportan como touch por error!!
        var ua = navigator.userAgent;
        if (ua.indexOf("BlackBerry") >= 0) {
            if (ua.indexOf("Version/") >= 0) {
                // BlackBerry 6 and 7
                var model = ua.match(/BlackBerry\s[0-9]*/);
                if (model) {
                    var model_number = model[0].match(/[0-9]+/);
                    if (model_number) model_number = model_number[0];
                    pos = ua.indexOf("Version/") + 8;
                    os_version = ua.substring(pos, pos + 3);

                    if (os_version === '6.0' && model_number === '9300') {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    
    return ns;
}();
