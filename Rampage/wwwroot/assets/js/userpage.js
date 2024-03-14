// Basket Start
let basketClick = document.querySelector(".basket_click");
let basketContainer = document.querySelector(".basket-container");

basketClick.addEventListener("click", () => {
    basketContainer.style.transform = "translateX(0)";
});

let closeBtnClick = document.querySelector(".close_btn_click");
closeBtnClick.addEventListener("click", () => {
    basketContainer.style.transform = "translateX(100%)";
});

// Basket End
// Shop İtem Container Start
let menu_btn = document.getElementById("shop-menu");
let shop_items = document.querySelector(".shop-item-container");
let header_color = document.querySelector(".header");

menu_btn.addEventListener("click", () => {
    if (!shop_items.style.display || shop_items.style.display === "none") {
        shop_items.style.display = "block";
        header_color.style.position = "fixed";
        header_color.style.background = "#464646";
        menu_btn.className = "fa-solid fa-x";
    } else {
        menu_btn.className = "fa-solid fa-bars";
        shop_items.style.display = "none";
        header_color.style.background = "transparent";
        header_color.style.position = "absolute";
    }
});
// Shop İtem Container end

//Search Item Container Start
const searchOpen = document.querySelector(".search-open-icon");
const searchClose = document.querySelector(".search-close-icon");
const shopSearch = document.querySelector(".shop-item-search-container");
const form = document.getElementById("form");
const formInput = document.querySelector(".form_input");

searchOpen.addEventListener("click", () => {
    shopSearch.style.opacity = "1";
    shopSearch.style.visibility = "visible";
});

searchClose.addEventListener("click", () => {
    shopSearch.style.opacity = "0";
    setTimeout(() => {
        shopSearch.style.visibility = "hidden";
    }, 400);
});

form.addEventListener("input", (event) => {
    if (event.target.value.length >= 3) {
        form.style.margin = "40px auto";
        form.style.transition = formInput.style.transition = "0.5s";
        formInput.style.fontSize = "60px";
    } else if (event.target.value.length === 0) {
        form.style.margin = "150px auto";
        formInput.style.fontSize = "101px";
    }
});

//Search Item Container End

// Product Count Minus Plus Start

document.addEventListener("DOMContentLoaded", function () {
    var qtyPlusBtn = document.querySelectorAll(".qty-plus");
    var qtyMinusBtn = document.querySelectorAll(".qty-minus");
    var inputs = document.querySelectorAll('input[name="adet"]');

    qtyPlusBtn.forEach(function (btn, index) {
        btn.addEventListener("click", function () {
            var value = parseInt(inputs[index].value);
            inputs[index].value = value + 1;
        });
    });

    qtyMinusBtn.forEach(function (btn, index) {
        btn.addEventListener("click", function () {
            var value = parseInt(inputs[index].value);
            if (value > 1) {
                inputs[index].value = value - 1;
            }
        });
    });
});

// Product Count Minus Plus End

// UserPage Tabs Start
const bilgi = document.querySelector(".bilgilerim");
const sifre = document.querySelector(".sifre");
const close_user = document.querySelector(".close");
const close_content = document.querySelector(".close-user-all");
const bilgi_content = document.querySelector(".user-screen-content");
const sifre_content = document.querySelector(".user-screen-content-password");
bilgi.addEventListener("click", () => {
    bilgi_content.style.display = "block";
    bilgi.classList.add('active')
    sifre.classList.remove('active')
    sifre_content.style.display = "none";
});
sifre.addEventListener("click", () => {
    bilgi_content.style.display = "none";
    sifre.classList.add('active')
    bilgi.classList.remove('active')
    sifre_content.style.display = "block";
});
close_user.addEventListener("click", () => {
    if (
        close_content.style.display === "none" ||
        close_content.style.display === ""
    ) {
        close_content.style.display = "block";
    } else {
        close_content.style.display = "none";
    }
});
// UserPage Tabs End
