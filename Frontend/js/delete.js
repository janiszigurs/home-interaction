  $("deletelink").click(function () {
            var value = $("deletelink").attr("id");
            //alert(value);
            var id= value.substring(1, 25);
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
    });