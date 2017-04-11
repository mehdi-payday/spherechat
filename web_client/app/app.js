'use strict';

var serverAddress = 'spherechat.tk:8080/api/';

angular
.module('myApp', [
  'ngRoute',
  'myApp.404',
  'myApp.502',
  'myApp.about',
  'myApp.comingsoon',
  'myApp.downloads',
  'myApp.faq',
  'myApp.login',
  'myApp.resetpassword',
  'myApp.signup',
  'myApp.main',
  'api'
])

.config(['$locationProvider', '$routeProvider', function($locationProvider, $routeProvider) {
  $locationProvider.hashPrefix('!');

  $routeProvider.otherwise({redirectTo: '/main'});
}]);


angular
	.module('api', ['ngResource'])
    .factory('Api', ['$resource', function($resource){
    	var api = {};
    	
    	api.login = function() {return $resource(serverAddress + 'auth/login');};
    	api.register = function() {return $resource(serverAddress + 'auth/registration');};
    	
    	api.friendship = function() {return $resource(serverAddress + 'api/friendship/friendship');};
    	api.friendrequest = function() {return $resource(serverAddress + 'api/friendship/friendrequest');};
    	
    	api.user = function() {return $resource(serverAddress + 'user', {}, {
    		'getCurrent': {
    			method: 'GET',
    			url: serverAddress + 'me'
    		},
    		'getOne': {
    			method: 'GET',
    			url: serverAddress + 'user/:id',
    			params: {id: '@id'}
    		}
    	});};
    	
    	api.channel = function() {return $resource(serverAddress + 'api/messaging/channel', {}, {
    		'getOne': {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/channel/:id',
    			params: {id: '@id'}
    		},
	    	'getMessages': {
				method: 'GET',
				url: serverAddress + 'api/messaging/channel/:id/message',
				params: {id: '@id'}
			},
    		'getOneMessage': {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/channel/:channelId/message/:messageId',
    			params: {discussionId: '@channelId', messageId: '@messageId'}
    		},
			'postMessage': {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/channel/:channelId/message/',
    			params: {discussionId: '@channelId'}
    		}
    	});};
    	
    	api.privateDiscussion = function() {return $resource(serverAddress + 'api/messaging/privatediscussion', {}, {
    		'getOne': {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/privatediscussion/:id',
    			params: {id: '@id'}
    		},
    		'getMessages': {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/privatediscussion/:id/message',
    			params: {id: '@id'}
    		},
    		'getOneMessage': {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/privatediscussion/:discussionId/message/:messageId',
    			params: {discussionId: '@discussionId', messageId: '@messageId'}
    		},
    		'postMessage': {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/privatediscussion/:discussionId/message/',
    			params: {discussionId: '@discussionId'}
    		}
    	});};
    	return api;
    }])
    .run(function($rootScope) {
	    $rootScope.$on("$routeChangeStart", function(next, current) { 
	    	$rootScope.hideNavbar = false;
	    	$rootScope.hideFooter = false;
	    });
	});