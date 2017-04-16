'use strict';

angular.module('myApp.signup', ['ngRoute', 'api', 'auth', 'session'])

.controller('SignupCtrl', ['$rootScope', '$scope', '$location', 'api', 'auth', 'session', function($rootScope, $scope, $location, api, auth, session) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
	
	$scope.register = function(){
        var username = document.getElementById("username").value;
        var email = document.getElementById("email").value;
        var password = document.getElementById("password").value;
        var repeatPassword = document.getElementById("repeatPassword").value;
        
        if(username === "" || email === "" || password === "" || repeatPassword === ""){
        	$('#verification').text("One or more fields are empty. Please fill them all.");
			document.getElementById("verification").style.color = "#FF3838";
        }else{
        	api.register.save({}, {username: username, email: email, password1: password, password2: repeatPassword}, 
        	function(successResponse){
        		var token = successResponse.key;
        		session.setAuthToken(token);
        		
				api.user.getCurrent({}, {}, 
				function(successResponse){					
					var user = successResponse;
					session.setCurrentUser(user);
					
					$location.path('#!/');
					
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