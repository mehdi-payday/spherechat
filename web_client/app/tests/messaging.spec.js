
describe('messaging Service tests', function(){
	
	var session;
	var messaging;
	var serverAddress;
	var $httpBackend;
	
	beforeEach(angular.mock.module('myApp'));
	
	beforeEach(angular.mock.module('ngMock'));
	
	beforeEach(inject(function(_$httpBackend_) {
		$httpBackend = _$httpBackend_;
	}));
	
	beforeEach(inject(function(_messaging_, _api_, _session_){
		messaging = _messaging_;
		serverAddress = _api_.serverAddress;
		session = _session_;
	}));
	
	it('$httpBackend object should exist', function(){
		expect($httpBackend).toBeDefined();
	});
	
	it('messaging object should exist', function(){
		expect(messaging).toBeDefined();
	});
	
	it('session object should exist', function(){
		expect(session).toBeDefined();
	});
	
	it('createChannel should post Authorization header', function(){
		session.setAuthToken('x');
		$httpBackend.expect('POST', serverAddress + 'api/messaging/channel/', undefined, function(headers){
			return headers['Authorization'] === 'Token x';
		}).respond(200, 'success');
		messaging.createChannel();
		$httpBackend.flush();
	});
	
	it('getChannel should return Channel Object', function(){
		session.setAuthToken('x');
		$httpBackend.expect('GET', serverAddress + 'api/messaging/channel/1/').respond(200, 'success');
		messaging.getChannel(1);
		$httpBackend.flush();
	});
	
	it('getChannels should return array of Channel Objects', function(){
		session.setAuthToken('x');
		$httpBackend.expect('GET', serverAddress + 'api/messaging/channel/').respond(200, ['success']);
		messaging.getChannels();
		$httpBackend.flush();
	});
	
	it('getChannelMessages should return array of Message Objects per channel id', function(){
		session.setAuthToken('x');
		$httpBackend.expect('GET', serverAddress + 'api/messaging/channel/1/message/').respond(200, ['success']);
		messaging.getChannelMessages(1);
		$httpBackend.flush();
	});
	
	it('sendChannelMessage should return Message Object', function(){
		session.setAuthToken('x');
		$httpBackend.expect('POST', serverAddress + 'api/messaging/channel/1/message/').respond(200, 'success');
		messaging.sendChannelMessage(1, "Hello");
		$httpBackend.flush();
	});
});