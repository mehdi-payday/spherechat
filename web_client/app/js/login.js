      $('#sign').click(function(e) { 
      e.preventDefault(); 
      e.stopPropagation(); 
      window.location.href = $(e.currentTarget).data().href; 
      }); 
    $('.input-checkbox').on('click', function(){
        $(this).toggleClass('checked');
    });