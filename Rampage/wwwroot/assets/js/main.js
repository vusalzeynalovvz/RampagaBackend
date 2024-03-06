// Main Slider Start
var swiper = new Swiper(".mySwiper2", {});
// Main Slider End

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
//Shop Image Slider Start
var swiper = new Swiper(".corusel", {
  slidesPerView: 4,
  // loop: true,
  spaceBetween: 30,

  breakpoints: {
    1200: {
      slidesPerView: 3,
      spaceBetween: 50,
    },
    1024: {
      slidesPerView: 3,
      spaceBetween: 30,
    },
    768: {
      slidesPerView: 2,
      spaceBetween: 20,
    },
    576: {
      slidesPerView: 1,
    },
    450: {
      slidesPerView: 1,
    },
    360: {
      slidesPerView: 1,
    },
  },
});
//Shop Image Slider End

//FOR YOU Sections Slider Start
var swiper = new Swiper(".foryou", {
  slidesPerView: 2,
  // loop: true,
  spaceBetween: 30,
  freeMode: true,
});
//FOR YOU Sections Slider End

const powerCategoryMap = {
  1: {
    category: document.querySelector(".power-slider-categori1"),
    box: document.querySelector(".power-slider-boxs1"),
  },
  2: {
    category: document.querySelector(".power-slider-categori2"),
    box: document.querySelector(".power-slider-boxs2"),
  },
  3: {
    category: document.querySelector(".power-slider-categori3"),
    box: document.querySelector(".power-slider-boxs3"),
  },
};

powerCategoryMap[1].category.style.color = "white";
powerCategoryMap[1].category.style.borderColor = "white";
powerCategoryMap[1].box.style.display = "block";

for (let i = 2; i <= 3; i++) {
  powerCategoryMap[i].category.style.color = "#464646";
  powerCategoryMap[i].category.style.borderColor = "#464646";
  powerCategoryMap[i].box.style.display = "none";
}

function handleCategoryClick(categoryNumber) {
  for (const [key, value] of Object.entries(powerCategoryMap)) {
    const { category, box } = value;
    if (key === categoryNumber) {
      category.style.color = "white";
      category.style.borderColor = "white";
      box.style.display = "block";
    } else {
      category.style.color = "#464646";
      category.style.borderColor = "#464646";
      box.style.display = "none";
    }
  }
}

for (const [key, value] of Object.entries(powerCategoryMap)) {
  const { category } = value;
  category.addEventListener("click", () => handleCategoryClick(key));
}

//FOR YOU Sections Slider End

//Category Tab Menu Start
const categoryClassNames = [
  ".category-menu1",
  ".category-menu2",
  ".category-menu3",
  ".category-menu4",
];
const productClassNames = [
  ".category-cards1",
  ".category-cards2",
  ".category-cards3",
  ".category-cards4",
];

const tabs = categoryClassNames.map((className) =>
  document.querySelector(className)
);
const productTabs = productClassNames.map((className) =>
  document.querySelector(className)
);

tabs.forEach((tab) => {
  tab.style.border = "1px solid #464646";
  tab.style.color = "#ffff";
});

tabs[0].style.border = "1px solid white";

tabs.forEach((tab, index) => {
  tab.addEventListener("click", () => {
    productTabs.forEach((productTab) => {
      productTab.style.display = "none";
    });
    productTabs[index].style.display = "block";

    tabs.forEach((t) => {
      t.style.border = "1px solid #464646";
    });

    tab.style.border = "1px solid white";
  });
});

//Category Tab Menu End

//User Comments Start
var swiper = new Swiper(".user-comment-slider", {
  slidesPerView: 3,
  loop: true,
  spaceBetween: 30,
});
//User Comments End

