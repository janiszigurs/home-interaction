var select = document.getElementById("hh");
var hourbottom = 0;
var hourtop = 23;
var hours = [];

while(hourbottom <= hourtop){
   hours.push(hourbottom++);
}

for(var i = 0; i < hours.length; i++) {
    var hh = hours[i];
    var el = document.createElement("option");
    el.textContent = hh;
    el.value = hh;
    select.appendChild(el);
}

var select = document.getElementById("mm");
var minutebottom = 0;
var minutetop = 59;
var minutes = [];
while(minutebottom <= minutetop){
   minutes.push(minutebottom++);
}

for(var i = 0; i < minutes.length; i++) {
    var mm = minutes[i];
    var el = document.createElement("option");
    el.textContent = mm;
    el.value = mm;
    select.appendChild(el);
}

$('.datepicker').pickadate({
    selectMonths: true, // Creates a dropdown to control month
    selectYears: 15 // Creates a dropdown of 15 years to control year
  });

  
function Postreminder()
{
	$.ajax({
    url: "http://84.237.250.21:55000/reminders/add?owner="+encodeURIComponent($("#owner").val())+"&priority="+encodeURIComponent($("#priority").val())+"&text="+encodeURIComponent($("#remindertext").val())+"&hh="+encodeURIComponent($("#hh").val())+"&mm="+encodeURIComponent($("#mm").val())+"&date="+encodeURIComponent($("#date").datepicker({ dateFormat: 'dd/mm/yyyy' }).val()),
    dataType: "json",
    success: function (response) {
        console.log('Success response: ' + JSON.stringify(response));
    },
    fail: function (response) {
        console.log('Fail response: ' + JSON.stringify(response));
    }
	});
}

//reminders/add?owner=xxx&priority=2&text=fucks&hh=12&&mm=12&date=22/12/2015