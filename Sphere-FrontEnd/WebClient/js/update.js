$(function() {

  // We can attach the `fileselect` event to all file inputs on the page
  $(document).on('change', ':file', function() {
    var input = $(this),
        numFiles = input.get(0).files ? input.get(0).files.length : 1,
        label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
    input.trigger('fileselect', [numFiles, label]);
  });

  // We can watch for our custom `fileselect` event like this
  $(document).ready( function() {
      $(':file').on('fileselect', function(event, numFiles, label) {

          var input = $(this).parents('.input-group').find(':text'),
              log = numFiles > 1 ? numFiles + ' files selected' : label;

          if( input.length ) {
              input.val(log);
          } else {
              if( log ) alert(log);
          }

      });
  });
  
});
// Regex
var pattern = /^[a-zA-Z0-9_.-]*$/;
var emailRegex = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
var mobileRegex = /^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$/;
var numberOnlyRegex = /^[0-9()-]*$/;
var noSpaceRegex = /^(\w+\s?)*\s*$/;
// End Regex

// Global Timeout
var timeout;
// End Global Timeout

// Form variables
var firstname = null;
var lastname = null;
var username = null;
var imagepath = null;
var email = null;
var mobilenumber = null;
var password = null;
var birthday = null;
var country = null;
var city = null;
var gender = null;
var terms = null;
// End variables

// execute/clear BS loaders for docs
$(function() {
        if (window.BS && window.BS.loader && window.BS.loader.length) {
            while (BS.loader.length) {
                (BS.loader.pop())()
            }
        }
    })
    // ========================================= First Name Verifications ================================================== //
$('#firstName').keyup(function(e) {
    while ($('#firstName').val().length < 2 && $('#firstName').val().length > 0) {
        $('#firstName').css("border", "1px solid #FF0000");
        $('#firstName').focus(function() {
            $('#firstName').css("border", "1px solid #66afe9");
        })
        $('#firstName').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;
    }
    if ($('#firstName').val().length >= 2 && noSpaceRegex.test()) {
        $('#firstName').css("border", "1px solid #40D127");
        $('#firstName').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        firstname = $('#firstName').val().replace(/\s\s+/g, ' ');
        document.getElementById("firstName").value = firstname;
    } else {
        $('#firstName').css("border", "1px solid #d4dbe0");
        $('#firstName').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }
});
// ========================================= End First Name Verifications ================================================== //

// ========================================= Last Name Verifications ================================================== //
$('#lastName').keyup(function(e) {
    while ($('#lastName').val().length < 2 && $('#lastName').val().length > 0) {
clear();
        $('#lastName').css("border", "1px solid #FF0000");
        $('#lastName').focus(function() {
            $('#lastName').css("border", "1px solid #66afe9");
        })
        $('#lastName').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;
    }
    if ($('#lastName').val().length >= 2 && noSpaceRegex.test()) {
        $('#lastName').css("border", "1px solid #40D127");
        $('#lastName').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        lastname = $('#lastName').val().replace(/\s\s+/g, ' ');
        document.getElementById("lastName").value = lastname;
    } else {
        $('#lastName').css("border", "1px solid #d4dbe0");
        $('#lastName').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }
});
// ========================================= End Last Name Verifications ================================================== //


// ========================================= Username Verifications ================================================== //
$('#username').keyup(function(e) {
    while ($('#username').val().length < 4 && $('#username').val().length > 0) {
        $('#username').css("border", "1px solid #FF0000");
        $('#username').focus(function() {
            $('#username').css("border", "1px solid #66afe9");
        })
        $('#username').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;
    }
    if ($('#username').val().length >= 4) {
        $('#username').css("border", "1px solid #40D127");
        $('#username').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        username = $('#username').val();
    } else {
        $('#username').css("border", "1px solid #d4dbe0");
        $('#username').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }
});
$('#username').keydown(function(e) {

    if (e.keyCode == 32) {
        e.preventDefault();
    }
    if (e.keyCode == 8 && $(this).val().length == 2) {
        document.getElementById("username").value = "";
    }
});

function usernameCheck(e) {
    var userinput = $('#username').val();
    while (userinput.charAt(0) === '@') {
        userinput = userinput.substr(1);
    }
    if (!pattern.test(userinput)) {
        document.getElementById("username").value = "@" + userinput.substr(0, userinput.length - 1);
        //e.preventDefault();
    }
}
$('#username').keypress(function(event) {
    if (timeout) {
        clearTimeout(timeout);
        timeout = null;
    }

    timeout = setTimeout(usernameCheck, 1)
});
$('#username').keypress(function(event) {
    var code = (event.keyCode ? event.keyCode : event.which);
    if ($(this).val().indexOf('@') === -1) {
        document.getElementById("username").value = "@" + $(this).val();
    } else {
        document.getElementById("username").value = $(this).val();
    }
});
// ========================================= End Username Verifications ================================================== //