// // MapBox Api Start
// mapboxgl.accessToken =
//   "pk.eyJ1IjoidnVzYWw1OSIsImEiOiJjbHMzbjh1cmIwcXkyMmxxcDltdDQxcXJvIn0.nrS1yACX7kMacUYIFupTYQ";
// const map = new mapboxgl.Map({
//   container: "map",
//   style: "mapbox://styles/mapbox/dark-v11",
//   zoom: 13,
//   center: [-77.47, 25.04],
// });

// map.on("load", () => {
//   map.loadImage(
//     "https://docs.mapbox.com/mapbox-gl-js/assets/cat.png",
//     (error, image) => {
//       if (error) throw error;
//       map.addImage("cat", image);
//       map.addSource("point", {
//         type: "geojson",
//         data: {
//           type: "FeatureCollection",
//           features: [
//             {
//               type: "Feature",
//               geometry: {
//                 type: "Point",
//                 coordinates: [-77.47, 25.04],
//               },
//             },
//           ],
//         },
//       });
//       map.addLayer({
//         id: "points",
//         type: "symbol",
//         source: "point",
//         layout: {
//           "icon-image": "cat",
//           "icon-size": 0.25,
//         },
//       });
//     }
//   );
// });
// // MapBox Api End
// // Accardion Start
// let toggles = document.getElementsByClassName("toggle");
// let contentDiv = document.getElementsByClassName("content");
// let icons = document.getElementsByClassName("icon");
// let allCategories = document.querySelector(".all-categories");
// let boxCategories = document.querySelectorAll(".box-category");
// let boxContainer = document.querySelector(".container");

// for (let i = 0; i < toggles.length; i++) {
//   toggles[i].addEventListener("click", () => {
//     if (parseInt(contentDiv[i].style.height) != contentDiv[i].scrollHeight) {
//       contentDiv[i].style.height = contentDiv[i].scrollHeight + "px";
//       icons[i].classList.remove("fa-minus");
//       icons[i].classList.add("fa-plus");
//     } else {
//       contentDiv[i].style.height = "0px";
//       icons[i].classList.remove("fa-minus");
//       icons[i].classList.add("fa-plus");
//     }
//     for (let j = 0; j < contentDiv.length; j++) {
//       if (j != i) {
//         contentDiv[j].style.height = "0px";
//         icons[j].classList.remove("fa-minus");
//         icons[j].classList.add("fa-plus");
//       }
//     }
//   });
// }

// let isCategoriesVisible = false;

// allCategories.addEventListener("click", () => {
//   boxCategories.forEach((boxCategory) => {
//     boxCategory.style.display = isCategoriesVisible ? "none" : "block";
//   });

//   isCategoriesVisible = !isCategoriesVisible;
// });

// // Accardion End

// //Price Range Start
// $(".slider").each(function (e) {
//   var slider = $(this),
//     width = slider.width(),
//     handle,
//     handleObj;

//   let svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
//   svg.setAttribute("viewBox", "0 0 " + width + " 83");

//   slider.html(svg);
//   slider.append($("<div>").addClass("active").html(svg.cloneNode(true)));

//   slider.slider({
//     range: true,
//     values: [0, 3500],
//     min: 0,
//     step: 5,
//     minRange: 0,
//     max: 3500,
//     create(event, ui) {
//       slider.find(".ui-slider-handle").append($("<div />"));

//       $(slider.data("value-0")).html(
//         slider
//           .slider("values", 0)
//           .toString()
//           .replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
//       );
//       $(slider.data("value-1")).html(
//         slider
//           .slider("values", 1)
//           .toString()
//           .replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
//       );
//       $(slider.data("range")).html(
//         (slider.slider("values", 1) - slider.slider("values", 0))
//           .toString()
//           .replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
//       );

//       setCSSVars(slider);
//     },
//     start(event, ui) {
//       $("body").addClass("ui-slider-active");

//       handle = $(ui.handle).data("index", ui.handleIndex);
//       handleObj = slider.find(".ui-slider-handle");
//     },
//     change(event, ui) {
//       setCSSVars(slider);
//     },
//     slide(event, ui) {
//       let min = slider.slider("option", "min"),
//         minRange = slider.slider("option", "minRange"),
//         max = slider.slider("option", "max");

