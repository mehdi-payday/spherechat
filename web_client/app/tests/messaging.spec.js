
describe('messaging Service tests', function(){
	
	var messaging;
	var serverAddress;
	var $httpBackend;
	
	beforeEach(angular.mock.module('myApp'));
	
	beforeEach(angular.mock.module('ngMock'));
	
	beforeEach(inject(function(_$httpBackend_) {
		$httpBackend = _$httpBackend_;
	}));
	
	beforeEach(inject(function(_messaging_, _api_){
		messaging = _messaging_;
		serverAddress = _api_.serverAddress;
	}));
	
	it('$httpBackend object should exist', function(){
		expect($httpBackend).toBeDefined();
	});
	
	it('messaging object should exist', function(){
		expect(messaging).toBeDefined();
	});
});