function hideLoading(toshow, tohide) {
    document.getElementById(tohide).style.display = 'none';
    document.getElementById(toshow).style.display = 'block';
}

$.ajax({
			    url: "http://localhost:55000/alarms?user=zigurs93",
			    success: function(response) {
			        $.each(response, function (i, alarm) {
			            $('#alarms').append('<div class="col s12 m6"> <div class="card blue-grey darken-1" id="'+ alarm.id +'"> <span class="card-title">'+alarm.AlarmTime+'</span><p>'
							 + 'Is Repeatable: '+ alarm.isRepeatable + '</br>'
                             + 'Active monday: '+ alarm.weekdays[0] + '</br>'
                             + 'Active tuesday: '+ alarm.weekdays[1] + '</br>'
                             + 'Active wednesday: '+ alarm.weekdays[2] + '</br>'
                             + 'Active thursday: '+ alarm.weekdays[3] + '</br>'
                             + 'Active friday: '+ alarm.weekdays[4] + '</br>'
                             + 'Active saturday: '+ alarm.weekdays[5] + '</br>'
                             + 'Active sunday: '+ alarm.weekdays[6] + '</br> <div class="card-action">'
                             + '</p><a href="alarm.html?alarm='+alarm.id+ '">Visit article</a></div></div></div>');
			        });
					//hideLoading("allsection", "progressbar");
			    }
				
				
			});
			
			console.log("asd");

String.prototype.trunc = String.prototype.trunc ||
      function(n){
          return this.length>n ? this.substr(0,n-1)+'&hellip;' : this;
      };
      
      /*
            <div class="row">
        <div class="col s12 m6">
          <div class="card blue-grey darken-1">
            <div class="card-content white-text">
              <span class="card-title">Card Title</span>
              <p>I am a very simple card. I am good at containing small bits of information.
              I am convenient because I require little markup to use effectively.</p>
            </div>
            <div class="card-action">
              <a href="#">This is a link</a>
              <a href="#">This is a link</a>
            </div>
          </div>
        </div>
      </div>
   
      
                                   + alarm.SnoozeCount + alarm.weekdays[0] + alarm.weekdays[1] + alarm.weekdays[2] + alarm.weekdays[3] + alarm.weekdays[4] + alarm.weekdays[5] +  alarm.weekdays[6] + 
                             + alarm.AlarmTuneLocation + alarm.AlarmText + alarm.Owner + alarm.AlarmCreated +     */