//       if (ui.handleIndex == 0) {
//         if (ui.values[0] + minRange >= ui.values[1]) {
//           slider.slider("values", 1, ui.values[0] + minRange);
//         }
//         if (ui.values[0] > max - minRange) {
//           return false;
//         }
//       } else if (ui.handleIndex == 1) {
//         if (ui.values[1] - minRange <= ui.values[0]) {
//           slider.slider("values", 0, ui.values[1] - minRange);
//         }
//         if (ui.values[1] < min + minRange) {
//           return false;
//         }
//       }

//       $(slider.data("value-0")).html(
//         ui.values[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
//       );
//       $(slider.data("value-1")).html(
//         ui.values[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
//       );
//       $(slider.data("range")).html(
//         (slider.slider("values", 1) - slider.slider("values", 0))
//           .toString()
//           .replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
//       );

//       setCSSVars(slider);
//     },
//     stop(event, ui) {
//       $("body").removeClass("ui-slider-active");

//       let duration = 0.6,
//         ease = Elastic.easeOut.config(1.08, 0.44);

//       TweenMax.to(handle, duration, {
//         "--y": 0,
//         ease: ease,
//       });

//       TweenMax.to(svgPath, duration, {
//         y: 42,
//         ease: ease,
//       });

//       handle = null;
//     },
//   });

//   var svgPath = new Proxy(
//     {
//       x: null,
//       y: null,
//       b: null,
//       a: null,
//     },
//     {
//       set(target, key, value) {
//         target[key] = value;
//         if (
//           target.x !== null &&
//           target.y !== null &&
//           target.b !== null &&
//           target.a !== null
//         ) {
//           slider
//             .find("svg")
//             .html(getPath([target.x, target.y], target.b, target.a, width));
//         }
//         return true;
//       },
//       get(target, key) {
//         return target[key];
//       },
//     }
//   );

//   svgPath.x = width / 2;
//   svgPath.y = 42;
//   svgPath.b = 0;
//   svgPath.a = width;

//   $(document).on("mousemove touchmove", (e) => {
//     if (handle) {
//       let laziness = 4,
//         max = 24,
//         edge = 52,
//         other = handleObj.eq(handle.data("index") == 0 ? 1 : 0),
//         currentLeft = handle.position().left,
//         otherLeft = other.position().left,
//         handleWidth = handle.outerWidth(),
//         handleHalf = handleWidth / 2,
//         y = e.pageY - handle.offset().top - handle.outerHeight() / 2,
//         moveY =
//           y - laziness >= 0
//             ? y - laziness
//             : y + laziness <= 0
//             ? y + laziness
//             : 0,
//         modify = 1;

//       moveY = moveY > max ? max : moveY < -max ? -max : moveY;
//       modify =
//         handle.data("index") == 0
//           ? (currentLeft + handleHalf <= edge
//               ? (currentLeft + handleHalf) / edge
//               : 1) *
//             (otherLeft - currentLeft - handleWidth <= edge
//               ? (otherLeft - currentLeft - handleWidth) / edge
//               : 1)
//           : (currentLeft - (otherLeft + handleHalf * 2) <= edge
//               ? (currentLeft - (otherLeft + handleWidth)) / edge
//               : 1) *
//             (slider.outerWidth() - (currentLeft + handleHalf) <= edge
//               ? (slider.outerWidth() - (currentLeft + handleHalf)) / edge
//               : 1);
//       modify = modify > 1 ? 1 : modify < 0 ? 0 : modify;

//       if (handle.data("index") == 0) {
//         svgPath.b = (currentLeft / 2) * modify;
//         svgPath.a = otherLeft;
//       } else {
//         svgPath.b = otherLeft + handleHalf;
//         svgPath.a =
//           (slider.outerWidth() - currentLeft) / 2 +
//           currentLeft +
//           handleHalf +
//           ((slider.outerWidth() - currentLeft) / 2) * (1 - modify);
//       }

