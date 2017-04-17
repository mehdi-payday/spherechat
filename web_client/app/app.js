'use strict';

angular
	.module('myApp', [
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
		'myApp.webclient',
		'ui.router',
		'betsol.uiRouterStyles',
		'ngAnimate',
		'ngStorage',
		'api',
		'auth',
		'session',
		'messagingService'
	])

	.config(['$locationProvider', '$stateProvider', '$urlRouterProvider', '$resourceProvider', function($locationProvider, $stateProvider, $urlRouterProvider, $resourceProvider) {
		$locationProvider.hashPrefix('!');
		$resourceProvider.defaults.stripTrailingSlashes = false;
		
		$urlRouterProvider.otherwise('/');
		
		$stateProvider
			.state('websiteRoot', {
		        abstract: true,
		        template: '<ui-view/>',
		        data: {
		        	css: ['css/animation-angular.css',
		        	      'https://fonts.googleapis.com/css?family=Montserrat:100,300,400,600,700,800,900',
						  'http://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800',
						  'https://fonts.googleapis.com/css?family=Open+Sans:100,200,300,400,600,700&amp;subset=latin,latin-ext',
						  'http://fonts.googleapis.com/css?family=Varela+Round%3A400%7CHind%3A700%2C300%7CMontserrat%3A700%7CPlayfair+Display%3A400&#038;subset=latin&#038;ver=1490464751',
						  'lib/font-awesome-4.6.3/css/font-awesome.css',
						  'lib/font-awesome-4.6.3/css/font-awesome.min.css',
						  'css/animate.css',
						  'css/preloader.css',
						  'css/uncss.css',
						  'css/bootstrap.min.css',
						  'css/main.css',
						  'css/style.css',
					  	  'css/style2.css',
						  'css/testimony.css',
						  'css/common.min.css',
						  'css/home.min.css',
						  'css/apps.min.css',
						  'css/intro.css',
						  'css/social.css',
						  'css/steps.css',
						  'css/integrations.css']
		        }
		    })
			.state('main', {
				url: '/',
				parent: 'websiteRoot',
			    templateUrl: 'website/main/main.html',
			    controller: 'MainCtrl',
			    data: {
			    	css: ''
			    }
			})
			.state('404', {
				url: '/404',
				parent: 'websiteRoot',
				templateUrl: 'website/404/404.html',
			    controller: '404Ctrl',
			    data: {
			    	css: ''
			    }
			})
			.state('502', {
				url: '/502',
				parent: 'websiteRoot',
				templateUrl: 'website/502/502.html',
			    controller: '502Ctrl',
			    data: {
			    	css: ''
			    }
			})
			.state('about', {
				url: '/about',
				parent: 'websiteRoot',
				templateUrl: 'website/about/about.html',
			    controller: 'AboutCtrl',
			    data: {
			    	css: ''
			    }
			})
			.state('comingsoon', {
				url: '/comingsoon',
				parent: 'websiteRoot',
				templateUrl: 'website/comingsoon/comingsoon.html',
			    controller: 'ComingsoonCtrl',
			    data: {
			    	css: ['http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,400,600,700,300',
			          'http://fonts.googleapis.com/css?family=Exo+2:400,100,100italic,200,200italic,300,300italic,400italic,500,500italic,700,700italic,600,600italic',
			          'http://fonts.googleapis.com/css?family=Josefin+Sans:100,300,400,600']
			    }
			})
			.state('downloads', {
				url: '/downloads',
				parent: 'websiteRoot',
				templateUrl: 'website/downloads/downloads.html',
			    controller: 'DownloadsCtrl',
			    data: {
			    	css: ['css/downloads.css']
			    }
			})
			.state('faq', {
				url: '/faq',
				parent: 'websiteRoot',
				templateUrl: 'website/faq/faq.html',
			    controller: 'FaqCtrl',
			    data: {
			    	css: ['css/init.css']
			    }
			})
			.state('login', {
				url: '/login',
				parent: 'websiteRoot',
				templateUrl: 'website/login/login.html',
			    controller: 'LoginCtrl',
			    redirectIfLoggedIn: true,
			    data: {
			    	css: ['http://fonts.googleapis.com/css?family=Open+Sans:400,300,600',
			          'css/login.css',
			          'css/particules.css',
			          'css/checkbox.css']
			    }
			})
			.state('resetpassword', {
				url: '/resetpassword',
				parent: 'websiteRoot',
				templateUrl: 'website/resetpassword/resetpassword.html',
			    controller: 'ResetpasswordCtrl',
			    data: {
			    	css: ['http://fonts.googleapis.com/css?family=Open+Sans:400,300,600',
			          'css/resetpassword.css',
			          'css/particules.css']
			    }
			})
			.state('signup', {
				url: '/signup',
				parent: 'websiteRoot',
				templateUrl: 'website/signup/signup.html',
			    controller: 'SignupCtrl',
			    redirectIfLoggedIn: true,
			    data: {
			    	css: ['http://fonts.googleapis.com/css?family=Open+Sans:400,300,600',
			          'css/confetti.css',
			          'css/signup.css',
			          'css/checkbox.css']
			    }
			})
			.state('webclientRoot', {
		        abstract: true,
		        template: '<ui-view/>',
		        data: {
		        	css: ['https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/css/bootstrap.min.css',
				    	  'css/update.css',
				    	  'css/checkbox.css',
				    	  'css/chatheader.css',
				    	  'https://cdnjs.cloudflare.com/ajax/libs/Primer/3.0.1/css/primer.css',
				          'https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css',
				          'css/preloader-client.css',
				          'css/front-prof.css',
				          'css/sidemenu.css',
				          'css/intercom.css',
				          'css/chat.css',
				          'css/main-client.css',
				          'css/test2.css',
				          'css/gear.css',
				          'css/anime.css',
				          'css/style2-client.css',
				          'css/image.css',
				          'lib/font-awesome-4.6.3/css/font-awesome.css',
				          'lib/octicons/octicons.min.css',
				          'css/animate.css',
				          'css/confetti.css']
		        }
			})
			.state('webclient', {
				url: '/webclient',
				parent: 'webclientRoot',
				templateUrl: 'webclient/webclient.html',
			    controller: 'WebclientCtrl',
			    unauthorized: true,
			    data: {
			    	css: ''
			    }
			});
	}])
	
    .run(function($rootScope, $location, session, auth) {
    	$rootScope.session = session;
    	$rootScope.auth = auth;
    	
    	$rootScope.logout = function(){
    		auth.logout();
    		$location.path('/');
    	}
    	
	    $rootScope.$on("$stateChangeStart", function(event, next, current) {
	    	$rootScope.hideNavbar = false;
	    	$rootScope.hideFooter = false;
	    	
	    	if(auth.isLoggedIn() && next.redirectIfLoggedIn){
	    		$location.path('/');
	    	}
	    });
	});

