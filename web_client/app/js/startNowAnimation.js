document.addEventListener("scroll", function(e) {
    var t = 400,
        s = document.body.scrollTop,
        o = document.getElementsByClassName("js-heading")[0],
        a = document.getElementsByClassName("js-show-magic")[0],
        i = document.getElementsByClassName("steps")[0];
    if (a) {
        var c = a.offsetTop;
        s >= c - t ? i.classList.add("active") : i.classList.remove("active")
    }
});
