/**
 * Load utils - BEGIN
 */
/*
 CSS Browser Selector 1.0
 Originally written by Rafael Lima (http://rafael.adm.br)
 http://rafael.adm.br/css_browser_selector
 License: http://creativecommons.org/licenses/by/2.5/

 Co-maintained by:
 https://github.com/ridjohansen/css_browser_selector
 https://github.com/wbruno/css_browser_selector
 */

"use strict";

var uaInfo = {
	ua: '',
	is: function(t) {
		return RegExp(t, "i").test(uaInfo.ua);
	},
	version: function(p, n) {
		n = n.replace(".", "_");
		var i = n.indexOf('_'),
			ver = "";
		while (i > 0) {
			ver += " " + p + n.substring(0, i);
			i = n.indexOf('_', i + 1);
		}
		ver += " " + p + n;
		return ver;
	},
	getBrowser: function() {
		var g = 'gecko',
			w = 'webkit',
			c = 'chrome',
			f = 'firefox',
			s = 'safari',
			o = 'opera',
			a = 'android',
			bb = 'blackberry',
			dv = 'device_',
			ua = uaInfo.ua,
			is = uaInfo.is;
		return [
			(!(/opera|webtv/i.test(ua)) && /msie\s(\d+)/.test(ua)) ? ('ie ie' + (/trident\/4\.0/.test(ua) ? '8' : RegExp.$1)) : is('firefox/') ? g + " " + f + (/firefox\/((\d+)(\.(\d+))(\.\d+)*)/.test(ua) ? ' ' + f + RegExp.$2 + ' ' + f + RegExp.$2 + "_" + RegExp.$4 : '') : is('gecko/') ? g : is('opera') ? o + (/version\/((\d+)(\.(\d+))(\.\d+)*)/.test(ua) ? ' ' + o + RegExp.$2 + ' ' + o + RegExp.$2 + "_" + RegExp.$4 : (/opera(\s|\/)(\d+)\.(\d+)/.test(ua) ? ' ' + o + RegExp.$2 + " " + o + RegExp.$2 + "_" + RegExp.$3 : '')) : is('konqueror') ? 'konqueror' : is('blackberry') ? (bb + (/Version\/(\d+)(\.(\d+)+)/i.test(ua) ? " " + bb + RegExp.$1 + " " + bb + RegExp.$1 + RegExp.$2.replace('.', '_') : (/Blackberry ?(([0-9]+)([a-z]?))[\/|;]/gi.test(ua) ? ' ' + bb + RegExp.$2 + (RegExp.$3 ? ' ' + bb + RegExp.$2 + RegExp.$3 : '') : ''))) // blackberry
			: is('android') ? (a + (/Version\/(\d+)(\.(\d+))+/i.test(ua) ? " " + a + RegExp.$1 + " " + a + RegExp.$1 + RegExp.$2.replace('.', '_') : '') + (/Android (.+); (.+) Build/i.test(ua) ? ' ' + dv + ((RegExp.$2).replace(/ /g, "_")).replace(/-/g, "_") : '')) //android
			: is('chrome') ? w + ' ' + c + (/chrome\/((\d+)(\.(\d+))(\.\d+)*)/.test(ua) ? ' ' + c + RegExp.$2 + ((RegExp.$4 > 0) ? ' ' + c + RegExp.$2 + "_" + RegExp.$4 : '') : '') : is('iron') ? w + ' iron' : is('applewebkit/') ? (w + ' ' + s + (/version\/((\d+)(\.(\d+))(\.\d+)*)/.test(ua) ? ' ' + s + RegExp.$2 + " " + s + RegExp.$2 + RegExp.$3.replace('.', '_') : (/ Safari\/(\d+)/i.test(ua) ? ((RegExp.$1 == "419" || RegExp.$1 == "417" || RegExp.$1 == "416" || RegExp.$1 == "412") ? ' ' + s + '2_0' : RegExp.$1 == "312" ? ' ' + s + '1_3' : RegExp.$1 == "125" ? ' ' + s + '1_2' : RegExp.$1 == "85" ? ' ' + s + '1_0' : '') : ''))) //applewebkit
			: is('mozilla/') ? g : ''
		];
	},
	getPlatform: function() {
		var ua = uaInfo.ua,
			version = uaInfo.version,
			is = uaInfo.is;
		return [
			is('j2me') ? 'j2me' : is('ipad|ipod|iphone') ? (
				(/CPU( iPhone)? OS (\d+[_|\.]\d+([_|\.]\d+)*)/i.test(ua) ? 'ios' + version('ios', RegExp.$2) : '') + ' ' + (/(ip(ad|od|hone))/gi.test(ua) ? RegExp.$1 : "")) //'iphone'
			//:is('ipod')?'ipod'
			//:is('ipad')?'ipad'
			: is('playbook') ? 'playbook' : is('kindle|silk') ? 'kindle' : is('playbook') ? 'playbook' : is('mac') ? 'mac' + (/mac os x ((\d+)[.|_](\d+))/.test(ua) ? (' mac' + (RegExp.$2) + ' mac' + (RegExp.$1).replace('.', "_")) : '') : is('win') ? 'win' + (is('windows nt 6.2') ? ' win8' : is('windows nt 6.1') ? ' win7' : is('windows nt 6.0') ? ' vista' : is('windows nt 5.2') || is('windows nt 5.1') ? ' win_xp' : is('windows nt 5.0') ? ' win_2k' : is('windows nt 4.0') || is('WinNT4.0') ? ' win_nt' : '') : is('freebsd') ? 'freebsd' : is('x11|linux') ? 'linux' : ''
		];
	},
	getMobile: function() {
		var is = uaInfo.is;
		return [
			is("android|mobi|mobile|j2me|iphone|ipod|ipad|blackberry|playbook|kindle|silk") ? 'mobile' : ''
		];
	},
	getIpadApp: function() {
		var is = uaInfo.is;
		return [
			(is('ipad|iphone|ipod') && !is('safari')) ? 'ipad_app' : ''
		];
	},
	getLang: function() {
		var ua = uaInfo.ua;
		return [/[; |\[](([a-z]{2})(\-[a-z]{2})?)[)|;|\]]/i.test(ua) ? ('lang_' + RegExp.$2).replace("-", "_") + (RegExp.$3 != '' ? (' ' + 'lang_' + RegExp.$1).replace("-", "_") : '') : ''];
	}
}
var screenInfo = {
	width: (window.outerWidth || document.documentElement.clientWidth) - 15,
	height: window.outerHeight || document.documentElement.clientHeight,
	screens: [0, 768, 980, 1200],
	screenSize: function() {
		screenInfo.width = (window.outerWidth || document.documentElement.clientWidth) - 15;
		screenInfo.height = window.outerHeight || document.documentElement.clientHeight;
		var screens = screenInfo.screens,
			i = screens.length,
			arr = [],
			maxw,
			minw;
		while (i--) {
			if (screenInfo.width >= screens[i]) {
				if (i) {
					arr.push("minw_" + screens[(i)]);
				}
				if (i <= 2) {
					arr.push("maxw_" + (screens[(i) + 1] - 1));
				}
				break;
			}
		}
		return arr;
	},
	getOrientation: function() {
		return screenInfo.width < screenInfo.height ? ["orientation_portrait"] : ["orientation_landscape"];
	},
	getInfo: function() {
		var arr = [];
		arr = arr.concat(screenInfo.screenSize());
		arr = arr.concat(screenInfo.getOrientation());
		return arr;
	},
	getPixelRatio: function() {
		var arr = [],
			pixelRatio = window.devicePixelRatio ? window.devicePixelRatio : 1;
		if (pixelRatio > 1) {
			arr.push('retina_' + parseInt(pixelRatio) + 'x');
			arr.push('hidpi');
		} else {
			arr.push('no-hidpi');
		}
		return arr;
	}
}
var dataUriInfo = {
	data: new Image(),
	div: document.createElement("div"),
	isIeLessThan9: false,
	getImg: function() {
		dataUriInfo.data.src = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==";
		dataUriInfo.div.innerHTML = "<!--[if lt IE 9]><i></i><![endif]-->";
		dataUriInfo.isIeLessThan9 = dataUriInfo.div.getElementsByTagName("i").length == 1;
		return dataUriInfo.data;
	},
	checkSupport: function() {
		if (dataUriInfo.data.width != 1 || dataUriInfo.data.height != 1 || dataUriInfo.isIeLessThan9) {
			return ["no-datauri"];
		} else {
			return ["datauri"];
		}
	}
}

