'use strict';

angular.module('myApp.webclient', ['session', 'auth'])

.controller('WebclientCtrl', ['$rootScope', '$scope', 'session', 'auth', 'messaging', function($rootScope, $scope, session, auth, messaging) {
	var publicChannel = "public_channel";
	var privateChannel = 'private_channel';
	
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
	
	$scope.retrieveChannels = function(){
		$scope.userChannels = messaging.getChannels();
	}
	
	$scope.createChannel = function(){
		var title = document.getElementById('publicLink').value;
		if(title === ''){
			alert('Enter a channel name');
			return;
		}
		
		var channel = {
			title: title,
			type: publicChannel,
			description: 'descriptionTest',
			members: []
		};
		messaging.createChannel(channel);
		$('body').removeClass('mode-panel');
	}
}]);