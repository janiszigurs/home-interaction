function hideLoading(toshow, tohide) {
    document.getElementById(tohide).style.display = 'none';
    document.getElementById(toshow).style.display = 'block';
}

var d=new Date();
console.log(d.getDay());
var t= (d.getDay()+6)%7;

$.ajax({
                
			    url: "http://localhost:55000/alarms?user=zigurs93",
			    success: function(response) {
			        $.each(response, function (i, alarm) {
			            $('#alarms').append('<div class="col s12 m6"> <div class="card blue-grey darken-1" id="'+ alarm.id +'"> <span class="card-title">Alarm Time: '+alarm.AlarmTime.substring(11, 16)+'</span><p>'
				//+ 'Is Repeatable: '+ alarm.isRepeatable + '</br>' 
                             + '<div class="'+alarm.weekdays[0]+'"'+(0===t?'id="today"':'')+'>M</div>' 
                             + '<div class="'+alarm.weekdays[1]+'"'+(1===t?'id="today"':'')+'>T</div>'
                             + '<div class="'+alarm.weekdays[2]+'"'+(2===t?'id="today"':'')+'>W</div>'
                             + '<div class="'+alarm.weekdays[3]+'"'+(3===t?'id="today"':'')+'>T</div>'
                             + '<div class="'+alarm.weekdays[4]+'"'+(4===t?'id="today"':'')+'>F</div>'
                             + '<div class="'+alarm.weekdays[5]+'"'+(5===t?'id="today"':'')+'>S</div>'
                             + '<div class="'+alarm.weekdays[6]+'"'+(6===t?'id="today"':'')+'>S</div></br> <div class="card-action">'
                             + '</p><a href="alarm.html?alarm='+alarm.id+ '">View in details</a> \
                             <a href="javascript:deleteLink(\''+alarm.id+'\');" class="deletelink" id="'+alarm.id+'_delete">Delete Alarm</a> </div></div></div>');
			        });
					//hideLoading("allsection", "progressbar");
			    }
				
				
			});
			
			console.log("asd");

String.prototype.trunc = String.prototype.trunc ||
      function(n){
          return this.length>n ? this.substr(0,n-1)+'&hellip;' : this;
      };

	  
function deleteLink (id){	 
			console.log(id);
            $.ajax({
            url: "http://localhost:55000/alarms/delete/"+encodeURIComponent(id),
            success: function (response) {
                console.log('Success response: ' + JSON.stringify(response));
                //toastr.success('Sucess! server responded: '+ JSON.stringify(response));
                $(location).attr('href',"alarms.html");
            },
            fail: function (response) {
                //toastr.warning('Failure! server responded: '+ JSON.stringify(response));
            }
			});
}
   $(".deletelink").click(function () {
	    console.log("AAA");
        var addressValue = $(this).attr("href");
        alert(addressValue );
    });