//       svgPath.x = currentLeft + handleHalf;
//       svgPath.y = moveY * modify + 42;

//       handle.css("--y", moveY * modify);
//     }
//   });
// });

// function getPoint(point, i, a, smoothing) {
//   let cp = (current, previous, next, reverse) => {
//       let p = previous || current,
//         n = next || current,
//         o = {
//           length: Math.sqrt(
//             Math.pow(n[0] - p[0], 2) + Math.pow(n[1] - p[1], 2)
//           ),
//           angle: Math.atan2(n[1] - p[1], n[0] - p[0]),
//         },
//         angle = o.angle + (reverse ? Math.PI : 0),
//         length = o.length * smoothing;
//       return [
//         current[0] + Math.cos(angle) * length,
//         current[1] + Math.sin(angle) * length,
//       ];
//     },
//     cps = cp(a[i - 1], a[i - 2], point, false),
//     cpe = cp(point, a[i - 1], a[i + 1], true);
//   return `C ${cps[0]},${cps[1]} ${cpe[0]},${cpe[1]} ${point[0]},${point[1]}`;
// }

// function getPath(update, before, after, width) {
//   let smoothing = 0.16,
//     points = [
//       [0, 42],
//       [before <= 0 ? 0 : before, 42],
//       update,
//       [after >= width ? width : after, 42],
//       [width, 42],
//     ],
//     d = points.reduce(
//       (acc, point, i, a) =>
//         i === 0
//           ? `M ${point[0]},${point[1]}`
//           : `${acc} ${getPoint(point, i, a, smoothing)}`,
//       ""
//     );
//   return `<path d="${d}" />`;
// }

// function setCSSVars(slider) {
//   let handle = slider.find(".ui-slider-handle");
//   slider.css({
//     "--l": handle.eq(0).position().left + handle.eq(0).outerWidth() / 2,
//     "--r":
//       slider.outerWidth() -
//       (handle.eq(1).position().left + handle.eq(1).outerWidth() / 2),
//   });
// }

// let none_filter_icon = document.querySelector(".none-filter-icon");
// let filter_left_all = document.querySelector(".filter-left-all");

// none_filter_icon.addEventListener("click", () => {
//   filter_left_all.style.display =
//     filter_left_all.style.display === "block" ? "none" : "block";
// });
// //Price Range End
// // Left Basket End

// // Product Detail Start
// const allHoverImages = document.querySelectorAll(".swiper-slide div img");
// const imgContainer = document.querySelector(".img-container");
// window.addEventListener("DOMContentLoaded", () => {
//   allHoverImages[0].parentElement.classList.add("active");
// });
// allHoverImages.forEach((image) => {
//   image.addEventListener("mouseover", () => {
//     imgContainer.querySelector("img").src = image.src;
//     resetActiveImg();
//     image.parentElement.classList.add("active");
//   });
// });
// function resetActiveImg() {
//   allHoverImages.forEach((img) => {
//     img.parentElement.classList.remove("active");
//   });
// }
// // Product Detail End

// // Product Detail Slider Start

// var swiper = new Swiper(".detailslider", {
//   slidesPerView: 5,
//   pagination: {
//     el: ".swiper-pagination",
//     clickable: true,
//   },
//   navigation: {
//     nextEl: ".swiper-button-next",
//     prevEl: ".swiper-button-prev",
//   },

//   breakpoints: {
//     1200: {
//       slidesPerView: 5,
//     },
//     1024: {
//       slidesPerView: 3,
//     },
//     768: {
//       slidesPerView: 4,
//     },
//     576: {
//       slidesPerView: 3,
//     },
//     450: {
//       slidesPerView: 3,
//     },
//     360: {
//       slidesPerView: 2,
//     },
//   },
// });

// // Product Detail Slider End
