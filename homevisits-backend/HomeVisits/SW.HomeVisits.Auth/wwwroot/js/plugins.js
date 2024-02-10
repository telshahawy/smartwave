// show password button
function showPass() {
  var eye = document.getElementById("loginPass");
  if (eye.type === "password") {
    eye.type = "text";
  } else {
    eye.type = "password";
  }
}

// hide notification alert 
$(".notifications .notification .close").click(function(){
  $(this).parent().parent().fadeOut().removeClass("d-flex")
})

try {
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
$(".sidebar a").click(function(){
  $(this).toggleClass("active").siblings().removeClass("active");
})




} catch (error) {
  console.log("check js")
}
