$(document).ready(function () {
  // HEADER

  $(document).on("click", "#search", function () {
    $(this).next().toggle();
  });

  $(document).on("click", "#mobile-navbar-close", function () {
    $(this).parent().removeClass("active");
  });
  $(document).on("click", "#mobile-navbar-show", function () {
    $(".mobile-navbar").addClass("active");
  });

  // SLIDER

  $(document).ready(function () {
    $(".slider").owlCarousel({
      items: 1,
      loop: true,
      autoplay: true,
    });
  });

  // SERVICES SECTION IN ABOUT PAGE
  $(".owl-2").owlCarousel({
    loop: true,
    margin: 10,
    nav: true,
    responsive: {
      0: {
        items: 2,
      },
      600: {
        items: 3,
      },
      1000: {
        items: 5,
      },
    },
  });
});