function css_browser_selector(u, ns) {
		var html = document.documentElement,
			b = []
		ns = ns ? ns : "";
		/* ua */
		uaInfo.ua = u.toLowerCase();
		var browser = uaInfo.getBrowser();
		if (browser == 'gecko') browser = (!(window.ActiveXObject) && "ActiveXObject" in window) ? 'ie ie11' : browser;
		var pattTouch = /no-touch/g;
		if (pattTouch.test(html.className)) b = b.concat('no-touch');
		else b = b.concat('touch');
		var pattAdmin = /admin-mode/g;
		if (pattAdmin.test(html.className)) b = b.concat('admin-mode');
		b = b.concat(browser);
		b = b.concat(uaInfo.getPlatform());
		b = b.concat(uaInfo.getMobile());
		b = b.concat(uaInfo.getIpadApp());
		b = b.concat(uaInfo.getLang());
		/* js */
		b = b.concat(['js']);
		/* pixel ratio */
		b = b.concat(screenInfo.getPixelRatio());
		/* screen */
		b = b.concat(screenInfo.getInfo());
		var updateScreen = function() {
			html.className = html.className.replace(/ ?orientation_\w+/g, "").replace(/ [min|max|cl]+[w|h]_\d+/g, "");
			html.className = html.className + ' ' + screenInfo.getInfo().join(' ');
		}
		window.addEventListener('resize', updateScreen);
		window.addEventListener('orientationchange', updateScreen);
		/* dataURI */
		var data = dataUriInfo.getImg();
		data.onload = data.onerror = function() {
				html.className += ' ' + dataUriInfo.checkSupport().join(' ');
			}
			/* removendo itens invalidos do array */
		b = b.filter(function(e) {
			return e;
		});
		/* prefixo do namespace */
		b[0] = ns ? ns + b[0] : b[0];
		html.className = b.join(' ' + ns);
		return html.className;
	}
	// define css_browser_selector_ns before loading this script to assign a namespace
var css_browser_selector_ns = css_browser_selector_ns || "";
// init
css_browser_selector(navigator.userAgent, css_browser_selector_ns);
/**
 * skip-link-focus-fix.js
 *
 * Helps with accessibility for keyboard only users.
 *
 * Learn more: https://github.com/Automattic/_s/pull/136
 */
(function() {
	var is_webkit = navigator.userAgent.toLowerCase().indexOf('webkit') > -1,
		is_opera = navigator.userAgent.toLowerCase().indexOf('opera') > -1,
		is_ie = navigator.userAgent.toLowerCase().indexOf('msie') > -1;
	if ((is_webkit || is_opera || is_ie) && document.getElementById && window.addEventListener) {
		window.addEventListener('hashchange', function() {
			var id = location.hash.substring(1),
				element;
			if (!(/^[A-z0-9_-]+$/.test(id))) {
				return;
			}
			element = document.getElementById(id);
			if (element) {
				if (!(/^(?:a|select|input|button|textarea)$/i.test(element.tagName))) {
					element.tabIndex = -1;
				}
				element.focus();
			}
		}, false);
	}
})();
// Polyfill for creating CustomEvents on IE9/10/11
// code pulled from:
// https://github.com/d4tocchini/customevent-polyfill
// https://developer.mozilla.org/en-US/docs/Web/API/CustomEvent#Polyfill
try {
	new CustomEvent("test");
} catch (e) {
	var CustomEvent = function(event, params) {
		var evt;
		params = params || {
			bubbles: false,
			cancelable: false,
			detail: undefined
		};
		evt = document.createEvent("CustomEvent");
		evt.initCustomEvent(event, params.bubbles, params.cancelable, params.detail);
		return evt;
	};
	CustomEvent.prototype = window.Event.prototype;
	window.CustomEvent = CustomEvent; // expose definition to window
}
// Evento - v1.0.0
// by Erik Royall <erikroyalL@hotmail.com> (http://erikroyall.github.io)
// Dual licensed under MIT and GPL
// Array.prototype.indexOf shim
// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/indexOf
Array.prototype.indexOf || (Array.prototype.indexOf = function(n) {
	"use strict";
	if (null == this) throw new TypeError;
	var t, e, o = Object(this),
		r = o.length >>> 0;
	if (0 === r) return -1;
	if (t = 0, arguments.length > 1 && (t = Number(arguments[1]), t != t ? t = 0 : 0 != t && 1 / 0 != t && t != -1 / 0 && (t = (t > 0 || -1) * Math.floor(Math.abs(t)))), t >= r) return -1;
	for (e = t >= 0 ? t : Math.max(r - Math.abs(t), 0); r > e; e++)
		if (e in o && o[e] === n) return e;
	return -1
});
var evento = function(n) {
	var t, e, o, r = n,
		i = r.document,
		f = {};
	return t = function() {
		return "function" == typeof i.addEventListener ? function(n, t, e) {
			n.addEventListener(t, e, !1), f[n] = f[n] || {}, f[n][t] = f[n][t] || [], f[n][t].push(e)
		} : "function" == typeof i.attachEvent ? function(n, t, e) {
			n.attachEvent(t, e), f[n] = f[n] || {}, f[n][t] = f[n][t] || [], f[n][t].push(e)
		} : function(n, t, e) {
			n["on" + t] = e, f[n] = f[n] || {}, f[n][t] = f[n][t] || [], f[n][t].push(e)
		}
	}(), e = function() {
		return "function" == typeof i.removeEventListener ? function(n, t, e) {
			n.removeEventListener(t, e, !1), Helio.each(f[n][t], function(o) {
				o === e && (f[n] = f[n] || {}, f[n][t] = f[n][t] || [], f[n][t][f[n][t].indexOf(o)] = void 0)
			})
		} : "function" == typeof i.detachEvent ? function(n, t, e) {
			n.detachEvent(t, e), Helio.each(f[n][t], function(o) {
				o === e && (f[n] = f[n] || {}, f[n][t] = f[n][t] || [], f[n][t][f[n][t].indexOf(o)] = void 0)
			})
		} : function(n, t, e) {
			n["on" + t] = void 0, Helio.each(f[n][t], function(o) {
				o === e && (f[n] = f[n] || {}, f[n][t] = f[n][t] || [], f[n][t][f[n][t].indexOf(o)] = void 0)
			})
		}
	}(), o = function(n, t) {
		f[n] = f[n] || {}, f[n][t] = f[n][t] || [];
		for (var e = 0, o = f[n][t].length; o > e; e += 1) f[n][t][e]()
	}, {
		add: t,
		remove: e,
		trigger: o,
		_handlers: f
	}
}(this);

