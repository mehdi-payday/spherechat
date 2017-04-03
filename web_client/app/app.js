'use strict';

var serverAddress = 'spherechat.tk:8080/api/';

angular
	.module('objects' [])
	.factory('Objects', function(){
		var objects = {};
		
    	objects.thread = function(title, slug, members, type, description, manager_user, creator_user, active){
    		this.title = title,
    		this.slug = slug,
    		this.members = members,
    		this.type = type,
    		this.description = description,
    		this.manager_user = manager_user,
    		this.creator_user = creator_user,
    		this.active = active;
    	}
    	
    	objects.message = function(user_sender, thread, contents, sent_date, attachment, message_type){
    		this.user_sender = user_sender,
    		this.thread = thread,
    		this.contents = contents,
    		this.sent_date = sent_date,
    		this.attachment = attachment,
    		this.message_type = message_type;
    	}
    	
    	objects.messageTag = function(tagged_user, message, placeholder_position){
    		this.tagged_user = tagged_user,
    		this.message = message,
    		this.placeholder_position;
    	}
    	
    	objects.membership = function(user, thread, last_seen_date, last_seen_message, active, join_date){
    		this.user = user,
    		this.thread = thread,
    		this.last_seen_date = last_seen_date,
    		this.last_seen_message = last_seen_message,
    		this.active = active,
    		this.join_date = join_date;
    	}
    	
    	objects.friendship = function(requester_user, addresser, status, request_date, approval_date, friendship_end_date, active){
    		this.requester_user = requester_user,
    		this.addresser = addresser,
    		this.status = status,
    		this.request_date = request_date,
    		this.approval_date = approval_date,
    		this.friendship_end_date = friendship_end_date,
    		this.active = active;
    	}
    	return objects;
    });

angular
	.module('api', ['ngResource'])
    .factory('Api', function($resource){
    	var api = {};
    	
    	api.login = function() {return $resource(serverAddress + 'auth/login');};
    	api.register = function() {return $resource(serverAddress + 'auth/registration');};
    	
    	api.friendship = function() {return $resource(serverAddress + 'api/friendship/friendship');};
    	api.friendrequest = function() {return $resource(serverAddress + 'api/friendship/friendrequest');};
    	
    	api.user = function() {return $resource(serverAddress + 'user', {}, {
    		getCurrent: {
    			method: 'GET',
    			url: serverAddress + 'me'
    		}
    		getOne: {
    			method: 'GET',
    			url: serverAddress + 'user/:id',
    			params: {id: '@id'}
    		}
    	});};
    	
    	api.channel = function() {return $resource(serverAddress + 'api/messaging/channel', {}, {
    		getOne: {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/channel/:id',
    			params: {id: '@id'}
    		}
	    	getMessages: {
				method: 'GET',
				url: serverAddress + 'api/messaging/channel/:id/message',
				params: {id: '@id'}
			}
    		getOneMessage: {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/channel/:channelId/message/:messageId',
    			params: {discussionId: '@channelId', messageId: '@messageId'}
    		}
			postMessage: {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/channel/:channelId/message/',
    			params: {discussionId: '@channelId'}
    		}
    	});};
    	
    	api.privateDiscussion = function() {return $resource(serverAddress + 'api/messaging/privatediscussion', {}, {
    		getOne: {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/privatediscussion/:id',
    			params: {id: '@id'}
    		}
    		getMessages: {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/privatediscussion/:id/message',
    			params: {id: '@id'}
    		}
    		getOneMessage: {
    			method: 'GET',
    			url: serverAddress + 'api/messaging/privatediscussion/:discussionId/message/:messageId',
    			params: {discussionId: '@discussionId', messageId: '@messageId'}
    		}
    		postMessage: {
    			method: 'POST',
    			url: serverAddress + 'api/messaging/privatediscussion/:discussionId/message/',
    			params: {discussionId: '@discussionId'}
    		}
    	});};

    	return api;
    });
	