angular
	.module('auth', ['api', 'session'])
	.service('auth',  ['api', 'session', function(api, session){
		this.isLoggedIn = function(){
			return session.getCurrentUser() !== undefined && session.getCurrentUser() !== null;
		}
		
		this.logout = function(){
			session.removeAuthToken();
			session.removeCurrentUser();
		}
	}]);

angular
	.module('session', ['ngStorage'])
	.service('session',  ['$localStorage', '$sessionStorage', function($localStorage, $sessionStorage){	
		// Auth Token
		this.getAuthToken = function(){
			return $localStorage.token;
		}
		
		this.setAuthToken = function(token){
			$localStorage.token = token;
		}
		
		this.removeAuthToken = function(){
			delete $localStorage.token;
		}
		
		// Current User
		this.getCurrentUser = function(){
			return $localStorage.user;
		}
		
		this.setCurrentUser = function(user){
			$localStorage.user = user;
		}
		
		this.removeCurrentUser = function(){
			delete $localStorage.user;
		}
	}]);

angular
	.module('messagingService', ['api'])
	.service('messaging', ['api', function(api){
		// Channels
		this.createChannel = function(){
			return api.channel.save();
		}
		
		this.getChannels = function(){
			return api.channel.query();
		}
		
		this.getChannel = function(id){
			return api.channel.getOne({id: id});
		}
		
		this.getChannelMessages = function(id){
			return api.channel.getMessages({id: id});
		}
		
		this.sendChannelMessage = function(id, message){
			api.channel.postMessage({id: id}, {contents: message});
		}
		
		// Private Discussions 
		this.createPrivateDiscussion = function(){
			return api.privateDiscussion.save();
		}
		
		this.getPrivateDiscussions = function(){
			return api.privateDiscussion.query();
		}
		
		this.getPrivateDiscussion = function(id){
			return api.privateDiscussion.getOne({id: id});
		}
		
		this.getPrivateDiscussionMessages = function(id){
			return api.privateDiscussion.getMessages({id: id});
		}
		
		this.sendPrivateDiscussionMessage = function(id, message){
			api.privateDiscussion.postMessage({id: id}, {contents: message});
		}
	}])
	

