'use strict';

angular.module('myApp.signup', ['ngRoute', 'api', 'auth', 'session'])

.controller('SignupCtrl', ['$rootScope', '$scope', 'api', 'auth', 'session', function($rootScope, $scope, api, auth, session) {
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
        		
        		console.log(successResponse);
        		
        		var token = successResponse.key;
        		console.log(token);
        		
        		session.setAuthToken(token);
        		console.log(session.getAuthToken());
        		
				api.user.getCurrent({}, {}, 
				function(successResponse){
					var user = successResponse.data.user;
					session.setCurrentUser(user);
					
					console.log(auth.isLoggedIn())
				}, function(errorResponse){
					console.log(errorResponse);
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