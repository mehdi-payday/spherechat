// left: 37, up: 38, right: 39, down: 40,
// spacebar: 32, pageup: 33, pagedown: 34, end: 35, home: 36
var isMobile = false;
var email = null;
var message = null;
var numclick = 0;
var timerStart = Date.now();
var keys = {
    37: 1,
    38: 1,
    39: 1,
    40: 1
};

function preventDefault(e) {
    e = e || window.event;
    if (e.preventDefault)
        e.preventDefault();
    e.returnValue = false;
}

function preventDefaultForScrollKeys(e) {
    if (keys[e.keyCode]) {
        preventDefault(e);
        return false;
    }
}

function disableScroll() {
    if (window.addEventListener) // older FF
        window.addEventListener('DOMMouseScroll', preventDefault, false);
    window.onwheel = preventDefault; // modern standard
    window.onmousewheel = document.onmousewheel = preventDefault; // older browsers, IE
    window.ontouchmove = preventDefault; // mobile
    document.onkeydown = preventDefaultForScrollKeys;
}

function enableScroll() {
    var Enabled = false;
    if (window.removeEventListener)
        window.removeEventListener('DOMMouseScroll', preventDefault, false);
    window.onmousewheel = document.onmousewheel = null;
    window.onwheel = null;
    window.ontouchmove = null;
    document.onkeydown = null;
}
$(document).ready(function() {
    var ready = "Document Loaded";
    console.log(ready);
    window.onbeforeunload = function(){ 
    // Hide scrollbar here
    }
})
function scrollTopFunc(){
    window.scrollTo(0,0);
}
$(window).load(function() {

    var load = Date.now() - timerStart
    // makes sure the whole site is loadeds
    console.log("Time until everything loaded: ", load);
    var loadTime = Date.now() - load + 1000;
    var loadTime2 = load + 6000;

    disableScroll();
    console.log("NewsLetter time : ", loadTime2);
    if(load > 10000){
        setTimeout(function(){
         beginNewsletterForm();
                  }, (load - (loadTime2 - 3000)));
    }
    else{
        setTimeout(function(){
         beginNewsletterForm();
                  }, loadTime2);
    }
    $('#status').delay(5500).fadeOut(); // will first fade out the loading animation
    $('#preloader').delay(5500).fadeOut('slow'); // will fade out the white DIV that covers the website.
    //$('#masthead').delay(6500).fadeIn('slow'); // Will need this later for new mobile navbar from MTLord Website

    // Hide() overflow 
    setTimeout(enableScroll(), loadTime);
})

// =========================================================== NOTES ====================================================================== \\
// 1. make new page for mobile app                                              ===========================================================
// 2. make new button under mobile section                                      |Done
// 3. make scroll disappear on load                                             |Done
// 4. put partial register form next to intro text                              |
// 5. make application app logo with application logo maker                     |Done
// 6. fix old navbar and implement for mobile views                             |Done
// 7. sync newletter with preloader                                             |Done
// 8. make register page {(priority)}                                           |Done
// 9. fix ugly css and paths in feed.html and login.html                        |
// 10. fix general paths                                                        |
// 11. push fixes                                                               |
// 12. do whole back-end in record time {(priority)}                            |
// 13. clear unused files (css & js)                                            |
// 14. compare login.html css with feed.html css                                |
// 15. clean css for login and feed                                             |
// 16. remove unused images                                                     |Done
// 17. optimize website (loading time and animation performance)                |
// 18. sync preloader and device load time correctly                            |Done
// 19. add buttons and fields to post (likes and reply field)                   |
// 20. make user profile page                                                   |
// 21. create views, controllers and models {take exemple from picture}         |
// 22. sync everything together                                                 |
// 23. testing and implementations                                              |
// 24. fix bugs                                                                 |
// 25. try to make application crash                                            |
// 26. deploy whole web app to server                                           |
// 27. make mobile app (if i have the time)                                     |
// 28. presentation                                                             |===============================================================
// =========================================================== END NOTES =================================================================== \\

function validate(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function validateEmail() {
    $("#result").text(""); // email field by id (to change)
    email = $("#emailInputField").val();
    if (validate(email) && email.length > 6 && email != null) {
        document.getElementById("emailInputField").style.borderColor = "#40D127";
        return true;
    } else {
        // Error code here    
        //return false;
    }

}
function mobileDeviceAuth(){

    // device detection
    if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent) ||
        /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) isMobile = true;

    if (isMobile === true) {
        console.log("Mobile : " + isMobile);
    } else {
        isMobile = false;
        console.log("Mobile : " + isMobile);
    }
}

$(document).on('click', 'a[href^="#"]', function(e) {
    // target element id
    var id = $(this).attr('href');

    // target element
    var $id = $(id);
    if ($id.length === 0) {
        return;
    }

    // prevent standard hash navigation (avoid blinking in IE)
    e.preventDefault();

    // top position relative to the document
    var pos = $(id).offset().top - 120;

    // animated top scrolling
    $('html, body').animate({
        scrollTop: pos
    }, 1500);
});


$(window).scroll(function (event) {
    var scroll = $(window).scrollTop();
    if(scroll == 0){
      $('#scrollTop').fadeOut(500);
      console.log("init");
    }
});

$('#learn-mobile').click(function(e) { 
      e.preventDefault(); 
      e.stopPropagation(); 
      window.location.href = $(e.currentTarget).data().href; 
}); 

// ===== NewsLetter Functions ===== \\
jQuery(function news($) {
          var check_cookie = $.cookie('newsletter_popup');
          if(window.location!=window.parent.location){
              jQuery('#newsletter_popup').remove();
          }else{
              if(check_cookie == null || check_cookie == 'shown') {

              }
              $('#newsletter_popup_dont_show_again').on('change', function(){
                  if($(this).length){        
                      var check_cookie = $.cookie('newsletter_popup');
                      if(check_cookie == null || check_cookie == 'shown') {
                          $.cookie('newsletter_popup','dontshowitagain');            
                      }
                      else
                      {
                          $.cookie('newsletter_popup','shown');
                          beginNewsletterForm();
                      }
                  } else {
                      $.cookie('newsletter_popup','shown');
                  }
              });
          }
      });
      
      function beginNewsletterForm() {
      jQuery.fancybox({
          'padding': '0px',
          'autoScale': true,
          'transitionIn': 'fade',
          'transitionOut': 'fade',
          'type': 'inline',
          'href': '#newsletter_popup',
          'onComplete': function() {
              $.cookie('newsletter_popup', 'shown');
          },
          'tpl': { 
              closeBtn: '<a title="Close" class="fancybox-item fancybox-close fancybox-newsletter-close" href="javascript:;"></a>' 
          },
          'helpers': {
              overlay: {
                  locked: false
              }
          }
      });
      jQuery('#newsletter_popup').trigger('click');
  }
