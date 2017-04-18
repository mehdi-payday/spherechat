(function() {
  var toggleElement;

  toggleElement = function($el, type) {
    if (type != null) {
      if (type === 'open') {
        $el.addClass('user-element-open');
        $el.siblings('.user-element').removeClass('user-element-open');
      } else if (type === 'close') {
        $el.removeClass('user-element-open');
      }
    } else {
      if ($el.hasClass('user-element-open')) {
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
      $parent = $(this).parents('.user-element');
      if ($(this).hasClass('btn-heart')) {
        if ($parent.hasClass('user-element-hearted')) {
          return $parent.removeClass('user-element-hearted');
        } else {
          $parent.addClass('user-element-hearted');
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
      hammertime = $('.user-element .element-content').hammer();
      return hammertime.on('swipeleft swiperight tap', function(e) {
        var $parent;
        $parent = $(e.currentTarget).parent();
        if (e.type === 'tap') {
          return toggleElement($parent);
        } else if (e.type === 'swipeleft') {
          if (!$parent.hasClass('user-element-open')) {
            return toggleElement($parent, 'open');
          }
        } else {
          if ($parent.hasClass('user-element-open')) {
            return toggleElement($parent, 'close');
          }
        }
      });
    }
  });

}).call(this);