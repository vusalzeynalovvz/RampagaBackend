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

// Accardion Start
let toggles = document.getElementsByClassName("toggle");
let contentDiv = document.getElementsByClassName("content");
let icons = document.getElementsByClassName("icon");
let allCategories = document.querySelector(".all-categories");
let boxCategories = document.querySelectorAll(".box-category");
let boxContainer = document.querySelector(".container");

for (let i = 0; i < toggles.length; i++) {
  toggles[i].addEventListener("click", () => {
    if (parseInt(contentDiv[i].style.height) != contentDiv[i].scrollHeight) {
      contentDiv[i].style.height = contentDiv[i].scrollHeight + "px";
      icons[i].classList.remove("fa-minus");
      icons[i].classList.add("fa-plus");
    } else {
      contentDiv[i].style.height = "0px";
      icons[i].classList.remove("fa-minus");
      icons[i].classList.add("fa-plus");
    }
    for (let j = 0; j < contentDiv.length; j++) {
      if (j != i) {
        contentDiv[j].style.height = "0px";
        icons[j].classList.remove("fa-minus");
        icons[j].classList.add("fa-plus");
      }
    }
  });
}

let isCategoriesVisible = false;

allCategories.addEventListener("click", () => {
  boxCategories.forEach((boxCategory) => {
    boxCategory.style.display = isCategoriesVisible ? "none" : "block";
  });

  isCategoriesVisible = !isCategoriesVisible;
});

// Accardion End

