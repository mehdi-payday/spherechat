/*
* Theme Name: Oppo
* Author: Okathemes
* Version: 1.0.0

/* Table of Content
*****************************************************
   
    01. Scroll Events
    02. Navigation
    03. Smooth Scroll to Inner Links
    04. Revolution Slider
	05. Slick Carousel
    06. Preloader
    07. Pricing Table
    08. Wow Animation
    09. Menu For Xs Mobile Screens
	10. Magnific Popup
*/

// /* ====== ON DOCUMENT READY START ====== */ 

$(document).ready(function() {

/*****************************************************
*** Scroll Events ***
*****************************************************/

	$(window).scroll(function(){

		var wScroll = $(this).scrollTop();

		// Activate menu
		if (wScroll > 20) {
			$('#main-nav').addClass('active');
			$('#slide_out_menu').addClass('scrolled');
		}
		else {
			$('#main-nav').removeClass('active');
			$('#slide_out_menu').removeClass('scrolled');
		};


		//Scroll Effects

	});

/*****************************************************
*** Navigation ***
*****************************************************/

	$('#navigation').on('click', function(e){
		e.preventDefault();
		$(this).addClass('open');
		$('#slide_out_menu').toggleClass('open');

		if ($('#slide_out_menu').hasClass('open')) {
			$('.menu-close').on('click', function(e){
				e.preventDefault();
				$('#slide_out_menu').removeClass('open');
			})
		}
	});

/*****************************************************
*** Smooth Scroll to Inner Links ***
*****************************************************/
	
	$('.top-link').smoothScroll({
		offset: -59,
		speed: 800
	});
	
/*****************************************************
*** Revolution Slider ***
*****************************************************/

    jQuery('.banner-full-height').revolution({
        delay: 9000,
        startwidth: 1170,
        startheight: 920,
        hideThumbs: 100,
        fullWidth: "on"
    });
	
/*****************************************************
*** Slick Carousel ***
*****************************************************/

	$('.center').slick({
        centerMode: true,
        infinite: true,
        centerPadding: '60px',
        slidesToShow: 5,
        speed: 500,
        responsive: [{
            breakpoint: 768,
            settings: {
                arrows: true,
                centerMode: true,
                centerPadding: '40px',
                slidesToShow: 5
            }
        }, {
            breakpoint: 480,
            settings: {
                arrows: true,
                centerMode: true,
                centerPadding: '40px',
                slidesToShow: 1
            }
        }]
    });

/*****************************************************
*** Preloader ***
*****************************************************/
   
        $('.spinner').fadeOut(); // will first fade out the loading animation
        $('.loader').delay(350).fadeOut('slow'); // will fade out the white DIV that covers the website.
        $('body').delay(350).css({'overflow': 'visible'
        });
    

/*****************************************************
*** Pricing Table ***
*****************************************************/

	var personal_price_table = $('#price_tables').find('.personal');
	var company_price_table = $('#price_tables').find('.company');


	$('.switch-toggles').find('.personal').addClass('active');
	$('#price_tables').find('.personal').addClass('active');

	$('.switch-toggles').find('.personal').on('click', function(){
		$(this).addClass('active');
		$(this).closest('.switch-toggles').removeClass('active');
		$(this).siblings().removeClass('active');
		personal_price_table.addClass('active');
		company_price_table.removeClass('active');
	});

	$('.switch-toggles').find('.company').on('click', function(){
		$(this).addClass('active');
		$(this).closest('.switch-toggles').addClass('active');
		$(this).siblings().removeClass('active');
		company_price_table.addClass('active');
		personal_price_table.removeClass('active');			
	});


/*****************************************************
*** Wow Animation ***
*****************************************************/

    wow = new WOW(
      {
      boxClass:     'wow',      // default
      animateClass: 'animated', // default
      offset:       0,          // default
      mobile:       true,       // default
      live:         true        // default
    }
    )
    wow.init();

/*****************************************************
*** Menu For Xs Mobile Screens ***
*****************************************************/

    if ($(window).height() < 450) {
    	$('#slide_out_menu').addClass('xs-screen');
    }

    $(window).on('resize', function(){
	    if ($(window).height() < 450) {
	    	$('#slide_out_menu').addClass('xs-screen');
	    } else{
	    	$('#slide_out_menu').removeClass('xs-screen');
	    }
    });

/*****************************************************
*** Magnific Popup ***
*****************************************************/
 
    $(".lightbox").magnificPopup();

});