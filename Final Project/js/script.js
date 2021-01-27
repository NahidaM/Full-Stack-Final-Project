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

  $(document).on("click", ".mobile-navbar ul li a", function () {
    if ($(this).children("i").hasClass("fa-caret-right")) {
      $(this)
        .children("i")
        .removeClass("fa-caret-right")
        .addClass("fa-sort-down");
    } else {
      $(this)
        .children("i")
        .removeClass("fa-sort-down")
        .addClass("fa-caret-right");
    }
    $(this).parent().next().slideToggle();
  });
});

// Set the original/default language
var lang = "en";
var currentClass = "currentLang";

// Load the language lib
google.load("language", 1);

// When the DOM is ready....
window.addEvent("domready", function () {
  // Retrieve the DIV to be translated.
  var translateDiv = document.id("languageBlock");
  // Define a function to switch from the currentlanguage to another
  var callback = function (result) {
    if (result.translation) {
      translateDiv.set("html", result.translation);
    }
  };
  // Add a click listener to update the DIV
  $$("#languages a").addEvent("click", function (e) {
    // Stop the event
    if (e) e.stop();
    // Get the "to" language
    var toLang = this.get("rel");
    // Set the translation into motion
    google.language.translate(translateDiv.get("html"), lang, toLang, callback);
    // Set the new language
    lang = toLang;
    // Add class to current
    this.getSiblings().removeClass(currentClass);
    this.addClass(currentClass);
  });
});
