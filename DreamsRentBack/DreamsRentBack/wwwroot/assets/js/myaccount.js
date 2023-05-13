let accountDetailsLi = document.querySelector("#account-detail")
let wishlistLi = document.querySelector("#li-wishlist")

let accountDetailsMain = document.querySelector(".account-details")
let wishlistMain = document.querySelector(".wishlist")

wishlistLi.addEventListener("click", function(){
    wishlistLi.classList.add("active-li")
    accountDetailsLi.classList.remove("active-li")

    accountDetailsMain.classList.remove("active-main")
    wishlistMain.classList.add("active-main")
})

accountDetailsLi.addEventListener("click", function(){
    accountDetailsLi.classList.add("active-li")
    wishlistLi.classList.remove("active-li")

    wishlistMain.classList.remove("active-main")
    accountDetailsMain.classList.add("active-main")
})
