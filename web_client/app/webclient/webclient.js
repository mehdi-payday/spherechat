'use strict';

angular.module('myApp.webclient', ['session', 'auth'])

.controller('WebclientCtrl', ['$rootScope', '$scope', 'session', 'auth', 'messaging', function($rootScope, $scope, session, auth, messaging) {
	var publicChannel = "public_channel";
	var privateChannel = 'private_channel';
	
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
	
	$scope.retrieveChannels = function(){
		messaging.getChannels().$promise.then(function(channels){
			$scope.userChannels = channels;
		});
	}
	
	$scope.getUnseenMessagesCount = function(membershipArray){
		membershipArray.forEach(function(value){
			if(value.user === session.getCurrentUser().id){
				return value.unchecked_count;
			}
		});
	}
	
	$scope.getLastSeenDateDelta = function(membershipArray){
		membershipArray.forEach(function(value){
			if(value.user === session.getCurrentUser().id){
				return parseInt((new Date(value.last_seen_date).getTime() - new Date().getTime())/(24*3600*1000));
			}
		});
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
		messaging.createChannel(channel).$promise.then(function(){
			$scope.retrieveChannels();
		});
		$('body').removeClass('mode-panel');
	}
	
	$scope.setChannelSelected = function(channelId){
		$scope.channelSelected = channelId;
	}
	
	$scope.cancelChannelSelection = function(){
		delete $scope.channelSelected;
	}
	
	$scope.sendMessage = function(){
		
	}
	
	$scope.retrieveChannels();
}]);