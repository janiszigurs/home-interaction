window.onload = function(){
	$('.nav-wrapper').append(
		      '<a href="#" class="brand-logo right">Welcome: Arturs</a>\
	      <ul id="nav-mobile" class="left hide-on-med-and-down">\
					<li>\
						<a href="addarticle.html">Add Alarm</a>\
					</li>\
					<li>\
						<a href="alarms.html">Alarms</a>\
					</li>\
					<li>\
						<a href="reminders.html">Reminders</a>\
					</li>\
					<li>\
						<a href="addreminder.html">Add Reminder</a>\
					</li>\
					<li>\
						<a href="http://localhost:55000/index.html">API</a>\
					</li>\
	      </ul>')
	$('.page-footer').append(
          '<div class="footer-copyright">\
            <div class="container">\
            © 2015 A.Z J.Z A.O\
            <a class="grey-text text-lighten-4 right" href="#!">More Links</a>\
            </div>\
          </div>')
}