//Price Range Start
$(".slider").each(function (e) {
  var slider = $(this),
    width = slider.width(),
    handle,
    handleObj;

  let svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
  svg.setAttribute("viewBox", "0 0 " + width + " 83");

  slider.html(svg);
  slider.append($("<div>").addClass("active").html(svg.cloneNode(true)));

  slider.slider({
    range: true,
    values: [0, 3500],
    min: 0,
    step: 5,
    minRange: 0,
    max: 3500,
    create(event, ui) {
      slider.find(".ui-slider-handle").append($("<div />"));

      $(slider.data("value-0")).html(
        slider
          .slider("values", 0)
          .toString()
          .replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
      );
      $(slider.data("value-1")).html(
        slider
          .slider("values", 1)
          .toString()
          .replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
      );
      $(slider.data("range")).html(
        (slider.slider("values", 1) - slider.slider("values", 0))
          .toString()
          .replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
      );

      setCSSVars(slider);
    },
    start(event, ui) {
      $("body").addClass("ui-slider-active");

      handle = $(ui.handle).data("index", ui.handleIndex);
      handleObj = slider.find(".ui-slider-handle");
    },
    change(event, ui) {
      setCSSVars(slider);
    },
    slide(event, ui) {
      let min = slider.slider("option", "min"),
        minRange = slider.slider("option", "minRange"),
        max = slider.slider("option", "max");

      if (ui.handleIndex == 0) {
        if (ui.values[0] + minRange >= ui.values[1]) {
          slider.slider("values", 1, ui.values[0] + minRange);
        }
        if (ui.values[0] > max - minRange) {
          return false;
        }
      } else if (ui.handleIndex == 1) {
        if (ui.values[1] - minRange <= ui.values[0]) {
          slider.slider("values", 0, ui.values[1] - minRange);
        }
        if (ui.values[1] < min + minRange) {
          return false;
        }
      }

      $(slider.data("value-0")).html(
        ui.values[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
      );
      $(slider.data("value-1")).html(
        ui.values[1].toString().replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
      );
      $(slider.data("range")).html(
        (slider.slider("values", 1) - slider.slider("values", 0))
          .toString()
          .replace(/\B(?=(\d{3})+(?!\d))/g, "&thinsp;")
      );

      setCSSVars(slider);
    },
    stop(event, ui) {
      $("body").removeClass("ui-slider-active");

      let duration = 0.6,
        ease = Elastic.easeOut.config(1.08, 0.44);

      TweenMax.to(handle, duration, {
        "--y": 0,
        ease: ease,
      });

      TweenMax.to(svgPath, duration, {
        y: 42,
        ease: ease,
      });

      handle = null;
    },
  });

  var svgPath = new Proxy(
    {
      x: null,
      y: null,
      b: null,
      a: null,
    },
    {
      set(target, key, value) {
        target[key] = value;
        if (
          target.x !== null &&
          target.y !== null &&
          target.b !== null &&
          target.a !== null
        ) {
          slider
            .find("svg")
            .html(getPath([target.x, target.y], target.b, target.a, width));
        }
        return true;
      },
      get(target, key) {
        return target[key];
      },
    }
  );

  svgPath.x = width / 2;
  svgPath.y = 42;
  svgPath.b = 0;
  svgPath.a = width;

  $(document).on("mousemove touchmove", (e) => {
    if (handle) {
      let laziness = 4,
        max = 24,
        edge = 52,
        other = handleObj.eq(handle.data("index") == 0 ? 1 : 0),
        currentLeft = handle.position().left,
        otherLeft = other.position().left,
        handleWidth = handle.outerWidth(),
        handleHalf = handleWidth / 2,
        y = e.pageY - handle.offset().top - handle.outerHeight() / 2,
        moveY =
          y - laziness >= 0
            ? y - laziness
            : y + laziness <= 0
            ? y + laziness
            : 0,
        modify = 1;

      moveY = moveY > max ? max : moveY < -max ? -max : moveY;
      modify =
        handle.data("index") == 0
          ? (currentLeft + handleHalf <= edge
              ? (currentLeft + handleHalf) / edge
              : 1) *
            (otherLeft - currentLeft - handleWidth <= edge
              ? (otherLeft - currentLeft - handleWidth) / edge
              : 1)
          : (currentLeft - (otherLeft + handleHalf * 2) <= edge
              ? (currentLeft - (otherLeft + handleWidth)) / edge
              : 1) *
            (slider.outerWidth() - (currentLeft + handleHalf) <= edge
              ? (slider.outerWidth() - (currentLeft + handleHalf)) / edge
              : 1);
      modify = modify > 1 ? 1 : modify < 0 ? 0 : modify;

      if (handle.data("index") == 0) {
        svgPath.b = (currentLeft / 2) * modify;
        svgPath.a = otherLeft;
      } else {
        svgPath.b = otherLeft + handleHalf;
        svgPath.a =
          (slider.outerWidth() - currentLeft) / 2 +
          currentLeft +
          handleHalf +
          ((slider.outerWidth() - currentLeft) / 2) * (1 - modify);
      }

      svgPath.x = currentLeft + handleHalf;
      svgPath.y = moveY * modify + 42;

      handle.css("--y", moveY * modify);
    }
  });
});

function getPoint(point, i, a, smoothing) {
  let cp = (current, previous, next, reverse) => {
      let p = previous || current,
        n = next || current,
        o = {
          length: Math.sqrt(
            Math.pow(n[0] - p[0], 2) + Math.pow(n[1] - p[1], 2)
          ),
          angle: Math.atan2(n[1] - p[1], n[0] - p[0]),
        },
        angle = o.angle + (reverse ? Math.PI : 0),
        length = o.length * smoothing;
      return [
        current[0] + Math.cos(angle) * length,
        current[1] + Math.sin(angle) * length,
      ];
    },
    cps = cp(a[i - 1], a[i - 2], point, false),
    cpe = cp(point, a[i - 1], a[i + 1], true);
  return `C ${cps[0]},${cps[1]} ${cpe[0]},${cpe[1]} ${point[0]},${point[1]}`;
}

function getPath(update, before, after, width) {
  let smoothing = 0.16,
    points = [
      [0, 42],
      [before <= 0 ? 0 : before, 42],
      update,
      [after >= width ? width : after, 42],
      [width, 42],
    ],
    d = points.reduce(
      (acc, point, i, a) =>
        i === 0
          ? `M ${point[0]},${point[1]}`
          : `${acc} ${getPoint(point, i, a, smoothing)}`,
      ""
    );
  return `<path d="${d}" />`;
}

function setCSSVars(slider) {
  let handle = slider.find(".ui-slider-handle");
  slider.css({
    "--l": handle.eq(0).position().left + handle.eq(0).outerWidth() / 2,
    "--r":
      slider.outerWidth() -
      (handle.eq(1).position().left + handle.eq(1).outerWidth() / 2),
  });
}

let none_filter_icon = document.querySelector(".none-filter-icon");
let filter_left_all = document.querySelector(".filter-left-all");

none_filter_icon.addEventListener("click", () => {
  filter_left_all.style.display =
    filter_left_all.style.display === "block" ? "none" : "block";
});
//Price Range End

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
