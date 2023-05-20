let announcementstLi = document.querySelector("#li-announcements")
let accountDetailsLi = document.querySelector("#account-detail")

let announcementsMain = document.querySelector(".about-announcements")
let accountDetailsMain = document.querySelector(".account-details")

announcementstLi.addEventListener("click", function(){
    announcementstLi.classList.add("active-li")
    accountDetailsLi.classList.remove("active-li")

    announcementsMain.classList.add("active-main")
    accountDetailsMain.classList.remove("active-main")
})

accountDetailsLi.addEventListener("click", function(){
    accountDetailsLi.classList.add("active-li")
    announcementstLi.classList.remove("active-li")

    accountDetailsMain.classList.add("active-main")
    announcementsMain.classList.remove("active-main")
})

let announcementsHead = document.querySelector(".announcements-head")
let pendingHead = document.querySelector(".pending-head")
let rentedHead = document.querySelector(".rented-head")

let announcementsBody = document.querySelector(".announcements-body")
let pendingBody = document.querySelector(".pending-body")
let rentedBody = document.querySelector(".rented-body")


announcementsHead.addEventListener("click", function(){
    announcementsHead.classList.add("active-li")
    pendingHead.classList.remove("active-li")
    rentedHead.classList.remove("active-li")

    announcementsBody.classList.remove("d-none")
    pendingBody.classList.add("d-none")
    rentedBody.classList.add("d-none")
})

pendingHead.addEventListener("click", function(){
    pendingHead.classList.add("active-li")
    announcementsHead.classList.remove("active-li")
    rentedHead.classList.remove("active-li")

    pendingBody.classList.remove("d-none")
    announcementsBody.classList.add("d-none")
    rentedBody.classList.add("d-none")
})

rentedHead.addEventListener("click", function(){
    rentedHead.classList.add("active-li")
    announcementsHead.classList.remove("active-li")
    pendingHead.classList.remove("active-li")

    rentedBody.classList.remove("d-none")
    announcementsBody.classList.add("d-none")
    pendingBody.classList.add("d-none")
})