var mr = function(a, b, c) {
    "use strict";

    function d(b) {
        b = "undefined" == typeof b ? a : b, g.documentReady.concat(g.documentReadyDeferred).forEach(function(a) {
            a(b)
        })
    }

    function e(b) {
        b = "object" == typeof b ? a : b, g.windowLoad.concat(g.windowLoadDeferred).forEach(function(a) {
            a(b)
        })
    }
    var f = {},
        g = {
            documentReady: [],
            documentReadyDeferred: [],
            windowLoad: [],
            windowLoadDeferred: []
        };
    return a(c).ready(d), a(b).on("load", e), f.setContext = function(b) {
        var c = a;
        return "undefined" != typeof b ? function(c) {
            return a(b).find(c)
        } : c
    }, f.components = g, f.documentReady = d, f.windowLoad = e, f
}(jQuery, window, document);
mr = function(a, b, c, d) {
        "use strict";
        return a.util = {}, a.util.requestAnimationFrame = c.requestAnimationFrame || c.mozRequestAnimationFrame || c.webkitRequestAnimationFrame || c.msRequestAnimationFrame, a.util.documentReady = function(a) {
            var b = new Date,
                c = b.getFullYear();
            a(".update-year").text(c)
        }, a.util.getURLParameter = function(a) {
            return decodeURIComponent((new RegExp("[?|&]" + a + "=([^&;]+?)(&|#|;|$)").exec(location.search) || [void 0, ""])[1].replace(/\+/g, "%20")) || null
        }, a.util.capitaliseFirstLetter = function(a) {
            return a.charAt(0).toUpperCase() + a.slice(1)
        }, a.util.slugify = function(a, b) {
            return "undefined" != typeof b ? a.replace(/ +/g, "") : a.toLowerCase().replace(/[\~\!\@\#\$\%\^\&\*\(\)\-\_\=\+\]\[\}\{\'\"\;\\\:\?\/\>\<\.\,]+/g, "").replace(/ +/g, "-")
        }, a.util.sortChildrenByText = function(a, c) {
            var d = b(a),
                e = d.children().get(),
                f = -1,
                g = 1;
            "undefined" != typeof c && (f = 1, g = -1), e.sort(function(a, c) {
                var d = b(a).text(),
                    e = b(c).text();
                return e > d ? f : d > e ? g : 0
            }), d.empty(), b(e).each(function(a, b) {
                d.append(b)
            })
        }, a.util.idleSrc = function(a, c) {
            c = "undefined" != typeof c ? c : "";
            var d = a.is(c + "[src]") ? a : a.find(c + "[src]");
            d.each(function(a, c) {
                c = b(c);
                var d = c.attr("src"),
                    e = c.attr("data-src");
                "undefined" == typeof e && c.attr("data-src", d), c.attr("src", "")
            })
        }, a.util.activateIdleSrc = function(a, c) {
            c = "undefined" != typeof c ? c : "";
            var d = a.is(c + "[src]") ? a : a.find(c + "[src]");
            d.each(function(a, c) {
                c = b(c);
                var d = c.attr("data-src");
                "undefined" != typeof d && c.attr("src", d)
            })
        }, a.util.pauseVideo = function(a) {
            var c = a.is("video") ? a : a.find("video");
            c.each(function(a, c) {
                var d = b(c).get(0);
                d.pause()
            })
        }, a.util.parsePixels = function(a) {
            var d, e = b(c).height();
            return /^[1-9]{1}[0-9]*[p][x]$/.test(a) ? parseInt(a.replace("px", ""), 10) : /^[1-9]{1}[0-9]*[v][h]$/.test(a) ? (d = parseInt(a.replace("vh", ""), 10), e * (d / 100)) : -1
        }, a.components.documentReady.push(a.util.documentReady), a
    }(mr, jQuery, window, document), mr = function(a, b, c, d) {
        "use strict";
        return a.window = {}, a.window.height = b(c).height(), a.window.width = b(c).width(), b(c).on("resize", function() {
            a.window.height = b(c).height(), a.window.width = b(c).width()
        }), a
    }(mr, jQuery, window, document), mr = function(a, b, c, d) {
        "use strict";
        a.scroll = {}, a.scroll.listeners = [], a.scroll.y = 0, a.scroll.x = 0;
        var e = function(b) {
            b("body").hasClass("scroll-assist") && (a.scroll.assisted = !0), addEventListener("scroll", function(a) {
                c.mr.scroll.y = c.pageYOffset, c.mr.scroll.update(a)
            }, !1)
        };
        return a.scroll.update = function(b) {
            for (var c = 0, d = a.scroll.listeners.length; d > c; c++) a.scroll.listeners[c](b)
        }, a.scroll.documentReady = e, a.components.documentReady.push(e), a
    }(mr, jQuery, window, document), 
    mr = function(a, b, c, d) {
        "use strict";
        a.scroll.classModifiers = {}, a.scroll.classModifiers.rules = [], a.scroll.classModifiers.parseScrollRules = function(b) {
            var c = b.attr("data-scroll-class"),
                d = c.split(";");
            return d.forEach(function(c) {
                var d, e, f = {};
                if (d = c.replace(/\s/g, "").split(":"), 2 === d.length) {
                    if (e = a.util.parsePixels(d[0]), !(e > -1)) return !1;
                    if (f.scrollPoint = e, !d[1].length) return !1;
                    var g = d[1];
                    f.toggleClass = g, f.hasClass = b.hasClass(g), f.element = b.get(0), a.scroll.classModifiers.rules.push(f)
                }
            }), a.scroll.classModifiers.rules.length ? !0 : !1
        }, a.scroll.classModifiers.update = function(b) {
            for (var c, d = a.scroll.y, e = a.scroll.classModifiers.rules, f = e.length; f--;) c = e[f], d > c.scrollPoint && !c.hasClass && (c.element.classList.add(c.toggleClass), c.hasClass = a.scroll.classModifiers.rules[f].hasClass = !0), d < c.scrollPoint && c.hasClass && (c.element.classList.remove(c.toggleClass), c.hasClass = a.scroll.classModifiers.rules[f].hasClass = !1)
        };
        var e = function() {
                b('.main-container [data-scroll-class*="pos-fixed"]').each(function() {
                    var a = b(this);
                    a.css("max-width", a.parent().outerWidth()), a.parent().css("min-height", a.outerHeight())
                })
            },
            f = function(b) {
                b("[data-scroll-class]").each(function() {
                    var c = b(this);
                    !a.scroll.classModifiers.parseScrollRules(c)
                }), e(), b(c).on("resize", e), a.scroll.classModifiers.rules.length && a.scroll.listeners.push(a.scroll.classModifiers.update)
            };
        return a.components.documentReady.push(f), a.scroll.classModifiers.documentReady = f, a
    }(mr, jQuery, window, document)