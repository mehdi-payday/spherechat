(function() {
  var toggleElement;

  toggleElement = function($el, type) {
    if (type != null) {
      if (type === 'open') {
        $el.addClass('panel-element-open');
        $el.siblings('.panel-element').removeClass('panel-element-open');
      } else if (type === 'close') {
        $el.removeClass('panel-element-open');
      }
    } else {
      if ($el.hasClass('panel-element-open')) {
        toggleElement($el, 'close');
      } else {
        toggleElement($el, 'open');
      }
    }
    return null;
  };

  $(document).ready(function() {
    var hammertime;
    $('.btn').click(function() {
      var $parent;
      $parent = $(this).parents('.panel-element');
      if ($(this).hasClass('btn-heart')) {
        if ($parent.hasClass('panel-element-hearted')) {
          return $parent.removeClass('panel-element-hearted');
        } else {
          $parent.addClass('panel-element-hearted');
          return toggleElement($parent, 'close');
        }
      } else if ($(this).hasClass('btn-hide')) {
        toggleElement($parent, 'close');
        return $parent.delay(200).fadeOut(300);
      } else if ($(this).hasClass('btn-more')) {
        if (!hammertime) {
          return toggleElement($parent);
        }
      }
    });
    if ($(window).width() < 800) {
      hammertime = $('.panel-element .element-content').hammer();
      return hammertime.on('swipeleft swiperight tap', function(e) {
        var $parent;
        $parent = $(e.currentTarget).parent();
        if (e.type === 'tap') {
          return toggleElement($parent);
        } else if (e.type === 'swipeleft') {
          if (!$parent.hasClass('panel-element-open')) {
            return toggleElement($parent, 'open');
          }
        } else {
          if ($parent.hasClass('panel-element-open')) {
            return toggleElement($parent, 'close');
          }
        }
      });
    }
  });

}).call(this);