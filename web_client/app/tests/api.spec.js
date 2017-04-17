
describe('api factory tests', function() {
	var serverAddress;
	var api;
	var $httpBackend;
	
	beforeEach(angular.mock.module('myApp'));
	
	beforeEach(angular.mock.module('ngMock'));
	
	beforeEach(inject(function(_$httpBackend_) {
		$httpBackend = _$httpBackend_;
	}));
	
	beforeEach(inject(function(_api_){
		api = _api_;
		serverAddress = api.serverAddress;
	}));
	
	it('$httpBackend object should exist', function(){
		expect($httpBackend).toBeDefined();
	});
	
	it('api factory object should exist', function(){
		expect(api).toBeDefined();
	});
	
	it('api serverAddress should exist', function(){
		expect(serverAddress).toBeDefined();
	});
	
	it('api login function', function(){
		$httpBackend.expect('POST', serverAddress + 'api/auth/login/', {username: 'test', password: 'test'}).respond(200, {key: 'x'});
		api.login.save({}, {username: 'test', password: 'test'});
		$httpBackend.flush();
	});
	
	it('api register function', function(){
		$httpBackend.expect('POST', serverAddress + 'api/auth/registration/', {username: 'test', email: 'test', password1: 'test', password2: 'test'}).respond(200, {key: 'x'});
		api.register.save({}, {username: 'test', email: 'test', password1: 'test', password2: 'test'});
		$httpBackend.flush();
	});
});