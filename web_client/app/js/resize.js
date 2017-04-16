      function textAreaAdjust(o) {
        var inp = $("#text2");
        o.style.height = "1px";
        o.style.height = (o.scrollHeight)+"px";
        if(inp.val().lenght <= 0){
          o.height = "55px";
        }
      }