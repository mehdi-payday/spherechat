
describe('api factory tests', function() {
	
	var api;
	var $httpBackend;
	
	beforeEach(angular.mock.module('myApp'));
	
	beforeEach(angular.mock.module('ngMock'));
	
	beforeEach(inject(function(_$httpBackend_) {
		$httpBackend = _$httpBackend_;
	}));
	
	beforeEach(inject(function(_api_){
		api = _api_;
	}));
	
	it('$httpBackend object should exist', function(){
		expect($httpBackend).toBeDefined();
	});
	
	it('api factory object should exist', function(){
		expect(api).toBeDefined();
	});
});