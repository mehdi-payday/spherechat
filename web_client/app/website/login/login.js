'use strict';

angular.module('myApp.login', ['auth', 'api'])

.controller('LoginCtrl', ['$rootScope', '$scope', '$location', 'auth', 'api', function($rootScope, $scope, $location, auth, api) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
	
	$scope.login = function(){
        var username = document.getElementById("username").value;
        var password = document.getElementById("password").value;
		if(username === "" || password === ""){
			$('#verification').text("username or password is incorrect");
			document.getElementById("verification").style.color = "#FF3838";
		}else{
			api.login.save({}, {username: username, password: password}, 
			function(successResponse){

			}, function(errorResponse){
				$('#verification').text("username or password is incorrect");
				document.getElementById("verification").style.color = "#FF3838";
			});
		}
	}
}]);