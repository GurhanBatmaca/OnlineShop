// layout message toast script
$(document).ready(function(){
    $("#toast").addClass("show");
    setTimeout(function () { $("#toast").removeClass("show") },4000)
})  

// footer year script
document.getElementById("year-footer").innerHTML = new Date().getFullYear();