angular
	.module('myApp', [
      'ngRoute',
      'myApp.client',
      'myApp.version',
      'objects',
      'api'
    ])
    .config(['$locationProvider', '$routeProvider', function($locationProvider, $routeProvider) {
      $locationProvider.hashPrefix('!');
    
      $routeProvider.otherwise({redirectTo: '/login'});
    }])
    
    .run(function(){
        var distX = 0 , distY = 0;
        var viewportX,viewportY;
        
        $(document).on('mousedown touchstart', '.touch-y', function(e) {
          var $ele = $(this);
          viewportY = (-1 * $ele.height()) + viewport($ele).height();
          var startY = e.pageY || e.originalEvent.touches[0].pageY;
          var currentY = parseFloat($ele.css('transform').split(',')[5]);
          
          $(document).on('mousemove touchmove', function(e) {
            e.preventDefault();
            var y = e.pageY || e.originalEvent.touches[0].pageY;
            distY = y - startY;
            if(currentY){
              distY += currentY;
            }
            if(distY > 3 || distY < (-3)){
              $(".messages-list > ul > li").off('click');
            }
            $ele.css('transform', 'translateY('+ distY +'px)');
            if(distY > 0 || distY < viewportY){
              if(viewport($ele).find('.viewportShadow').length == 0)
              viewport($ele).append('<span class="viewportShadow"></span>');
            }
            if(distY > 0){
              viewport($ele).find('.viewportShadow').css({
                'top': '-5px',
                'box-shadow': '0px 0px 140px ' + distY/5 + 'px rgba(255,183,0,0.8)'
              });
            }else if(distY < viewportY){
              viewport($ele).find('.viewportShadow').css({
                'bottom': '-5px',
                'box-shadow': '0px 0px 140px ' + ((-1*distY) + viewportY)/5 + 'px rgba(255,183,0,08)'
              });
            }
          });
          
          $(document).on('mouseup touchend', function() {
            $(document).off('mousemove touchmove mouseup touchend');
            if (!distY) return;
            if(distY > 0 ){
              $ele.css('transform', 'translateY(0)').addClass("reset");
              viewport($ele).find('.viewportShadow').css('box-shadow','0 0 0 rgba(255,183,0,.5)');
            }else if(distY < viewportY){
              if($ele.height() > viewport($ele).height()){
                $ele.css('transform', 'translateY(' + viewportY + 'px)').addClass("reset");
              }else{
                $ele.css('transform', 'translateY(0)').addClass("reset");
              }
              viewport($ele).find('.viewportShadow').css('box-shadow','0 0 0 rgba(255,183,0,.5)');
            }
            setTimeout(function(){
              $ele.removeClass("reset");
              viewport($ele).find('.viewportShadow').remove();
              $(".messages-list > ul > li").bind('click',showMessages);
            },400);
          });
        });

        // Get viewport
        function viewport(ele){
          return ele.closest('[data-viewport="true"]');
        }

        // nav control
        var $el, leftPos, newWidth,
        $mainNav = $("#master-nav ul");
        var $active_line = $("#active-line");
        $active_line.css("left", $("#master-nav ul .active").position().left);
        navItemPos($("#master-nav ul .active").index());
        $("#master-nav ul li").click(function() {
            $el = $(this);
            $("#master-nav ul li").removeClass('active');
            $el.addClass('active');
            navItemPos($el.index());
            leftPos = $el.position().left;
            newWidth = $el.parent().width();
            $active_line.stop().animate({
                left: leftPos
            });
        });
        
        function navItemPos(index){
          $("#master-nav-items > div").removeClass("active after before");
          $("#master-nav-items > div").eq(index).addClass("active");
          $("#master-nav-items > div").eq(index).nextAll().addClass("after");
          $("#master-nav-items > div").eq(index).prevAll().addClass("before");
        }
        $(".messages-list > ul > li").bind('click',showMessages);
        
        function showMessages(){
          $('.view-main').addClass("deactive");
          $('.view-message').addClass("active");
        }
        $(".back-arrow").click(function(){
          $('.view-main').removeClass("deactive");
          $('.view-message').removeClass("active");
        })

        $("#send-message").click(sendMassage);
        $(document).keyup(function(e){
          if(e.which==13 && $('.view-message').hasClass("active")){
            sendMassage();
          }
        })
        
        function sendMassage(){
          var date = new Date();
          var message = $("#message-text").val();
          $("#message-text").val("");
          var messageItem = "<li class='sent goto'><div>"+message+"<span>"+getTime(date)+"</span></div></li>";
          $(".messages-area > ul").append(messageItem);
          setTimeout(function(){
            $(document).find(".goto").removeClass("goto");
            systemMessage(date);
            messageScrollFix();
          },50);
        }
        
        function systemMessage(date){
          var messages=[
            "Hi my name is yousef sami",
            "سلام خوبی؟",
            "If you want contact with me please send email to this address <br/> 'yousef.sami19@gmail.com'",
            "Thank you for watching this pen"
          ]
          setTimeout(function(){
            var messageItem = "<li class='recive'><div>"+messages[parseInt(Math.random(1,4)*4)]+"<span>"+getTime(date)+"</span></div></li>";
            $(".messages-area > ul").append(messageItem);
            messageScrollFix();
          },1500);
        }
        
        function getTime(date) {
          var hours = date.getHours();
          var minutes = date.getMinutes();
          var ampm = hours >= 12 ? 'PM' : 'AM';
          hours = hours % 12;
          hours = hours ? hours : 12;
          minutes = minutes < 10 ? '0'+minutes : minutes;
          var strTime = hours + ':' + minutes + ' ' + ampm;
          return strTime;
        }
        
        function messageScrollFix(){
          setTimeout(function(){
            var mes_hieght=parseInt($(".messages-area > ul").height());
            var viewportHeight = parseInt($(".messages-area").height());
            if(mes_hieght > viewportHeight)
            $(".messages-area > ul").css("transform","translateY(" + ((viewportHeight - mes_hieght)-10) + "px)");
          },100);
        }
      });
      
	  $(".messages").animate({ scrollTop: $(document).height() }, "fast");

      $("#profile-img").click(function() {
        $("#status-options").toggleClass("active");
      });

      $(".expand-button").click(function() {
        $("#profile").toggleClass("expanded");
        $("#contacts").toggleClass("expanded");
      });

      $("#status-options ul li").click(function() {
        $("#profile-img").removeClass();
        $("#status-online").removeClass("active");
        $("#status-away").removeClass("active");
        $("#status-busy").removeClass("active");
        $("#status-offline").removeClass("active");
        $(this).addClass("active");
        
        if($("#status-online").hasClass("active")) {
          $("#profile-img").addClass("online");
        } else if ($("#status-away").hasClass("active")) {
          $("#profile-img").addClass("away");
        } else if ($("#status-busy").hasClass("active")) {
          $("#profile-img").addClass("busy");
        } else if ($("#status-offline").hasClass("active")) {
          $("#profile-img").addClass("offline");
        } else {
          $("#profile-img").removeClass();
        };
        
        $("#status-options").removeClass("active");
      });

      function newMessage() {
        message = $(".message-input input").val();
        if($.trim(message) == '') {
          return false;
        }
        $('<li class="sent"><img src="http://emilcarlsson.se/assets/mikeross.png" alt="" /><p>' + message + '</p></li>').appendTo($('.messages ul'));
        $('.message-input input').val(null);
        $('.contact.active .preview').html('<span>You: </span>' + message);
        $(".messages").animate({ scrollTop: $(document).height() }, "fast");
      };

      $('.submit').click(function() {
        newMessage();
      });

      $(window).on('keydown', function(e) {
        if (e.which == 13) {
          newMessage();
          return false;
        }
      });

      (function(){
        
        var searchFilter = {
          options: { valueNames: ['name'] },
          init: function() {
            var userList = new List('people-list', this.options);
            var noItems = $('<li id="no-items-found">No items found</li>');
            
            userList.on('updated', function(list) {
              if (list.matchingItems.length === 0) {
                $(list.list).append(noItems);
              } else {
                noItems.detach();
              }
            });
          }
        };
        
        searchFilter.init();
    });
