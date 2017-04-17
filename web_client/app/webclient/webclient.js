'use strict';

angular.module('myApp.webclient', ['session', 'auth'])

.controller('WebclientCtrl', ['$rootScope', '$scope', '$interval', 'session', 'auth', 'messaging', function($rootScope, $scope, $interval, session, auth, messaging) {
	var publicChannel = "public_channel";
	var privateChannel = 'private_channel';
	
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
	
	// Channels
	$scope.retrieveChannels = function(){
		messaging.getChannels().$promise.then(function(channels){
			$scope.userChannels = channels;
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
		
		$scope.retrieveMessages();
	}
	
	$scope.cancelChannelSelection = function(){
		delete $scope.channelSelected;
	}
	
	//Messages
	$scope.retrieveMessages = function(){
		if(!$scope.channelSelected){
			return;
		}
		messaging.getChannelMessages($scope.channelSelected).$promise.then(function(data){
			$scope.currentMessages = data.results;
			console.log(data);
		});
	}
	
	$scope.sendMessage = function(){
		var message = document.getElementById('comment').value;
		if(message === "" || !$scope.channelSelected){
			return;
		}
		messaging.sendChannelMessage($scope.channelSelected, message).$promise.then(function(){
			$scope.retrieveMessages();
		});
	}
	
	// Calculated values for DOM
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
	
	// Actions at init
	$scope.retrieveChannels();
	
	$interval(function(){
		$scope.retrieveChannels();
		$scope.retrieveMessages();
	}, 5000);
}]);