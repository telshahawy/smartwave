
// show password button
$(document).ready(function ($) {
  try {

    function showPass() {
      var eye = document.getElementById("loginPass");
      if (eye.type === "password") {
        eye.type = "text";
      } else {
        eye.type = "password";
      }
    }

    $(".sidebar a").onClick(function () {
      $(this).toggleClass("active").siblings().removeClass("active");
      console.log('jjjjj');
    })

    // hide notification alert 
    $(".notifications .notification .close").click(function () {
      $(this).parent().parent().fadeOut().removeClass("d-flex")
    })
// toggle map point content
$(".point>img, .map .point .inner-content .icon-cross").click(function () {
  $(".map .point .inner-content ").toggleClass("d-flex")
})
    // toggle changing user forms 
    $(".for-user").click(function () {
      $(".change-username").addClass("show")
    })
    $(".for-pass").click(function () {
      $(".change-password").addClass("show")
    })
    $(document).on("click", function (event) {
      var $trigger = $(".dropdown");
      if ($trigger !== event.target && !$trigger.has(event.target).length) {
        $(".manual-dropdown").removeClass("show");
      }
    });

    // sidebar change active 
    $(".sidebar a,.track-chemist ul li").click(function () {
      $(this).toggleClass("active").siblings().removeClass("active");
    })

    // get selected image link 
    function readURL(input) {
      if (input.files && input.files[0]) {
        var reader = new FileReader();
        $(".selected-item").fadeIn();
        reader.onload = function (e) {
          $('.imgFile').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
        $(".img-url").text(input.files[0].name)
      }


    }

    $(".img-input").change(function () {
      readURL(this);

    });

    // chemists sidebar
    $(".side-toggler").click(function () {
      let mySelector = `#${$(this).data("id")}`;
      $(mySelector).addClass("expanded")

    })
    $(".chemists-sidebar .icon-cross").click(function () {
      $(".chemists-sidebar").removeClass("expanded")
    })

    // multi select2 
    $('.select2-multiple').select2();
  } catch (error) {
    console.log("check js")
  }
});