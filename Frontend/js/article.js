               //gets comments etc
               var selected = getParameterByName('article');
	    		$.ajax({
	        	url: "http://devnewsapi.azurewebsites.net/api/article/" + selected,
	        	jsonp: "callback",
	        	dataType: "jsonp",
	        	success: function(response) {
	            	$.each(response, function (i, article) {
            				var article =   "<h2>" + response.Author + "</h2>"
                            + "<br>" + response.Added
                            + "<br>" + response.Text
                            + "<br>" + response.Updated
            				$('#checked').html(article);
                    })
                    $("#commentid").val(response.Id);
                    $.each(response.Comments, function (i, comment) {
                        $('section').append(/*'<article class="comment" id="'+comment.Id+'"><h2>'+comment.Author+'</h2><p>' + 
            								 comment.Text +'</p></article>'+ */        
                                         '<div class="card light-green lighten-5"> \
                                            <div class="card-content black-text"> \
                                              <span class="card-title"><font color="black">'+ comment.Author +'</font></span> \
                                              <p>' + comment.Text + '</p> \
                                            </div> \
                                            <div class="card-action">  \
                                              <a href="#">This is a link</a> \
                                              <a href="#">This is a link</a> \
                                            </div> \
                                          </div>' 
                                );                       
            	            });
            	       		}
            		    });
                        