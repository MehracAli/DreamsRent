$(".responsive").slick({
    dots: false,
    infinite: true,
    variableWidth: true,
    speed: 300,
    autoplay: true,
    smartSpeed: 2000,
    prevArrow: '<i class="fa-solid fa-arrow-right left_arrow"></i>',
    nextArrow: '<i class="fa-solid fa-arrow-left right_arrow"></i>',
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3,
          infinite: true,
          dots: false,
        },
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2,
          dots: false,
          arrows: false
        },
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
        },
      },
    ],
  });

$('#populars .owl-carousel').owlCarousel({
    nav: true,
    navText: ["<i class='fa-solid fa-arrow-left'></i>", "<i class='fa-solid fa-arrow-right'></i>"],
    autoplay: true,
    smartSpeed: 2000,
    autoPlayTimeout: 2000,
    autoplayHoverPause: true,
    loop:true,
    infinite:true,
    margin:10,
    responsiveClass:true,
    dots:false,
    responsive:{
        0:{
            items:1,
            nav:true
        },
        600:{
            items:1,
            nav:false
        },
        1000:{
            items:3,
            nav:true,
        }
    }
})

$('#testimonials .owl-carousel').owlCarousel({
    loop:true,
    dots:true,
    dotsEach: 1,
    nav:false,
    navText: ["", ""],
    autoplay: true,
    smartSpeed: 2000,
    autoPlayTimeout: 2000,
    autoplayHoverPause: true,
    responsiveClass:true,
    responsive:{
        0:{
            items:1,
            nav:true
        },
        600:{
            items:1,
            nav:false
        },
        1000:{
            items:2,
            nav:true,
            loop:false
        }
    }
})

$('#detail-body .owl-carousel').owlCarousel({
  loop:true,
  nav: true,
  navText: ["<i class='fa-solid fa-arrow-left'></i>", "<i class='fa-solid fa-arrow-right'></i>"],
  autoplay: true,
  smartSpeed: 2000,
  autoPlayTimeout: 2000,
  autoplayHoverPause: true,
  margin:10,
  responsiveClass:true,
  dots:false,
  responsive:{
      0:{
          items:1,
          nav:true
      },
      600:{
          items:1,
          nav:false
      },
      1000:{
          items:3,
          nav:true,
      }
  }
})

$('.slider-for').slick({
  accessibility:true,
  slidesToShow: 1,
  slidesToScroll: 1,
  arrows: true,
  fade: true,
  asNavFor: '.slider-nav'
});

$('.slider-nav').slick({
  slidesToShow: 4,
  slidesToScroll: 1,
  infinite: true,
  asNavFor: '.slider-for',
  arrows: false,
  centerMode: false,
  focusOnSelect: true
});

