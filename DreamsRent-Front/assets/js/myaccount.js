let accountDetailsLi = document.querySelector("#account-detail")
let wishlistLi = document.querySelector("#li-wishlist")
let orderLi = document.querySelector("#li-order-history")

let accountDetailsMain = document.querySelector(".account-details")
let wishlistMain = document.querySelector(".wishlist")
let orderMain = document.querySelector(".order-history")

wishlistLi.addEventListener("click", function(){
    wishlistLi.classList.add("active-li")
    accountDetailsLi.classList.remove("active-li")
    orderLi.classList.remove("active-li")

    wishlistMain.classList.add("active-main")
    accountDetailsMain.classList.remove("active-main")
    orderMain.classList.remove("active-main")
})

accountDetailsLi.addEventListener("click", function(){
    accountDetailsLi.classList.add("active-li")
    wishlistLi.classList.remove("active-li")
    orderLi.classList.remove("active-li")


    accountDetailsMain.classList.add("active-main")
    wishlistMain.classList.remove("active-main")
    orderMain.classList.remove("active-main")

})

orderLi.addEventListener("click", function(){
    orderLi.classList.add("active-li")
    wishlistLi.classList.remove("active-li")
    accountDetailsLi.classList.remove("active-li")


    orderMain.classList.add("active-main")
    wishlistMain.classList.remove("active-main")
    accountDetailsMain.classList.remove("active-main")
})