(function(window) {
	'use strict';
	// class helper functions from bonzo https://github.com/ded/bonzo
	function classReg(className) {
			return new RegExp("(^|\\s+)" + className + "(\\s+|$)");
		}
		// classList support for class management
		// altho to be fair, the api sucks because it won't accept multiple classes at once
	var hasClass, addClass, removeClass;
	if ('classList' in document.documentElement) {
		hasClass = function(elem, c) {
			if (elem !== null) return elem.classList.contains(c);
		};
		addClass = function(elem, c) {
			if (elem !== null) elem.classList.add(c);
		};
		removeClass = function(elem, c) {
			if (elem !== null) elem.classList.remove(c);
		};
	} else {
		hasClass = function(elem, c) {
			if (elem !== null) return classReg(c).test(elem.className);
		};
		addClass = function(elem, c) {
			if (!hasClass(elem, c)) {
				if (elem !== null) elem.className = elem.className + ' ' + c;
			}
		};
		removeClass = function(elem, c) {
			if (elem !== null) elem.className = elem.className.replace(classReg(c), ' ');
		};
	}

	function toggleClass(elem, c) {
		var fn = hasClass(elem, c) ? removeClass : addClass;
		fn(elem, c);
	}
	var classie = {
		// full names
		hasClass: hasClass,
		addClass: addClass,
		removeClass: removeClass,
		toggleClass: toggleClass,
		// short names
		has: hasClass,
		add: addClass,
		remove: removeClass,
		toggle: toggleClass
	};
	// transport
	if (typeof define === 'function' && define.amd) {
		// AMD
		define(classie);
	} else {
		// browser global
		window.classie = classie;
	}
})(window);
/* From Modernizr */
function whichTransitionEvent() {
		var t;
		var el = document.createElement('fakeelement');
		var transitions = {
			'transition': 'transitionend',
			'OTransition': 'oTransitionEnd',
			'MozTransition': 'transitionend',
			'WebkitTransition': 'webkitTransitionEnd'
		}
		for (t in transitions) {
			if (el.style[t] !== undefined) {
				return transitions[t];
			}
		}
	}
/**
 * Load utils - END
 */

/**
 * Start main js
 */