// ========================================= Picture Verifications ================================================== //


$('#profilePicture').on('change', function() {
    while ($('#profilePicture').val().length == 0) {
        $('#file').css("border", "1px solid #FF0000");
        $('#file').focus(function() {
            $('#file').css("border", "1px solid #66afe9");
        })
        $('#file').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;

    }
    if ($('#profilePicture').val().length > 0) {
        $('#file').css("border", "1px solid #40D127");
        $('#file').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        imagepath = $('#profilePicture').val();
    } else {
        $('#file').css("border", "1px solid #d4dbe0");
        $('#file').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }

});

// ========================================= End Picture Verifications ================================================== //

// ========================================= Email Verifications ================================================== //
$('#email').keyup(function(e) {
    while (!emailRegex.test($('#email').val())) {
        $('#email').css("border", "1px solid #FF0000");
        $('#email').focus(function() {
            $('#email').css("border", "1px solid #66afe9");
        })
        $('#email').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;
    }

    if (emailRegex.test($('#email').val())) {
        $('#email').css("border", "1px solid #40D127");
        $('#email').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        email = $('#email').val();
    } else {
        $('#email').css("border", "1px solid #d4dbe0");
        $('#email').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }

});
$(function(){
  $('#email').bind('input', function(){
    $(this).val(function(_, v){
     return v.replace(/\s+/g, '');
    });
  });
});
// ========================================= End Email Verifications ================================================== //



// ========================================= Mobile Phone Verifications ================================================== //
$('#mobileNumber').keyup(function(e) {
    while (!mobileRegex.test($('#mobileNumber').val())) {
        $('#mobileNumber').css("border", "1px solid #FF0000");
        $('#mobileNumber').focus(function() {
            $('#mobileNumber').css("border", "1px solid #66afe9");
        })
        $('#mobileNumber').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;
    }

    if (mobileRegex.test($('#mobileNumber').val())) {
        $('#mobileNumber').css("border", "1px solid #40D127");
        $('#mobileNumber').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        mobilenumber = $('#mobileNumber').val();
    } else {
        $('#mobileNumber').css("border", "1px solid #d4dbe0");
        $('#mobileNumber').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }

});

$('#mobileNumber').keydown(function(e) {

    if (e.keyCode == 32) {
        e.preventDefault();
    }
    if (e.keyCode == 8 && $(this).val().length == 2) {
        document.getElementById("mobileNumber").value = "";
    }
});

function numberCheck(e) {
    var userinput = $('#mobileNumber').val();
    while (userinput.charAt(0) === '(') {
        userinput = userinput.substr(1);
    }
    if (!numberOnlyRegex.test(userinput)) {
        document.getElementById("mobileNumber").value = "(" + userinput.substr(0, userinput.length - 1);
        //e.preventDefault();
    }
}

function numberCheck2(e) {
    var userinput = $('#mobileNumber').val();
    if (!mobileRegex.test(userinput)) {

    }
    if (mobileRegex.test(userinput)) {
        $('#mobileNumber').css("border", "1px solid #40D127");
        $('#mobileNumber').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    } else {
        $('#email').css("border", "1px solid #d4dbe0");
        $('#email').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }
}
$('#mobileNumber').keypress(function(event) {
    if (timeout) {
        clearTimeout(timeout);
        timeout = null;
    }

    timeout = setTimeout(numberCheck, 1);
    if ($('#mobileNumber').val().length >= 13) {
        timeout = setTimeout(numberCheck2, 2);
    }
});

$('#mobileNumber').keypress(function(event) {

    var code = (event.keyCode ? event.keyCode : event.which);
    if ($(this).val().indexOf('(') === -1) {
        document.getElementById("mobileNumber").value = "(" + $(this).val();
    }

    if ($('#mobileNumber').val().length === 4) {
        document.getElementById("mobileNumber").value = $(this).val() + ")-";
    }
    if ($('#mobileNumber').val().length === 9) {
        document.getElementById("mobileNumber").value = $(this).val() + "-";
    } else {
        document.getElementById("mobileNumber").value = $(this).val();
    }
});

// ========================================= End Mobile Phone Verifications ================================================== //


// ========================================= Password Verifications ================================================== //
$('#password').keyup(function(e) {
    while ($('#password').val().length < 7 && $('#password').val().length > 0) {
        $('#password').css("border", "1px solid #FF0000");
        $('#password').focus(function() {
            $('#password').css("border", "1px solid #66afe9");
        })
        $('#password').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;
    }

    if ($('#password').val().length >= 7) {
        $('#password').css("border", "1px solid #40D127");
        $('#password').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    } else {
        $('#password').css("border", "1px solid #d4dbe0");
        $('#password').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }
});


