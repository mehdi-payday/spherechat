(function() {
  var integrationIcons = document.querySelectorAll(".icon-integration");

  var transforms = ["transform", "msTransform", "webkitTransform", "mozTransform", "oTransform"];
  var transformProperty = propertyName(transforms);

  function setInitialProperties() {
    for (var i = 0; i < integrationIcons.length; i++) {
      var integrationIcon = integrationIcons[i];

      setTranslate3DTransform(integrationIcon);
    }
    setTimeout(kickOffTransition, 100);
  }
  setInitialProperties();


  function kickOffTransition() {
    for (var i = 0; i < integrationIcons.length; i++) {
      var integrationIcon = integrationIcons[i];

      integrationIcon.addEventListener("transitionend", updatePosition, false);
      integrationIcon.addEventListener("webkitTransitionEnd", updatePosition, false);
      integrationIcon.addEventListener("mozTransitionEnd", updatePosition, false);
      integrationIcon.addEventListener("msTransitionEnd", updatePosition, false);
      integrationIcon.addEventListener("oTransitionEnd", updatePosition, false);

      setTranslate3DTransform(integrationIcon);
    }
  }

  function updatePosition(e) {
    var integrationIcon = e.currentTarget;

    if (e.propertyName.indexOf("transform") != -1) {
      setTranslate3DTransform(integrationIcon);
    }
  }

  function getRandomXPosition() {
    return Math.round(-15 + Math.random() * 60);
  }

  function getRandomYPosition() {
    return Math.round(-25 + Math.random() * 60);
  }

  function propertyName(properties) {
      for (var i = 0; i < properties.length; i++) {
          if (typeof document.body.style[properties[i]] != "undefined") {
              return properties[i];
          }
      }
      return null;
  }

  function setTranslate3DTransform(element) {
    element.style[transformProperty] = "translate3d(" + getRandomXPosition() + "px" + ", " + getRandomYPosition() + "px" + ", 0)";
  }

})();