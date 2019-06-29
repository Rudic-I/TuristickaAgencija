$(document).ready(function () {
    var heightDocument = $(document).height(); //ako je manji od vh, vraca vh
    var heightWindow = $(window).height();
    if (heightDocument == heightWindow) {
        $('footer').css({ "position": "fixed", "bottom": 0 });
    }
})