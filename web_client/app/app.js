'use strict';

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
		
		$routeProvider
			.when('/', {
			    templateUrl: 'website/main/main.html',
			    controller: 'MainCtrl'
			})
			.when('/404', {
			    templateUrl: 'website/404/404.html',
			    controller: '404Ctrl'
			})
			.when('/502', {
			    templateUrl: 'website/502/502.html',
			    controller: '502Ctrl'
			})
			.when('/about', {
			    templateUrl: 'website/about/about.html',
			    controller: 'AboutCtrl'
			})
			.when('/comingsoon', {
			    templateUrl: 'website/comingsoon/comingsoon.html',
			    controller: 'ComingsoonCtrl'
			})
			.when('/downloads', {
			    templateUrl: 'website/downloads/downloads.html',
			    controller: 'DownloadsCtrl'
			})
			.when('/faq', {
			    templateUrl: 'website/faq/faq.html',
			    controller: 'FaqCtrl'
			})
			.when('/login', {
			    templateUrl: 'website/login/login.html',
			    controller: 'LoginCtrl'
			})
			.when('/resetpassword', {
			    templateUrl: 'website/resetpassword/resetpassword.html',
			    controller: 'ResetpasswordCtrl'
			})
			.when('/signup', {
			    templateUrl: 'website/signup/signup.html',
			    controller: 'SignupCtrl'
			})
			.otherwise({redirectTo: '/'});
	}])

	.directive('head', ['$rootScope','$compile', function($rootScope, $compile) {
		return {
		    restrict: 'E',
		    link: function(scope, elem){
		        var html = '<link rel="stylesheet" ng-repeat="(routeCtrl, cssUrl) in routeStyles" ng-href="{{cssUrl}}" />';
		        elem.append($compile(html)(scope));
		        scope.routeStyles = {};
		        $rootScope.$on('$routeChangeStart', function (e, next, current) {
		            if(current && current.$$route && current.$$route.css){
		                if(!angular.isArray(current.$$route.css)){
		                    current.$$route.css = [current.$$route.css];
		                }
		                angular.forEach(current.$$route.css, function(sheet){
		                    delete scope.routeStyles[sheet];
		                });
		            }
		            if(next && next.$$route && next.$$route.css){
		                if(!angular.isArray(next.$$route.css)){
		                    next.$$route.css = [next.$$route.css];
		                }
		                angular.forEach(next.$$route.css, function(sheet){
		                    scope.routeStyles[sheet] = sheet;
		                });
		            }
		        });
		    }
		};
	}])
	
    .run(function($rootScope) {
	    $rootScope.$on("$routeChangeStart", function(next, current) { 
	    	$rootScope.hideNavbar = false;
	    	$rootScope.hideFooter = false;
	    });
	});


angular
	.module('api', ['ngResource'])
    .factory('Api', ['$resource', function($resource){
    	var serverAddress = 'spherechat.tk:8080/api/';
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
    }]);