      function textAreaAdjust(o) {
        var inp = $("#comment");
        o.style.height = "1px";
        o.style.height = (o.scrollHeight)+"px";
        if(inp.val().lenght <= 0){
          o.height = "55px";
        }
      }