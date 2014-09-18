var APP = APP || {};
APP.utils = function () {
    var ns = {};
    ns.is_touch_device = function() {
        return (('ontouchstart' in window)
            || (navigator.MaxTouchPoints > 0)
            || (navigator.msMaxTouchPoints > 0));
    };

    return ns;
}();
