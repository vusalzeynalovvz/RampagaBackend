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

// Left Basket Start
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
// Left Basket End

// Product Detail Start
const allHoverImages = document.querySelectorAll(".swiper-slide div img");
const imgContainer = document.querySelector(".img-container");
window.addEventListener("DOMContentLoaded", () => {
  allHoverImages[0].parentElement.classList.add("active");
});
allHoverImages.forEach((image) => {
  image.addEventListener("mouseover", () => {
    imgContainer.querySelector("img").src = image.src;
    resetActiveImg();
    image.parentElement.classList.add("active");
  });
});
function resetActiveImg() {
  allHoverImages.forEach((img) => {
    img.parentElement.classList.remove("active");
  });
}
// Product Detail End

// Product Detail Slider Start

var swiper = new Swiper(".detailslider", {
  slidesPerView: 5,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },

  breakpoints: {
    1200: {
      slidesPerView: 5,
    },
    1024: {
      slidesPerView: 3,
    },
    768: {
      slidesPerView: 4,
    },
    576: {
      slidesPerView: 3,
    },
    450: {
      slidesPerView: 3,
    },
    360: {
      slidesPerView: 2,
    },
  },
});

// Product Detail Slider End

// Product Count Minus Plus Start
document.addEventListener("DOMContentLoaded", function () {
  var qtyPlusBtn = document.querySelector(".qty-plus");
  var qtyMinusBtn = document.querySelector(".qty-minus");
  var input = document.querySelector('input[name="adet"]');

  qtyPlusBtn.addEventListener("click", function () {
    var value = parseInt(input.value);
    input.value = value + 1;
  });

  qtyMinusBtn.addEventListener("click", function () {
    var value = parseInt(input.value);
    if (value > 1) {
      input.value = value - 1;
    }
  });
});

// Product Count Minus Plus End

//FOR YOU Sections Slider Start
var swiper = new Swiper(".foryou", {
  slidesPerView: 2,
  // loop: true,
  spaceBetween: 20,
  freeMode: true,
});
//FOR YOU Sections Slider End

// Prop Tabs Start

const tab_item1 = document.querySelector(".tab-item1");
const tab_item2 = document.querySelector(".tab-item2");
const tab_item3 = document.querySelector(".tab-item3");

const tab_content1 = document.querySelector(".tab-content1");
const tab_content2 = document.querySelector(".tab-content2");
const tab_content3 = document.querySelector(".tab-content3");

tab_item1.addEventListener("click", () => {
  tab_content1.style.display = "block";
  tab_content2.style.display = "none";
  tab_content3.style.display = "none";
  tab_item1.style.opacity = "1";
  tab_item2.style.opacity = "0.5";
  tab_item3.style.opacity = "0.5";
});

tab_item2.addEventListener("click", () => {
  tab_content1.style.display = "none";
  tab_content2.style.display = "block";
  tab_content3.style.display = "none";
  tab_item1.style.opacity = "0.5";
  tab_item2.style.opacity = "1";
  tab_item3.style.opacity = "0.5";
});

tab_item3.addEventListener("click", () => {
  tab_content1.style.display = "none";
  tab_content2.style.display = "none";
  tab_content3.style.display = "block";
  tab_item1.style.opacity = "0.5";
  tab_item2.style.opacity = "0.5";
  tab_item3.style.opacity = "1";
});

// Prop Tabs End
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