$('#repeatPassword').keyup(function(e) {
    while ($('#repeatPassword').val().length < $('#password').val().length && $('#repeatPassword').val().length > 0) {
        $('#repeatPassword').css("border", "1px solid #FF0000");
        $('#repeatPassword').focus(function() {
            $('#repeatPassword').css("border", "1px solid #66afe9");
        })
        $('#repeatPassword').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;
    }
    if ($('#repeatPassword').val().length == $('#password').val().length) {
        $('#repeatPassword').css("border", "1px solid #40D127");
        $('#repeatPassword').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        password = $('#repeatPassword').val();
    } else {
        $('#repeatPassword').css("border", "1px solid #d4dbe0");
        $('#repeatPassword').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }
});
// ========================================= End Password Verifications ================================================== //


// ========================================= Birthday Verifications ================================================== //
$('#date').on('change', function(e) {
    var birthdate = new Date($('#date').val()).getFullYear();
    var actualDate = new Date().getFullYear();

    while ($('#date').val() == '' || actualDate - birthdate <= 10) {
        $('#date').css("border", "1px solid #FF0000");
        $('#date').focus(function() {
            $('#date').css("border", "1px solid #66afe9");
        })
        $('#date').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;
    }
    if (actualDate - birthdate > 10) {
        $('#date').css("border", "1px solid #40D127");
        $('#date').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        birthday = new Date($('#date').val());
    } else {
        $('#date').css("border", "1px solid #d4dbe0");
        $('#date').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }

});

// ========================================= End Birthday Verifications ================================================== //

// ========================================= Country Verifications ================================================== //

$('#country').on('change', function(e) {
    while ($('#country').prop('selectedIndex') == 0) {
        $('#country').css("border", "1px solid #FF0000");
        $('#country').focus(function() {
            $('#country').css("border", "1px solid #66afe9");
        })
        $('#country').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;
    }
    if ($('#country').prop('selectedIndex') > 0) {
        $('#country').css("border", "1px solid #40D127");
        $('#country').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        country = $('#country').val();
    } else {
        $('#country').css("border", "1px solid #d4dbe0");
        $('#country').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }
});
// ========================================= End Country Verifications ================================================== //

// ========================================= City Verifications ================================================== //  
$('#city').keyup(function(e) {
    while ($('#city').val().length < 2 && $('#city').val().length > 0) {
        $('#city').css("border", "1px solid #FF0000");
        $('#city').focus(function() {
            $('#city').css("border", "1px solid #66afe9");
        })
        $('#city').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;
    }
    if ($('#city').val().length >= 2) {
        $('#city').css("border", "1px solid #40D127");
        $('#city').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        city = $('#city').val();
    } else {
        $('#city').css("border", "1px solid #d4dbe0");
        $('#city').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }
});
// ========================================= End City Verifications ================================================== // 

// ========================================= Gender Verifications ================================================== // 
$('#gender').on('change', function(e) {
    while ($('#gender').prop('selectedIndex') == 0) {
        $('#gender').css("border", "1px solid #FF0000");
        $('#gender').focus(function() {
        $('#gender').css("border", "1px solid #66afe9");
        })
        $('#gender').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        return;
    }
    if ($('#gender').prop('selectedIndex') > 0) {
        $('#gender').css("border", "1px solid #40D127");
        $('#gender').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
        gender = $('#gender').val();
    } else {
        $('#gender').css("border", "1px solid #d4dbe0");
        $('#gender').css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, 0.075)");
    }

});
// ========================================= Gender Verifications ================================================== //

// ========================================= terms Verifications ================================================== // 


$('#terms').change(function() {

        alert("adsfjdbcx");

}); 
// ========================================= terms Verifications ================================================== //

function allVerifications(){

if(firstname != null && lastname != null && username != null && imagepath != null && email != null && mobilenumber != null && password != null && birthday != null && country != null && city != null && gender != null && $("#terms").is(':checked')){
    $('#createAccount').prop('disabled', false);
    console.log(firstname + "\n" + lastname + "\n" + username + "\n" + imagepath + "\n" + email + "\n" + mobilenumber + "\n" + password + "\n" + birthday + "\n" + country + "\n" + city + "\n" + gender);
  }
  else{
    return false;
    console.log("Some fields are still empty. Fill them up")
  }
}

console.log('\nHi there, fellow developer! Thanks for visiting.\nIf you’re an aspiring bootstrapper, startup-er,         ("`-’-/").___..--’’"`-._\nor business owner, make sure you sign up for my          `6_ 6  )   `-.  (     ).`-.__.‘)\nnewsletter!                                              (_Y_.)’  ._   )  `._ `. ``-..-’\n                                                       _..`--’_..-_/  /--’_.’ ,’\n— @xSash_                                           (il),-’‘  (li),’  ((!.-‘\n\n');