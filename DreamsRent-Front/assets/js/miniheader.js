//nav
$(".nav-icon").on("click", function(){
    $(".open-nav").css("transform", "translateX(0px)");
    $("html").css("overflow-y", "hidden");
})
$(".open-nav .top i").on("click", function(){
    $(".open-nav").css("transform", "translateX(-300px)");
    $("html").css("overflow-y", "scroll");
})


//login
$(".pro-user .btn").on("click",()=>{
    let drop = $(".drop-login")
    drop.slideToggle(500)
})