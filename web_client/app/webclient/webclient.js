'use strict';

angular.module('myApp.webclient', ['session', 'auth'])

.controller('WebclientCtrl', ['$rootScope', '$scope', '$interval', 'session', 'auth', 'messaging', function($rootScope, $scope, $interval, session, auth, messaging) {
	var publicChannel = "public_channel";
	var privateChannel = 'private_channel';
	
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
	
	// Membership
	$scope.getMembershipArrayFromUserChannels = function(channelId){
		var result;
		Array.from($scope.userChannels.results).forEach(function(channel){
			if(channel.id === channelId){
				result = channel.memberships;
			}
		});
		return result;
	}
	
	// Users
	$scope.getUser = function(id){
		messaging.getUser(id).$promise.then(function(value){
			return value;
		});
	}
	
	$scope.setChannelUsers = function(channelId){
		$scope.channelSelectedUsers = {};
		var memberships = $scope.getMembershipArrayFromUserChannels(channelId);
		memberships.forEach(function(membership){
			messaging.getUser(membership.user).$promise.then(function(user){
				$scope.channelSelectedUsers[user.id] = user;
			});
		});
	}
	
	// Channels
	$scope.retrieveChannels = function(){
		messaging.getChannels().$promise.then(function(channels){
			$scope.userChannels = channels;
			$scope.glocalCountUnseen = $scope.getUnseenMessagesCountGlobal();
		});
	}
	
	$scope.createChannel = function(){
		var title = document.getElementById('publicLink').value;
		var description = document.getElementById('publicLinkD').value;
		if(title === '' || description === ''){
			alert('Enter a channel name and description');
			return;
		}
		
		var channel = {
			title: title,
			type: publicChannel,
			description: description,
			members: []
		};
		messaging.createChannel(channel).$promise.then(function(){
			$scope.retrieveChannels();
		});
		$('body').removeClass('mode-panel');
	}
	
	$scope.sendHeartbeat = function(){
		messaging.sendChannelHeartbeat($scope.channelSelected);
	}
	
	$scope.setChannelSelected = function(channelId){
		$scope.channelSelected = channelId;
		$scope.setChannelUsers($scope.channelSelected);
		$scope.retrieveMessages();
		
		messaging.sendChannelAllRead($scope.channelSelected);
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
		document.getElementById('comment').value = "";
	}
	
	// Calculated values for DOM
	$scope.getUnseenMessagesCount = function(membershipArray){
		var count = 0;
		membershipArray.forEach(function(value){
			if(value.user === session.getCurrentUser().id){
				count = value.unchecked_count;
			}
		});
		return count;
	}
	
	$scope.getUnseenMessagesCountGlobal = function(){
		var count = 0;
		Array.from($scope.userChannels.results).forEach(function(channel){
			channel.memberships.forEach(function(membership){
				if(membership.user === session.getCurrentUser().id){
					count += membership.unchecked_count;
				}
			});
		});
		return count;
	}
	
	$scope.getLastSeenDateDelta = function(membershipArray){
		var delta;
		membershipArray.forEach(function(value){
			if(value.user === session.getCurrentUser().id){
				delta = parseInt((new Date(value.last_seen_date).getTime() - new Date().getTime())/(24*3600*1000));
			}
		});
		return delta;
	}
	
	// Actions at init
	$scope.retrieveChannels();
	
	$interval(function(){
		$scope.retrieveChannels();
		$scope.retrieveMessages();
		$scope.glocalCountUnseen = $scope.getUnseenMessagesCountGlobal();
	}, 5000);
}]);