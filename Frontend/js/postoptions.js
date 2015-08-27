var days = [];
var ir = "";

function Postarticle() 
{	
	if ($("#mon").prop('checked')) {
		days += "1";
	} else {
		days += "0";
	}
	if ($("#tue").prop('checked')) {
		days += "1";
	} else {
		days += "0";
	}
	if ($("#wed").prop('checked')) {
		days += "1";
	} else {
		days += "0";
	}
	if ($("#thu").prop('checked')) {
		days += "1";
	} else {
		days += "0";
	}
	if ($("#fri").prop('checked')) {
		days += "1";
	} else {
		days += "0";
	}
	if ($("#sat").prop('checked')) {
		days += "1";
	} else {
		days += "0";
	}
	if ($("#sun").prop('checked')) {
		days += "1";
	} else {
		days += "0";
	}
	
	if ($("#ir").val('on')) {
		ir = "true";
	} else {
		ir = "false";
	}
	
    $.ajax({
    url: "http://84.237.250.21:55000/alarms/add?days="+encodeURIComponent(days)+"&tunelocation="+encodeURIComponent($("#tunelocation").val())+"&ir="+ir+"&snoozecount="+encodeURIComponent($("#snoozecount").val())+"&alarmtext="+encodeURIComponent($("#alarmtext").val())+"&owner="+encodeURIComponent($("#owner").val())+"&hh="+encodeURIComponent($("#hh").val())+"&mm="+encodeURIComponent($("#mm").val()),
    dataType: "json",
    success: function (response) {
        console.log('Success response: ' + JSON.stringify(response));
	toastr.success('Alarm sucesfully added!');
    },
    fail: function (response) {
        console.log('Fail response: ' + JSON.stringify(response));
	toastr.error('Error adding alarm');
    }
	});


}