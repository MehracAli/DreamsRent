const dateInput = document.querySelectorAll('.date-now');
dateInput.forEach(Element=>{
    const now = new Date();
    Element.value = now.toISOString().slice(0, 10);
})

const timeInput = document.querySelectorAll('.time-now');
timeInput.forEach(Element=>{
    const now = new Date();
    Element.value = now.toTimeString().slice(0, 5);
})

let servicesSect = document.getElementById("services")
let locationHead = document.querySelector("#services .location-head")
let pickUpHead = document.querySelector("#services .pick-up-head")
let bookUpHead = document.querySelector("#services .book-head")
let headImg = document.querySelectorAll(".head img")

$(window).scroll(function () {
  var specificSection = $("#services");
  var specificScrollPosition = specificSection.offset().top - 300;

  if ($(this).scrollTop() >= specificScrollPosition) {
      
    locationHead.classList.add("location-head-moving")
    locationHead.style.opacity = "1"
    locationHead.classList.add(".threeD-rotate")
  
    setTimeout(() => {
      locationHead.style.background = "#fff"
    }, 500)
  
    setTimeout(() => {
      pickUpHead.classList.add("pick-up-head-moving")
      pickUpHead.style.opacity = "1"
    }, "500");
  
    setTimeout(() => {
      pickUpHead.style.background = "#fff"
    }, 1000)
    
    setTimeout(() => {
      bookUpHead.classList.add("book-head-moving")
      bookUpHead.style.opacity = "1"
    }, "1000")
  
    setTimeout(() => {
      bookUpHead.style.background = "#fff"
    }, 1500)
  
    headImg.forEach(Element=>{
      Element.style.opacity = "1"
    })

      $(window).off("scroll");
  }
});