angular
	.module('api', ['ngResource', 'session'])
    .factory('api', ['$resource', 'session', function($resource, session){
    	var serverAddress = 'http://spherechat.tk:8000/';
    	
    	var api = {};
    	
    	// Auth
    	api.login = $resource(serverAddress + 'api/auth/login/');
    	api.register = $resource(serverAddress + 'api/auth/registration/');
    	
    	// Friends
    	api.friendship = $resource(serverAddress + 'api/friendship/friendship/', {headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}});
    	api.friendrequest = $resource(serverAddress + 'api/friendship/friendrequest/', {headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}});
    	
    	// Users
    	api.user = $resource(serverAddress + 'api/users/', {headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}}, {
    		'getCurrent': {
    			method: 'GET',
    			url: serverAddress + 'api/me/',
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		},
    		'getOne': {
    			method: 'GET',
    			url: serverAddress + 'api/users/:id/',
    			params: {id: '@id'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		}
    	});
    	
    	// Channels
    	api.channel = $resource(serverAddress + 'api/messaging/channel/', {headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}}, {
    		'getOne': {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/channel/:id/',
    			params: {id: '@id'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		},
	    	'getMessages': {
				method: 'GET',
				url: serverAddress + 'api/messaging/channel/:id/message/',
				params: {id: '@id'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}, 
    			isArray: true
			},
    		'getOneMessage': {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/channel/:channelId/message/:messageId/',
    			params: {discussionId: '@channelId', messageId: '@messageId'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		},
			'postMessage': {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/channel/:channelId/message/',
    			params: {discussionId: '@channelId'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		},
    		'postHeartbeat': {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/channel/:id/tune/',
    			params: {id: '@id'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		},
    		'postAllRead': {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/channel/:id/see/',
    			params: {id: '@id'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		},
    		'postAddMembers': {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/channel/:id/add_members/',
    			params: {id: '@id'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		}
    	});
    	
    	// Private Disscussions
    	api.privateDiscussion = $resource(serverAddress + 'api/messaging/privatediscussion/', {headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}}, {
    		'getOne': {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/privatediscussion/:id/',
    			params: {id: '@id'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		},
    		'getMessages': {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/privatediscussion/:id/message/',
    			params: {id: '@id'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}, 
    			isArray: true
    		},
    		'getOneMessage': {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/privatediscussion/:discussionId/message/:messageId/',
    			params: {discussionId: '@discussionId', messageId: '@messageId'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		},
    		'postMessage': {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/privatediscussion/:discussionId/message/',
    			params: {discussionId: '@discussionId'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		},
    		'postHeartbeat': {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/privatediscussion/:id/tune/',
    			params: {id: '@id'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		},
    		'postAllRead': {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/privatediscussion/:id/see/',
    			params: {id: '@id'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		},
    		'postAddMembers': {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/privatediscussion/:id/add_members/',
    			params: {id: '@id'},
    			headers: {'Authorization': function(){return 'Token ' + session.getAuthToken()}}
    		}
    	});
    	return api;
    }]);