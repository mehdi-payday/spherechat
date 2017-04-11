/* ===================================================================
 * Infinity - Main JS
 *
 * ------------------------------------------------------------------- */ 

(function($) {

	"use strict";	

	var cfg = {		
		defAnimation   : "fadeInUp",    // default css animation		
		scrollDuration : 800,           // smoothscroll duration
	},
	
	$WIN = $(window);
	

   // Add the User Agent to the <html>
   // will be used for IE10 detection (Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0))
	var doc = document.documentElement;
	doc.setAttribute('data-useragent', navigator.userAgent);

	
	/* Preloader 
	 * -------------------------------------------------- */
	var ssPreloader = function() {

		$WIN.on('load', function() {	

			// force page scroll position to top at page refresh
			$('html, body').animate({ scrollTop: 0 }, 'normal');

	      // will first fade out the loading animation 
	    	$("#loader").fadeOut("slow", function(){

	        // will fade out the whole DIV that covers the website.
	        $("#preloader").delay(300).fadeOut("slow");

	      }); 
	  	});
	};

  
  /* Initialize
	* ------------------------------------------------------ */
	(function ssInit() {

		ssPreloader();

	})();
 

})(jQuery);