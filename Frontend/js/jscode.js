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
