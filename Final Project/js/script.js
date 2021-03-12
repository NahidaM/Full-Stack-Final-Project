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
    nav: false,
    dots: false,
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

  // SHOW PASSWORD IN LOGIN AND REGISTOR PAGE
  if ($("#login").length || $("#register").length) {
    $(".show-password").click(function () {
      if ($("#password").attr("type") == "password") {
        $("#password").attr("type", "text");
      } else {
        $("#password").attr("type", "password");
      }
    });
  }

  // PRODUCT

  $(document).on("click", ".categories", function (e) {
    e.preventDefault();
    $(this).next().next().slideToggle();
  });

  $(document).on("click", ".category li a", function (e) {
    e.preventDefault();
    let category = $(this).attr("data-id");
    let products = $(".card");

    products.each(function () {
      if (category == $(this).attr("data-id")) {
        $(this).parent().fadeIn();
      } else {
        $(this).parent().hide();
      }
    });
    if (category == "all") {
      products.parent().fadeIn();
    }
  });
});

// FORM VALIDATION FOR JOIN-US PAGE
function myForm() {
  var name = document.forms["RegForm"]["Name"];
  var email = document.forms["RegForm"]["EMail"];
  var phone = document.forms["RegForm"]["Telephone"];
  var what = document.forms["RegForm"]["Blood"];
  var address = document.forms["RegForm"]["Address"];

  if (name.value == "") {
    window.alert("Zəhmət olmasa ad, soyadınızı qeyd edin.");
    name.focus();
    return false;
  }

  if (address.value == "") {
    window.alert("Zəhmət olmasa adresinizi qeyd edin.");
    address.focus();
    return false;
  }

  if (email.value == "") {
    window.alert("Zəhmət olmasa mailinizi qeyd edin.");
    email.focus();
    return false;
  }

  if (phone.value == "") {
    window.alert("Zəhmət olmasa telefon nömrənizi qeyd edin.");
    phone.focus();
    return false;
  }

  if (what.selectedIndex < 1) {
    alert("Zəhmət olmasa qan qrupunuzu seçin.");
    what.focus();
    return false;
  }

  return true;
}
