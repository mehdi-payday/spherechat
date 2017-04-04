      $('#sign').click(function(e) { 
      e.preventDefault(); 
      e.stopPropagation(); 
      window.location.href = $(e.currentTarget).data().href; 
      }); 

    function log(){
      var username = document.getElementById("username").value;
      var password = document.getElementById("password").value;
        console.log(username + " || " + password);
        if(username == null || username == "" || password == null || password == ""){
           console.log("true");
           $('#verification').text("username or password is incorrect");
           document.getElementById("verification").style.color = "#FF3838";

        }
        else{
          console.log("false");
          $('#verification').text("");
        }

    }
    $('.input-checkbox').on('click', function(){
        $(this).toggleClass('checked');
    });