'use strict';

angular.module('myApp.signup', ['ngRoute'])

.controller('SignupCtrl', ['$rootScope', function($rootScope) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
	
	$scope.register = function(){
		var name = document.getElementById("name").value;
        var username = document.getElementById("username").value;
        var email = document.getElementById("email").value;
        var password = document.getElementById("password").value;
        var repeatPassword = document.getElementById("repeatPassword").value;
	}
}]);