(function(window, undefined) {
	'use strict';
	// Init variables
	var bodyTop,
		scrollbarWidth,
		noScroll = false,
		boxEvent = new CustomEvent('boxResized'),
		boxWidth = 0,
		boxLeft = 0,
		parallaxRows,
		parallaxCols,
		parallaxHeaders,
		headerWithOpacity,
		speedDivider = 0.25,
		pageHeader,
		masthead,
		menuwrapper,
		menuhide,
		menusticky,
		menuHeight = 0,
		mainmenu,
		secmenu,
		secmenuHeight = 0,
		transmenuHeight = 0,
		header,
		transmenuel,
		logo,
		logoel,
		logolink,
		logoMinScale,
		lastScrollValue = 0,
		deltaY,
		overHtml = true,
		wwidth = window.innerWidth || document.documentElement.clientWidth,
		wheight = window.innerHeight || document.documentElement.clientHeight,
		boxWrapper,
		docheight = 0,
		isMobile = classie.hasClass(document.documentElement, 'touch') ? true : false,
		isIE = classie.hasClass(document.documentElement, 'ie') || classie.hasClass(document.documentElement, 'opera12') ? true : false,
		isFF = classie.hasClass(document.documentElement, 'firefox') ? true : false,
		transitionEvent = whichTransitionEvent(),
		footerScroller = false,
		mediaQuery = 959,
		mediaQueryMobile = 569,
		resizeTimer,
	initBox = function() {
			if (!isMobile && scrollbarWidth == undefined) {
				// Create the measurement node
				var scrollDiv = document.createElement("div");
				scrollDiv.className = "scrollbar-measure";
				var dombody = document.body;
				if (dombody != null) {
					dombody.appendChild(scrollDiv);
					// Get the scrollbar width
					scrollbarWidth = 0;
					// Delete the DIV
					dombody.removeChild(scrollDiv);
				}
			}
			if (!isMobile) {
				forEachElement('.box-container', function(el, i) {
					if (!classie.hasClass(el, 'limit-width')) {
						var elWidth = outerWidth(el),
							newWidth = 12 * Math.ceil((wwidth - scrollbarWidth) / 12);
						boxWidth = newWidth;
						boxLeft = Math.ceil(((newWidth) - wwidth) / 2);
						el.style.width = boxWidth + 'px';
						el.style.marginLeft = '-' + (boxLeft + (scrollbarWidth / 2)) + 'px';
						if (mainmenu != undefined && mainmenu[0] != undefined) {
							mainmenu[0].style.width = newWidth + 'px';
						}
					}
				});
			}
		},
		fixMenuHeight = function() {
			if (!classie.hasClass(document.body, 'vmenu')) noScroll = true;
			menuwrapper = document.querySelectorAll(".menu-wrapper");
			masthead = document.getElementById("masthead");
			menuhide = document.querySelector('#masthead .menu-hide');
			menusticky = document.querySelectorAll('.menu-sticky');
			transmenuel = document.querySelectorAll('.menu-transparent:not(.vmenu-container)');
			var menuItemsButton = document.querySelectorAll('.menu-item-button .menu-btn-table');
			logo = document.querySelector('#main-logo');
			if (logo != undefined) logolink = (logo.firstElementChild || logo.firstChild);
			if (logolink != undefined) logoMinScale = logolink.getAttribute("data-minheight");
			logoel = document.querySelectorAll('.menu-shrink .logo-container');
			mainmenu = (wwidth > mediaQuery) ? document.querySelectorAll('.menu-primary .menu-container') : document.querySelectorAll('.menu-primary .menu-container, .vmenu-container .logo-container');
			secmenu = document.querySelectorAll('.menu-secondary');
			for (var k = 0; k < menuItemsButton.length; k++) {
				var a_item = menuItemsButton[k].parentNode,
					buttonHeight = outerHeight(menuItemsButton[k]);
				a_item.style.height = buttonHeight + 'px';
			}
		},
		initHeader = function() {
			UNCODE.adaptive();
			headerHeight('.header-wrapper');
			parallaxHeaders = document.querySelectorAll('.header-parallax > .header-bg-wrapper > .header-bg');
			header = document.querySelectorAll('.header-wrapper.header-uncode-block, .header-wrapper.header-revslider, .header-basic .header-wrapper, .header-uncode-block > .row-container:first-child > .row > .row-inner > .col-lg-12 > .uncol, .header-uncode-block .uncode-slider .owl-carousel > .row-container:first-child .col-lg-12 .uncoltable');
			headerWithOpacity = document.querySelectorAll('.header-scroll-opacity');
			pageHeader = document.getElementById("page-header");
			if (pageHeader != undefined) {
				var backs = pageHeader.querySelectorAll('.header-bg'),
					backsCarousel = pageHeader.querySelectorAll('.header-uncode-block .background-inner'),
					uri_pattern = /\b((?:[a-z][\w-]+:(?:\/{1,3}|[a-z0-9%])|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}\/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'".,<>?«»“”‘’]))/ig;
				if (backs.length == 0 && backsCarousel.length == 0) {
					pageHeader.setAttribute('data-imgready', 'true');
				} else {
					if (backsCarousel.length) {
						for (var j = 0; j < backsCarousel.length; j++) {
							if (j == 0) {
								if (!!backsCarousel[j].style.backgroundImage && backsCarousel[j].style.backgroundImage !== void 0) {
									var url = (backsCarousel[j].style.backgroundImage).match(uri_pattern),
										image = new Image();
									image.onload = function() {
										pageHeader.setAttribute('data-imgready', 'true');
									};
									image.src = url[0];
								} else {
									pageHeader.setAttribute('data-imgready', 'true');
								}
							}
						}
					} else {
						for (var i = 0; i < backs.length; i++) {
							if (i == 0) {
								if (!!backs[i].style.backgroundImage && backs[i].style.backgroundImage !== void 0) {
									var url = (backs[i].style.backgroundImage).match(uri_pattern),
										image = new Image();
									image.onload = function() {
										pageHeader.setAttribute('data-imgready', 'true');
									};
									image.src = url[0];
								} else {
									pageHeader.setAttribute('data-imgready', 'true');
								}
							}
						}
					}
				}
			}
			if (masthead != undefined) {
				if (header.length) {
					classie.addClass(menuwrapper[0], 'with-header');
					for (var j = 0; j < header.length; j++) {
						var headerel = header[j],
							closestStyle = getClosest(headerel, 'style-light');
						if (closestStyle != null && classie.hasClass(closestStyle, 'style-light')) switchColorsMenu(0, 'light');
						else if (getClosest(headerel, 'style-dark') != null) switchColorsMenu(0, 'dark');
						else {
							if (masthead.style.opacity !== 1) masthead.style.opacity = 1;
						}
						if (classie.hasClass(masthead, 'menu-transparent')) {
							masthead.parentNode.style.height = '0px';
							if (wwidth > mediaQuery) {
								if (classie.hasClass(masthead, 'menu-add-padding')) {
									var headerBlock = getClosest(headerel, 'header-uncode-block');
									if (headerBlock != null) {
										var innerRows = headerel.querySelectorAll('.column_parent > .uncol > .uncoltable > .uncell > .uncont, .uncode-slider .column_child > .uncol > .uncoltable > .uncell > .uncont');
										for (var k = 0; k < innerRows.length; k++) {
											if (innerRows[k] != undefined) {
												innerRows[k].style.paddingTop = transmenuHeight + 'px';
											}
										}
									} else {
										getDivChildren(headerel, '.header-content', function(headerContent, i) {
											headerContent.style.paddingTop = transmenuHeight + 'px';
										});
									}
								}
							}
						}
					}
				} else {
					classie.addClass(menuwrapper[0], 'no-header');
					classie.removeClass(masthead, 'menu-transparent');
					transmenuHeight = 0;
				}
			}
			bodyTop = document.documentElement['scrollTop'] || document.body['scrollTop'];
			if (wwidth > mediaQuery) scrollFunction();
			showHideScrollup(bodyTop);
		},
		initRow = function(currentRow) {
			UNCODE.adaptive();
			var el = currentRow.parentNode.parentNode.getAttribute("data-parent") == 'true' ? currentRow.parentNode : currentRow.parentNode.parentNode,
				rowParent = el.parentNode,
				rowInner = currentRow.parentNode,
				percentHeight = el.getAttribute("data-height-ratio"),
				minHeight = el.getAttribute("data-minheight"),
				calculateHeight,
				calculatePadding = 0,
				isHeader = false,
				isFirst = false,
				uri_pattern = /\b((?:[a-z][\w-]+:(?:\/{1,3}|[a-z0-9%])|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}\/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'".,<>?«»“”‘’]))/ig;

			/** Add class to the row when contains responsive column size */
			getDivChildren(el.parentNode, '.column_parent, .column_child', function(obj, i, total) {
				if ((obj.className).indexOf("col-md-") > - 1) classie.addClass(obj.parentNode, 'cols-md-responsive');
			});

			setRowHeight(el);

			var elements = 0;
			getDivChildren(el, '.row-internal .background-inner', function(obj, i, total) {
				elements++;
				if (i == 0) {
					if (!!obj.style.backgroundImage && obj.style.backgroundImage !== void 0) {
						var url = (obj.style.backgroundImage).match(uri_pattern),
							image = new Image();
						image.onload = function() {
							el.setAttribute('data-imgready', 'true');
							el.dispatchEvent(new CustomEvent('imgLoaded'));
						};
						image.src = url[0];
					} else {
						el.setAttribute('data-imgready', 'true');
						el.dispatchEvent(new CustomEvent('imgLoaded'));
					}
				}
			});
			if (elements == 0) {
				el.setAttribute('data-imgready', 'true');
			}

			/** init parallax is not mobile */
			if (!UNCODE.isMobile) {
				parallaxRows = el.parentNode.parentNode.querySelectorAll('.with-parallax > .row-background > .background-wrapper');
				parallaxCols = el.querySelectorAll('.with-parallax > .column-background > .background-wrapper');
				bodyTop = document.documentElement['scrollTop'] || document.body['scrollTop'];
				parallaxRowCol(bodyTop);
			}
		},
		setRowHeight = function(container, forced) {
			var currentTallest = 0,
				percentHeight = 0,
				minHeight = 0,
				el,
				child,
				hasSubCols = false;
			if (container.length == undefined) {
				container = [container];
			}
			// Loop for each container element to match their heights
			for (var i = 0; i < container.length; i++) {
				var el = container[i],
					$row = el,
					totalHeight = 0,
					colsArray = new Array(),
					calculatePadding = 0,
					$rowParent = $row.parentNode,
					isHeader = false,
					isFirst = false;
				$row.oversized = false;
				percentHeight = el.getAttribute("data-height-ratio");
				minHeight = el.getAttribute("data-minheight");
				child = (el.firstElementChild || el.firstChild);
				var childHeight = outerHeight(child);
				/** window height without header **/
				if (!!percentHeight || !!minHeight || forced || (isIE && classie.hasClass(el, 'unequal'))) {
					child.style.height = '';
					if (!!percentHeight) {
						if (percentHeight == 'full') {
							currentTallest = parseInt(wheight);
						} else {
							currentTallest = parseInt((wheight * percentHeight) / 100);
						}
					} else {
						currentTallest = el.clientHeight;
					}

					if (!!minHeight) {
						if (currentTallest < minHeight || currentTallest == undefined) currentTallest = parseInt(minHeight);
					}

					var computedStyleRow = getComputedStyle(el),
						computedStyleRowParent = getComputedStyle($rowParent);
					calculatePadding -= (parseFloat(computedStyleRow.paddingTop) + parseFloat(computedStyleRowParent.paddingTop));
					calculatePadding -= (parseFloat(computedStyleRow.paddingBottom) + parseFloat(computedStyleRowParent.paddingBottom));
					if (getClosest(el, 'header-uncode-block') != null) {
						el.setAttribute('data-row-header', 'true');
						isHeader = true;
					} else {
						if (pageHeader == null) {
							var prevRow = $rowParent.previousSibling;
							if (prevRow != null && prevRow.innerText == 'UNCODE.initHeader();') {
								isFirst = true;
							}
						}
					}

					if (classie.hasClass(el, 'row-slider')) {
						percentHeight = el.getAttribute("data-height-ratio");
						minHeight = el.getAttribute("data-minheight");
						if (percentHeight == 'full') {
							currentTallest = parseInt(wheight);
						} else {
							currentTallest = parseInt((wheight * percentHeight) / 100);
						}

						if (isHeader || isFirst) {
							if (wwidth > mediaQuery) currentTallest -= menuHeight - transmenuHeight;
							else currentTallest -= menuHeight - secmenuHeight;
							currentTallest += calculatePadding;
						} else {
							if (wwidth > mediaQuery) currentTallest += calculatePadding;
							else currentTallest = 'auto';
						}
						getDivChildren(el, '.owl-carousel', function(owl, i) {
							owl.style.height = (currentTallest == 'auto' ? 'auto' : currentTallest + 'px');
							if (isIE) {
								getDivChildren(owl, '.owl-stage', function(owlIn, i) {
									owlIn.style.height = (currentTallest == 'auto' ? '100%' : currentTallest + 'px');
								});
							}
						});
						continue;
					} else {
						if (isHeader || isFirst) {
							if (wwidth > mediaQuery) currentTallest -= menuHeight - transmenuHeight;
							else currentTallest -= menuHeight - secmenuHeight;
							currentTallest += calculatePadding;
						} else {
							if (wwidth > mediaQuery) currentTallest += calculatePadding;
							else currentTallest = 'auto';
						}
					}
					if (!!minHeight) {
						if (currentTallest < minHeight || currentTallest == 'auto') currentTallest = parseInt(minHeight);
					}

					child.style.height = (currentTallest == 'auto' ? 'auto' : currentTallest + 'px');
				} else {
					currentTallest = 0;
				}

				if (wwidth > mediaQuery) {

					getDivChildren(el, '.column_parent', function(col, i, total) {
						var $col = col,
							$colHeight = 0,
							$colDiff = 0,
							$colPercDiff = 100;
						$col.oversized = false;
						$col.forceHeight = currentTallest;
						currentTallest = child.clientHeight;
						if ((isHeader || isFirst) && currentTallest != 'auto') currentTallest -= transmenuHeight;
						var getFirstCol = null;
						var getMargin = 0;
						getDivChildren(col, '.row-child', function(obj, i, total) {
							var $colChild = obj,
								$colInner = $colChild.children[0],
								$colParent = $colChild.parentNode,
								$uncont = $colParent.parentNode;
							if (i == 0 && total > 1) getFirstCol = $colInner;
							$colChild.oversized = false;
							percentHeight = $colChild.getAttribute("data-height");
							minHeight = $colChild.getAttribute("data-minheight");
							if (percentHeight != null || minHeight != null) {
								$colInner.style.height = '';
								$colParent.style.height = 'auto';
								$uncont.style.height = '100%';
								$colChild.removeAttribute("style");
								var newHeight = (percentHeight != null) ? Math.ceil((currentTallest) * (percentHeight / 100)) : parseInt(minHeight);
								var computedStyleCol = getComputedStyle($colParent);
								parseFloat(computedStyleCol.marginTop);
								getMargin = parseFloat(computedStyleCol.marginTop);
								newHeight -= (getMargin);
								if (currentTallest > newHeight) {
									var getColHeight = outerHeight($colChild);
									if (getColHeight > newHeight) {
										$colHeight += getColHeight;
										$colDiff += getColHeight;
										$colPercDiff -= (percentHeight != null) ? percentHeight : 0;
										$colChild.oversized = true;
										$col.oversized = true;
										$row.oversized = true;
									} else {
										$colHeight += newHeight;
										$colInner.style.height = newHeight + 'px';
									}
								}
							} else {
								$colHeight += outerHeight($colChild);
							}
						});
						if (getFirstCol != null) {
							getFirstCol.style.height = (parseFloat(getFirstCol.style.height) - getMargin) + 'px';
						}
						colsArray.push({
							colHeight: $colHeight,
							colDiv: $col
						});
						$col.colDiff = $colDiff;
						$col.colPercDiff = $colPercDiff;
					});

					if ($row.oversized) {
						child.style.height = '';
						colsArray.sort(function(a, b) {
							if (a.colHeight < b.colHeight) return 1;
							if (a.colHeight > b.colHeight) return -1;
							return 0;
						});
						var $totalHeight = 0;
						colsArray.forEach(function(col) {
							var $col = col.colDiv,
								$colHeight = col.colHeight;
							getDivChildren($col, '.row-child', function(obj, i, total) {
								var $colChild = obj,
									$colInner = $colChild.children[0],
									percentHeight = $colChild.getAttribute("data-height"),
									$colParent = $colChild.parentNode,
									$uncont = $colParent.parentNode,
									newHeight;
								$colHeight = $col.forceHeight - $col.colDiff;
								if (percentHeight != null) {
									if ($colHeight > 0) {
										if ($col.oversized) {
											if (!$colChild.oversized) {
												newHeight = Math.ceil(($colHeight) * (percentHeight / $col.colPercDiff));
												if (i == total - 1 && total > 1) {
													$uncont.style.height = 'auto';
													$colChild.style.display = 'none';
													newHeight = outerHeight($col.parentNode) - outerHeight($uncont);
													$uncont.style.height = '100%';
													$colChild.style.display = 'table';
												}
												if (newHeight == 0) newHeight = Math.ceil(($col.forceHeight) * (percentHeight / 100));
												$colInner.style.height = newHeight + 'px';
											}
										} else {
											if ($totalHeight == 0) newHeight = Math.ceil(($colHeight) * (percentHeight / $col.colPercDiff));
											else {
												newHeight = Math.ceil(($totalHeight) * (percentHeight / $col.colPercDiff));
											}
											if (i == total - 1 && total > 1) {
												$uncont.style.height = 'auto';
												$colChild.style.display = 'none';
												newHeight = outerHeight($col.parentNode) - outerHeight($uncont);
												$uncont.style.height = '100%';
												$colChild.style.display = 'table';
											}
											$colInner.style.height = newHeight + 'px';
										}
									} else {
										if ($colChild.oversized) {
											if ($totalHeight == 0) newHeight = Math.ceil(($colHeight) * (percentHeight / $col.colPercDiff));
											else {
												if ($col.colPercDiff == 0) $col.colPercDiff = 100;
												newHeight = Math.ceil(($totalHeight) * (percentHeight / $col.colPercDiff));
											}
											if (i == total - 1 && total > 1) {
												$uncont.style.height = 'auto';
												$colChild.style.display = 'none';
												newHeight = outerHeight($col.parentNode) - outerHeight($uncont);
												$uncont.style.height = '100%';
												$colChild.style.display = 'table';
											}
											$colInner.style.height = newHeight + 'px';
										}
									}
								}
							});
							var uncell = $col.getElementsByClassName('uncell');
							if (uncell[0] != undefined && $totalHeight == 0) $totalHeight = outerHeight(uncell[0]);
						});
					}
					if (isFF) {
						getDivChildren(el, '.uncoltable', function(col, i, total) {
							if (col.style.minHeight != '') {
								col.style.height = '';
							}
						});
					}
					getDivChildren(el, '.row-child > .row-inner', function(obj, k, total) {
						if (obj.style.height == '') {
							if (wwidth > mediaQueryMobile) {
								var getStyle = (window.getComputedStyle((obj.parentNode), null)),
								getInnerHeight = (parseInt(getStyle.height) - parseInt(getStyle.paddingTop) - parseInt(getStyle.paddingBottom));
								obj.style.height = getInnerHeight + 1 + 'px';
								obj.style.marginBottom = '-1px';
							}
						}
					});
				} else {
					if (isFF) {
						getDivChildren(el, '.uncoltable', function(col, i, total) {
							if (col.style.minHeight != '') {
								col.style.height = '';
								col.style.height = outerHeight(col.parentNode) + 'px';
							}
						});
					}
					if (isIE && (wwidth > mediaQueryMobile)) {
						if (child.style.height == 'auto') {
							child.style.height = outerHeight(child) + 'px';
						}
					}
				}
				if (isFF) {
					var sliderColumnFix = document.querySelector('.uncode-slider .row-inner > .column_child:only-child');
					if (sliderColumnFix != null) {
						if (wwidth > mediaQuery) {
							sliderColumnFix.style.setProperty("height", "");
						} else {
							sliderColumnFix.style.setProperty("height", "");
							sliderColumnFix.style.setProperty("height", outerHeight(sliderColumnFix.parentNode) + "px", "important");
						}
					}
				}
			};
		},
		headerHeight = function(container) {
			forEachElement(container, function(el, i) {
				var getHeight = el.getAttribute("data-height"),
					newHeight = ((wheight * getHeight) / 100);
				if (getHeight != 'fixed' && newHeight != 0) {
					if (wwidth > mediaQuery) newHeight -= menuHeight - transmenuHeight;
					else newHeight -= menuHeight - secmenuHeight;
					el.style.height = newHeight + 'px';
				}
			});
			if (masthead != undefined) {
				if (header != undefined && header.length) {
					if (classie.hasClass(masthead, 'menu-transparent')) {
						if (wwidth > mediaQuery) {
							if (classie.hasClass(masthead, 'menu-add-padding')) {
								for (var j = 0; j < header.length; j++) {
									var headerel = header[j];
									var headerBlock = getClosest(headerel, 'header-uncode-block');
									if (headerBlock != null) {
										var innerRows = headerel.querySelectorAll('.column_parent > .uncol > .uncoltable > .uncell > .uncont, .uncode-slider .column_child > .uncol > .uncoltable > .uncell > .uncont');
										for (var k = 0; k < innerRows.length; k++) {
											if (innerRows[k] != undefined) {
												innerRows[k].style.paddingTop = transmenuHeight + 'px';
											}
										}
									} else {
										getDivChildren(headerel, '.header-content', function(headerContent, i) {
											headerContent.style.paddingTop = transmenuHeight + 'px';
										});
									}
								}
							}
						}
					}
				}
			}
		},
		initVideoComponent = function(container, classTarget) {
			getDivChildren(container, classTarget, function(el, i) {
				var width = outerWidth(el),
					pWidth, // player width, to be defined
					height = outerHeight(el),
					pHeight, // player height, tbd
					$tubularPlayer = (el.getElementsByTagName('iframe').length == 1) ? el.getElementsByTagName('iframe') : el.getElementsByTagName('video'),
					ratio = (el.getAttribute("data-ratio") != null) ? Number(el.getAttribute("data-ratio")) : null,
					heightOffset = 80,
					widthOffset = heightOffset * ratio;
				// when screen aspect ratio differs from video, video must center and underlay one dimension
				if ($tubularPlayer[0] != undefined) {
					if (width / ratio < height) { // if new video height < window height (gap underneath)
						pWidth = Math.ceil((height + heightOffset) * ratio); // get new player width
						$tubularPlayer[0].style.width = pWidth + widthOffset + 'px';
						$tubularPlayer[0].style.height = height + heightOffset + 'px';
						$tubularPlayer[0].style.left = ((width - pWidth) / 2) - (widthOffset / 2) + 'px';
						$tubularPlayer[0].style.top = '-' + (heightOffset / 2) + 'px';
						$tubularPlayer[0].style.position = 'absolute';
					} else { // new video width < window width (gap to right)
						pHeight = Math.ceil(width / ratio); // get new player height
						$tubularPlayer[0].style.width = width + widthOffset + 'px';
						$tubularPlayer[0].style.height = pHeight + heightOffset + 'px';
						$tubularPlayer[0].style.left = '-' + (widthOffset / 2) + 'px';
						$tubularPlayer[0].style.top = ((height - pHeight) / 2) - (heightOffset / 2) + 'px';
						$tubularPlayer[0].style.position = 'absolute';
					}
				}
			});
		},
		init_overlay = function() {
			var triggerButton,
			closeButton;
			function toggleOverlay(btn) {
				if (!classie.hasClass(triggerButton, 'search-icon')) {
					if (classie.hasClass(triggerButton, 'close')) {
						classie.removeClass(closeButton, 'close');
						classie.removeClass(triggerButton, 'close');
						classie.addClass(closeButton, 'closing');
						classie.addClass(triggerButton, 'closing');
						setTimeout(function() {
							classie.removeClass(closeButton, 'closing');
							classie.removeClass(triggerButton, 'closing');
							triggerButton.style.opacity = 1;
							closeButton.style.opacity = 0;
						}, 800);
					} else {
						var getBtnRect = triggerButton.getBoundingClientRect();
						closeButton.parentNode.setAttribute('style', 'top:' + getBtnRect.top + 'px; left:'+getBtnRect.left + 'px !important');
						classie.addClass(closeButton, 'close');
						classie.addClass(triggerButton, 'close');
						triggerButton.style.opacity = 0;
						closeButton.style.opacity = 1;
					}
				}
				Array.prototype.forEach.call(document.querySelectorAll('div.overlay'), function(overlay) {
					if (btn.getAttribute('data-area') == overlay.getAttribute('data-area')) {
						var container = document.querySelector('div.' + btn.getAttribute('data-container')),
							inputField = overlay.querySelector('.search-field');
						if (classie.has(overlay, 'open')) {
							classie.remove(overlay, 'open');
							classie.remove(container, 'overlay-open');
							classie.add(overlay, 'close');
							classie.remove(overlay, 'open-items');
							var onEndTransitionFn = function(ev) {
								if (transitionEvent) {
									if (ev.propertyName !== 'visibility') return;
									this.removeEventListener(transitionEvent, onEndTransitionFn);
								}
								classie.remove(overlay, 'close');
							};
							if (transitionEvent) {
								overlay.addEventListener(transitionEvent, onEndTransitionFn);
							} else {
								onEndTransitionFn();
							}
						} else if (!classie.has(overlay, 'close')) {
							classie.add(overlay, 'open');
							classie.add(container, 'overlay-open');
							if (jQuery('body.menu-overlay').length == 0) {
								setTimeout(function() {
									inputField.focus();
								}, 1000);
							}
							setTimeout(function() {
								if (classie.has(overlay, 'overlay-sequential')) classie.add(overlay, 'open-items');
							}, 800);
						}
					}
				});
			}
			Array.prototype.forEach.call(document.querySelectorAll('.trigger-overlay'), function(triggerBttn) {
				triggerButton = triggerBttn;
				triggerBttn.addEventListener('click', function(e) {
					if (wwidth > mediaQuery) toggleOverlay(triggerBttn);
					e.preventDefault();
					return false;
				}, false);
			});
			Array.prototype.forEach.call(document.querySelectorAll('.overlay-close'), function(closeBttn) {
				closeButton = closeBttn;
				closeBttn.addEventListener('click', function(e) {
					if (wwidth > mediaQuery) toggleOverlay(closeBttn);
					e.preventDefault();
					return false;
				}, false);
			});
		},
		/** All scrolling functions - Begin */
		/** Shrink menu **/
		shrinkMenu = function(bodyTop) {
			var logoShrink,
				offset = 100;
			for (var i = 0; i < logoel.length; i++) {
				if (((secmenuHeight == 0) ? bodyTop > menuHeight : bodyTop > secmenuHeight + offset) && !classie.hasClass(logoel[i], 'shrinked')) {
					classie.addClass(logoel[i], 'shrinked');
					if (logoMinScale != undefined) {
						logoShrink = logolink.children;
						Array.prototype.forEach.call(logoShrink, function(singleLogo) {
							singleLogo.style.height = logoMinScale + 'px';
							singleLogo.style.lineHeight = logoMinScale + 'px';
							if (classie.hasClass(singleLogo, 'text-logo')) singleLogo.style.fontSize = logoMinScale + 'px';
						});
					}
				} else if (((secmenuHeight == 0) ? bodyTop == 0 : bodyTop <= secmenuHeight + offset) && classie.hasClass(logoel[i], 'shrinked')) {
					classie.removeClass(logoel[i], 'shrinked');
					if (logoMinScale != undefined) {
						logoShrink = logolink.children;
						Array.prototype.forEach.call(logoShrink, function(singleLogo) {
							singleLogo.style.height = singleLogo.getAttribute('data-maxheight') + 'px';
							singleLogo.style.lineHeight = singleLogo.getAttribute('data-maxheight') + 'px';
							if (classie.hasClass(singleLogo, 'text-logo')) singleLogo.style.fontSize = singleLogo.getAttribute('data-maxheight') + 'px';
						});
					}
				}
			}
		},
		/** Switch colors menu **/
		switchColorsMenu = function(bodyTop, style) {
			for (var i = 0; i < transmenuel.length; i++) {
				if (masthead.style.opacity !== 1) masthead.style.opacity = 1;
				if ((secmenuHeight == 0) ? bodyTop > menuHeight / 2 : bodyTop > secmenuHeight) {
					if (classie.hasClass(masthead, 'style-dark-original')) {
						logo.className = logo.className.replace("style-light", "style-dark");
					}
					if (classie.hasClass(masthead, 'style-light-original')) {
						logo.className = logo.className.replace("style-dark", "style-light");
					}
					if (style != undefined) {
						if (style == 'dark') {
							classie.removeClass(transmenuel[i], 'style-light-override');
						}
						if (style == 'light') {
							classie.removeClass(transmenuel[i], 'style-dark-override');
						}
						classie.addClass(transmenuel[i], 'style-' + style + '-override');
					}
				} else {
					if (style != undefined) {
						if (style == 'dark') {
							classie.removeClass(transmenuel[i], 'style-light-override');
						}
						if (style == 'light') {
							classie.removeClass(transmenuel[i], 'style-dark-override');
						}
						classie.addClass(transmenuel[i], 'style-' + style + '-override');
					}
				}
			}
			if (pageHeader != undefined) {
				if (style != undefined) {
					if (classie.hasClass(pageHeader, 'header-style-dark')) {
						classie.removeClass(pageHeader, 'header-style-dark');
					}
					if (classie.hasClass(pageHeader, 'header-style-light')) {
						classie.removeClass(pageHeader, 'header-style-light');
					}
					classie.addClass(pageHeader, 'header-style-' + style);
				}
			}
		},
		/** Parallax Rows **/
		parallaxRowCol = function(bodyTop) {
			var value;
			if (typeof parallaxRows == 'object') {
				for (var i = 0; i < parallaxRows.length; i++) {
					var section = parallaxRows[i].parentNode,
						thisHeight = outerHeight(parallaxRows[i]),
						sectionHeight = outerHeight(section),
						offSetTop = bodyTop + (section != null ? (classie.hasClass(section.parentNode.parentNode, 'owl-carousel') ? section.parentNode.parentNode.getBoundingClientRect().top : section.getBoundingClientRect().top) : 0),
						offSetPosition = wheight + bodyTop - offSetTop;
					if (offSetPosition > 0 && offSetPosition < (sectionHeight + wheight)) {
						value = ((offSetPosition - wheight) * speedDivider);
						if (Math.abs(value) < (thisHeight - sectionHeight)) {
							translateElement(parallaxRows[i], value);
						} else {
							translateElement(parallaxRows[i], thisHeight - sectionHeight);
						}
					}
				}
			}
			if (typeof parallaxCols == 'object') {
				for (var j = 0; j < parallaxCols.length; j++) {
					var section = parallaxCols[j].parentNode,
						thisHeight = outerHeight(parallaxCols[j]),
						sectionHeight = outerHeight(section),
						offSetTop = bodyTop + (section != null ? section.getBoundingClientRect().top : 0),
						offSetPosition = wheight + bodyTop - offSetTop;
					if (offSetPosition > 0 && offSetPosition < (sectionHeight + wheight)) {
						value = ((offSetPosition - wheight) * speedDivider);
						value *= .8;
						if (Math.abs(value) < (thisHeight - sectionHeight)) {
							translateElement(parallaxCols[j], value);
						} else {
							translateElement(parallaxCols[j], thisHeight - sectionHeight);
						}
					}
				}
			}
		},
		/** Parallax Headers **/
		parallaxHeader = function(bodyTop) {
			var value;
			if (typeof parallaxHeaders == 'object') {
				for (var i = 0; i < parallaxHeaders.length; i++) {
					var section = parallaxHeaders[i].parentNode,
						thisSibling = section.nextSibling,
						thisHeight,
						sectionHeight,
						offSetTop,
						offSetPosition;
					if (classie.hasClass(parallaxHeaders[i], 'header-carousel-wrapper')) {
						getDivChildren(parallaxHeaders[i], '.t-background-cover', function(item, l, total) {
							thisHeight = outerHeight(item);
							sectionHeight = outerHeight(section);
							offSetTop = bodyTop + section.getBoundingClientRect().top;
							offSetPosition = wheight + bodyTop - offSetTop;
							if (offSetPosition > 0 && offSetPosition < (sectionHeight + wheight)) {
								value = ((offSetPosition - wheight) * speedDivider);
								if (Math.abs(value) < (thisHeight - sectionHeight)) {
									translateElement(item, value);
								}
							}
						});
					} else {
						thisHeight = outerHeight(parallaxHeaders[i]);
						sectionHeight = outerHeight(section);
						offSetTop = bodyTop + section.getBoundingClientRect().top;
						offSetPosition = wheight + bodyTop - offSetTop;
						if (offSetPosition > 0 && offSetPosition < (sectionHeight + wheight)) {
							value = ((offSetPosition - wheight) * speedDivider);
							if (Math.abs(value) < (thisHeight - sectionHeight)) {
								translateElement(parallaxHeaders[i], value);
							}
						}
					}
				}
			}
		},
		/** Header opacity **/
		headerOpacity = function(bodyTop) {
			if (headerWithOpacity && headerWithOpacity.length) {
				var thisHeight = outerHeight(headerWithOpacity[0]);
				if (bodyTop > thisHeight / 8) {
					if (pageHeader != undefined) classie.addClass(pageHeader, 'header-scrolled');
				} else {
					if (pageHeader != undefined) classie.removeClass(pageHeader, 'header-scrolled');
				}
			}
		},
		/** Show hide scroll top arrow **/
		showHideScrollup = function(bodyTop) {
			if (bodyTop != 0) {
				if (bodyTop > wheight || ((bodyTop + wheight) >= docheight) && docheight > 0) {
					classie.addClass(document.body, 'window-scrolled');
					classie.removeClass(document.body, 'hide-scrollup');
					if (footerScroller && footerScroller[0] != undefined) {
						footerScroller[0].style.display = '';
					}
				} else {
					if (classie.hasClass(document.body, 'window-scrolled')) classie.addClass(document.body, 'hide-scrollup');
					classie.removeClass(document.body, 'window-scrolled');
				}
			}
		},
		/** Hide Menu **/
		hideMenu = function(bodyTop) {
			if (typeof menuhide == 'object' && menuhide != null) {
				var translate;
				if (lastScrollValue > bodyTop) {
					if (!UNCODE.scrolling) {
						if (classie.hasClass(menuhide, 'menu-hided')) {
							classie.removeClass(menuhide, 'menu-hided');
							translateElement(menuhide, 0);
							if (mainmenu[0].style.position != 'fixed') {
								classie.addClass(mainmenu[0].parentNode, 'is_stuck');
								mainmenu[0].style.position = 'fixed';
								mainmenu[0].style.top = '0';
								if (!classie.hasClass(document.body, 'boxed-width')) mainmenu[0].style.width = boxWidth + 'px';
							}
						} else {
							if ((secmenuHeight == 0) ? bodyTop == 0 : bodyTop == secmenuHeight) {
								if (mainmenu[0].style.position == 'fixed') {
									classie.removeClass(mainmenu[0].parentNode, 'is_stuck');
									mainmenu[0].style.position = '';
									mainmenu[0].style.top = '';
								}
							}
						}
					}
				} else if (lastScrollValue < bodyTop) {
					if (bodyTop > wheight / 2 || UNCODE.scrolling) {
						if (!classie.hasClass(menuhide, 'menu-hided')) {
							classie.addClass(menuhide, 'menu-hided');
							translateElement(menuhide, -menuHeight);
						}
					}
				}
				lastScrollValue = bodyTop;
			}
		},
		/** Stick Menu **/
		stickMenu = function(bodyTop) {
			if (header) {
				if ((secmenuHeight == 0) ? bodyTop > 0 : bodyTop > secmenuHeight) {
					if (mainmenu[0].style.position != 'fixed') {
						classie.addClass(mainmenu[0].parentNode, 'is_stuck');
						mainmenu[0].style.position = 'fixed';
						if (document.documentElement.style.marginTop !== '') mainmenu[0].style.top = document.documentElement.style.marginTop;
						else mainmenu[0].style.top = '0';
						if (!classie.hasClass(document.body, 'boxed-width')) mainmenu[0].style.width = boxWidth + 'px';
					}
				} else {
					if (mainmenu[0].style.position != 'absolute') {
						classie.removeClass(mainmenu[0].parentNode, 'is_stuck');
						mainmenu[0].style.position = 'absolute';
						mainmenu[0].style.top = '';
					}
				}
			}
		},
		translateElement = function(element, valueY) {
			var translate = 'translate3d(0, ' + valueY + 'px' + ', 0)';
			element.style['-webkit-transform'] = translate;
			element.style['-moz-transform'] = translate;
			element.style['-ms-transform'] = translate;
			element.style['-o-transform'] = translate;
			element.style['transform'] = translate;
		},
		scrollFunction = function() {
			if (logoel != undefined && logoel.length) shrinkMenu(bodyTop);
			if (menusticky != undefined && menusticky.length && !isMobile) stickMenu(bodyTop);
			if (!isMobile) hideMenu(bodyTop);
			if (header && menusticky != undefined && menusticky.length) switchColorsMenu(bodyTop);
			parallaxRowCol(bodyTop);
			parallaxHeader(bodyTop);
			headerOpacity(bodyTop);
		};
	if (!noScroll) {
		window.addEventListener('scroll', function(e) {
			bodyTop = document.documentElement.scrollTop || document.body.scrollTop;
			if (wwidth > mediaQuery && !isMobile) {
				scrollFunction();
			}
			showHideScrollup(bodyTop);
		}, false);
	}
	/** All scrolling functions - End */
	/** help functions */
	function getClosest(el, tag) {
		do {
			if (el.className != undefined && el.className.indexOf(tag) > -1) return el;
		} while (el = el.parentNode);
		// not found :(
		return null;
	}

	function outerHeight(el, includeMargin) {
		if (el != null) {
			var height = el.offsetHeight;
			if (includeMargin) {
				var style = el.currentStyle || getComputedStyle(el);
				height += parseInt(style.marginTop) + parseInt(style.marginBottom);
			}
			return height;
		}
	}

	function outerWidth(el, includeMargin) {
			var width = el.offsetWidth;
			if (includeMargin) {
				var style = el.currentStyle || getComputedStyle(el);
				width += parseInt(style.marginLeft) + parseInt(style.marginRight);
			}
			return width;
		}
		// Replicate jQuery .each method
	function forEachElement(selector, fn) {
		var elements = document.querySelectorAll(selector);
		for (var i = 0; i < elements.length; i++) fn(elements[i], i);
	}

	function getDivChildren(containerId, selector, fn) {
		if (containerId !== null) {
			var elements = containerId.querySelectorAll(selector);
			for (var i = 0; i < elements.length; i++) fn(elements[i], i, elements.length);
		}
	}

	function hideFooterScroll() {
		if (classie.hasClass(document.body, 'hide-scrollup')) footerScroller[0].style.display = "none";
	}
	document.addEventListener("DOMContentLoaded", function(event) {
		UNCODE.adaptive();
		boxWrapper = document.querySelectorAll('.box-wrapper');
		docheight = boxWrapper[0] != undefined ? boxWrapper[0].offsetHeight : 0;
		if (!classie.hasClass(document.body, 'vmenu') && !classie.hasClass(document.body, 'menu-offcanvas')) init_overlay();
		if (!UNCODE.isMobile) {
			parallaxRows = document.querySelectorAll('.with-parallax > .row-background > .background-wrapper');
			parallaxCols = document.querySelectorAll('.with-parallax > .column-background > .background-wrapper');
		}
		footerScroller = document.querySelectorAll('.footer-scroll-top');
		if (footerScroller && footerScroller[0] != undefined) {
			if (transitionEvent) {
				footerScroller[0].addEventListener(transitionEvent, hideFooterScroll);
			}
		}
		Array.prototype.forEach.call(document.querySelectorAll('.row-inner'), function(el) {
			el.style.height = '';
			el.style.marginBottom = '';
		});
		setRowHeight(document.querySelectorAll('.page-wrapper .row-parent'));
	});
	/** On resize events **/
	window.addEventListener("resize", function() {
		docheight = (boxWrapper != undefined && boxWrapper[0] != undefined) ? boxWrapper[0].offsetHeight : 0;
		var oldWidth = wwidth;
		wwidth = window.innerWidth || document.documentElement.clientWidth;
		wheight = window.innerHeight || document.documentElement.clientHeight;
		if (isMobile && (oldWidth == wwidth)) return false;
		initBox();
		headerHeight('.header-wrapper');
		window.dispatchEvent(boxEvent);
		clearTimeout(resizeTimer);
		resizeTimer = setTimeout(function() {
			Array.prototype.forEach.call(document.querySelectorAll('.row-inner'), function(el) {
				el.style.height = '';
				el.style.marginBottom = '';
			});
			setRowHeight(document.querySelectorAll('.page-wrapper .row-parent'));
		}, 500);

		if (!isMobile) {
			setTimeout(function() {
				initVideoComponent(document.body, '.uncode-video-container.video, .uncode-video-container.self-video');
			}, 100);
		}
		if (wwidth > mediaQuery) {
			scrollFunction();
		}
		showHideScrollup(bodyTop);
	});

	/**
	 * On DOM ready
	 */
	window.addEventListener("load", function(){
		if (!UNCODE.isMobile) {
			if (wwidth > mediaQuery) {
				scrollFunction();
			}
			setTimeout(function() {
				window.dispatchEvent(UNCODE.boxEvent);
				Waypoint.refreshAll();
			}, 2000);
		}
		showHideScrollup(bodyTop);
		jQuery(window).trigger('resize');
	}, false);

	var UNCODE = {
		boxEvent: boxEvent,
		initBox: initBox,
		menuHeight: 0,
		fixMenuHeight: fixMenuHeight,
		initHeader: initHeader,
		initRow: initRow,
		setRowHeight: setRowHeight,
		switchColorsMenu: switchColorsMenu,
		isMobile: isMobile,
		scrolling: false,
		mediaQuery: mediaQuery,
		initVideoComponent: initVideoComponent
	};
	// transport
	if (typeof define === 'function' && define.amd) {
		// AMD
		define(UNCODE);
	} else {
		// browser global
		window.UNCODE = UNCODE;
	}

	UNCODE.adaptive = function() {
		var images = new Array(),
			getImages = document.querySelectorAll('.adaptive-async:not(.adaptive-fetching)');
		for (var i = 0; i < getImages.length; i++) {
			var imageObj = {},
				el = getImages[i];
			classie.addClass(el, 'adaptive-fetching');
			imageObj.unique = el.getAttribute('data-uniqueid');
			imageObj.url = el.getAttribute('data-guid');
			imageObj.path = el.getAttribute('data-path');
			imageObj.singlew = el.getAttribute('data-singlew');
			imageObj.singleh = el.getAttribute('data-singleh');
			imageObj.origwidth = el.getAttribute('data-width');
			imageObj.origheight = el.getAttribute('data-height');
			imageObj.crop = el.getAttribute('data-crop');
			imageObj.screen = window.uncodeScreen;
			imageObj.images = window.uncodeImages;
			images.push(imageObj);
		}
		var jsonString = JSON.stringify(images);
		var data = new Array();
		data['action'] = 'get_adaptive_async';
		data['images'] = jsonString;
		data['cache'] = 'false';

		if (images.length > 0) {
			var xmlhttp;
			if (window.XMLHttpRequest) {
				// code for IE7+, Firefox, Chrome, Opera, Safari
				xmlhttp = new XMLHttpRequest();
			} else {
				// code for IE6, IE5
				xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
			}
			xmlhttp.onreadystatechange = function() {
				if (xmlhttp.readyState == XMLHttpRequest.DONE ) {
					if(xmlhttp.status == 200){
						var images = JSON.parse(xmlhttp.responseText);
						for (var i = 0; i < images.length; i++) {
							var val = images[i],
								getImage = document.querySelectorAll('[data-uniqueid="'+val.unique+'"]');
							for (var j = 0; j < getImage.length; j++) {
								var attrScr = getImage[j].getAttribute('src'),
								replaceImg = new Image();
								replaceImg.source = attrScr;
								replaceImg.el = getImage[j];

								classie.removeClass(getImage[j], 'adaptive-async');
								classie.removeClass(getImage[j], 'adaptive-fetching');

								replaceImg.onload = function () {
									if (this.source !== null) {
										(this.el).src = this.src;
										// (this.el).removeAttribute('width');
										// (this.el).removeAttribute('height');
									} else {
										(this.el).style.backgroundImage = 'url("'+this.src+'")';
									}
									classie.addClass(this.el, 'async-done');
								}
								replaceImg.src = val.url;
							}
						}
					}
					else if(xmlhttp.status == 400) {
						console.log('There was an error 400')
					}
					else {
						console.log('something else other than 200 was returned')
					}
				}
			}
			//Serialize the data
	    var queryString = "",
	    	arrayLength = Object.keys(data).length,
	    	arrayCounter = 0;

	    for (var key in data) {
				queryString += key + "=" + data[key];
				if(arrayCounter < arrayLength - 1) {
					queryString += "&";
				}
				arrayCounter++;
			}

			xmlhttp.open("POST", SiteParameters.uncode_ajax, true);
			xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
	    xmlhttp.send(queryString);

		}
	};

})(window);