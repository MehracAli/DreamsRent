$("#faq .accordion-title").on("click", function () {
    let next = $(this).next();
    let icon = $(this).find("i");
  
    next.slideToggle();
    icon.toggleClass("active-i");
  
    $("#faq .accordion-desc").not(next).slideUp();
    $("#faq .accordion-title i").not(icon).removeClass("active-i");
});

$("#list .accordion-title").on("click", function () {
    let next = $(this).next();
    let icon = $(this).find("i");
  
    next.slideToggle();
    icon.toggleClass("active-i");
  
    $("#faq .accordion-desc").not(next).slideUp();
    $("#faq .accordion-title i").not(icon).removeClass("active-i");
});