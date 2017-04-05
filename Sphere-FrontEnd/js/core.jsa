/* -----------------------------------------------------------------------------

File:           JS Core
Version:        1.0
Last change:    25/02/16 
Author:         Suelo 

-------------------------------------------------------------------------------- */

"use strict";

var $body = $('body'),
    $pageLoader = $('#page-loader'),
    trueMobile;

var Core = {
    init: function() {
        this.Basic.init();
    },
    Basic: {
        init: function() { 
            this.mobileDetector();
            this.backgrounds();
            this.buttons();
            this.parallax(); 
            this.product();  
        },
        animations: function() {
            // Animation - appear 
            $('.animated').appear(function() {
                $(this).each(function(){ 
                    var $target =  $(this);
                    var delay = $(this).data('animation-delay');
                    setTimeout(function() {
                        $target.addClass($target.data('animation')).addClass('visible')
                    }, delay);
                });
            });
        },
        backgrounds: function() {
            // Image
            $('.bg-image, .post.single .post-image').each(function(){
                var src = $(this).children('img').attr('src');
                $(this).css('background-image','url('+src+')').children('img').hide();
            });
            
            //Video 
            var $bgVideo = $('.bg-video');
            if($bgVideo) {
                $bgVideo.YTPlayer();
            }
            if($(window).width() < 1200 && $bgVideo) {
                $bgVideo.prev('.bg-video-placeholder').show();
                $bgVideo.remove()
            }
        },
        buttons: function() {
            $('.btn:not(.btn-submit)').each(function(){
                var html = $(this).html();
                $(this).html('<span>'+html+'</span>');
            });
        },
        parallax: function() {
            // Skroll
            if(!trueMobile){
                skrollr.init({
                    forceHeight: false
                });
            }
        },
        product: function() {
            
            // Product Feature
            $('.product-container .product-feature','#content').each(function(){
                var x = $(this).data('x');
                var y = $(this).data('y');
                $(this).css({
                    'top': y,
                    'left': x
                });
            });

        },
        mobileDetector: function () {
            var isMobile = {
                Android: function() {
                    return navigator.userAgent.match(/Android/i);
                },
                BlackBerry: function() {
                    return navigator.userAgent.match(/BlackBerry/i);
                },
                iOS: function() {
                    return navigator.userAgent.match(/iPhone|iPad|iPod/i);
                },
                Opera: function() {
                    return navigator.userAgent.match(/Opera Mini/i);
                },
                Windows: function() {
                    return navigator.userAgent.match(/IEMobile/i);
                },
                any: function() {
                    return isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows();
                }
            };

            trueMobile = isMobile.any();
        }
    }
};

$(document).ready(function (){
    Core.init();
});

$(window).on('load', function(){
    $body.addClass('loaded');
    if($pageLoader.length != 0) {
        $pageLoader.fadeOut(600, function(){
            Core.Basic.animations();
        });
    } else {
        Core.Basic.animations();
    }
});