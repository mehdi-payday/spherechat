'use strict';

angular.module('myApp.login', ['auth', 'api', 'session'])

.controller('LoginCtrl', ['$rootScope', '$scope', '$location', 'auth', 'api', 'session', function($rootScope, $scope, $location, auth, api, session) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
	
	$scope.login = function(){
        var username = document.getElementById("username").value;
        var password = document.getElementById("password").value;
		if(username === "" || password === ""){
			$('#verification').text("Username or password is empty.");
			document.getElementById("verification").style.color = "#FF3838";
		}else{
			api.login.save({}, {username: username, password: password}, 
			function(successResponse){
				var token = successResponse.key;
        		session.setAuthToken(token);
        		
				api.user.getCurrent({}, {}, 
				function(successResponse){					
					var user = successResponse;
					session.setCurrentUser(user);
					
					$location.path('/webclient');
					
				}, function(errorResponse){
					$('#verification').text("An error occured while trying to login in to your account. Please try later.");
					document.getElementById("verification").style.color = "#FF3838";
				});
			}, function(errorResponse){
				var errors = "";
				for(var field in errorResponse.data){
					for(var error in errorResponse.data[field]){
						errors += field + ": " + errorResponse.data[field][error] + "\n\n"
					}
				}
				
				$('#verification').text(errors);
				document.getElementById("verification").style.color = "#FF3838";
			});
		}
	}
}]);