$(document).ready(function() {
  $('.nav-link-with-icon').hover(function() {
    $(this).siblings('.sub-menu').toggle();
  });
});
