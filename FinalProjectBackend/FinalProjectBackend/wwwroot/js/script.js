$(document).ready(function () {
    let skip = 8;
    let count = $("#pro-count").val();
    $(document).on('click', '#load-more', function () {
        let loadSpinner = $('#load-spinner');
        loadSpinner.removeClass('d-none');
        setTimeout(() => {
            loadSpinner.addClass("d-none")
        }, 1000);
        setTimeout(() => {
            $.ajax({
                url: '/Main/LoadMore?skip=' + skip,
                type: 'GET',
                success: function (res) {
                    $("#product-list").append(res);
                    skip += 8;
                    if (skip >= count) {
                        $("#load-more").remove()
                    }
                }
            })
        }, 900)

    });

    $(document).ready(function () {
        $(document).on('keyup', '#search-input', function () {

            let searchInput = $(this).val().trim();
            $("#search-list li").remove();
            if (searchInput.length > 0) {

                $.ajax({
                    url: "/Home/Search?search=" + searchInput,
                    type: "Get",
                    success: function (res) {
                        $("#search-list").append(res);
                    }
                });
            }
        })
    })

    $(document).on('click', '.add-basket', function () {
        let proId = $(this).find('input').val();
        let basketCount = $('#basket-count');
        $.ajax({
            url: '/Main/AddBasket?id=' + proId,
            type: 'GET',
            success: function (res) {
                basketCount.text(res)
            }
        })
    });

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
