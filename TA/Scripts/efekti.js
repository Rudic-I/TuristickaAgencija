//smooth scroll
$('nav ul li a').on('click', function () {
    $('html, body').animate({
        scrollTop: $(this.hash).offset().top
    }, 1000);
});

//nav change on scroll
$(window).scroll(function () {
    if (document.body.scrollTop > 80 || document.documentElement.scrollTop > 80) {
        document.getElementsByTagName("nav")[0].className = "navbar-fixed-top";
    }
    else {
        document.getElementsByTagName("nav")[0].className = "";
    }
});