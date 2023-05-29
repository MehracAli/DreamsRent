let step1 = document.querySelector(".step-1")
let step2 = document.querySelector(".step-2")
let goStep2 = document.querySelector(".go-step-2")
let bookingDetails = document.querySelector(".booking-details")
let paymentDetails = document.querySelector(".payment-details")

goStep2.addEventListener("click", function(){
    step2.classList.add("step-active")
    step1.classList.remove("step-active")
    paymentDetails.classList.remove("d-none")
    bookingDetails.classList.add("d-none")
})

//step1.addEventListener("click", function(){
//    step1.classList.add("step-active")
//    step2.classList.remove("step-active")
//    bookingDetails.classList.remove("d-none")
//    paymentDetails.classList.add("d-none")
//})