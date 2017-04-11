$(function() {

  $('#image-container').on('dragover', function(e) {
    e.preventDefault();
    $(this).addClass('file-over');
    //$('svg path').show();
  });

  $('#image-container').on('dragleave', function(e) {
    e.preventDefault();
    e.stopPropagation();
    $(this).removeClass('file-over');
  });

  $('#image-container').on('drop', function(e) {
    e.preventDefault();
    e.stopPropagation();
    $(this).addClass('file-over').stop(true, true).css({
      background:'#fff'
    });
    $('.progress').toggleClass('complete');
    $('#image-holder').addClass('move');
  });

  var dropzone = document.getElementById("image-container");
  
  FileReaderJS.setupDrop(dropzone, {
    readAsDefault: "DataURL",
    on: {
      load: function(e, file) {
        var img = document.getElementById("image-holder");
        img.onload = function() {
          document.getElementById("image-holder").appendChild(img);
        };
        img.src = e.target.result;
      }
    }
  });

});