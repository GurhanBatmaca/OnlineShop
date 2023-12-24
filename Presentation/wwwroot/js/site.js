$(document).ready(function(){

    // layout toast message script
    $("#toast").addClass("show");
    setTimeout(function () { 
        $("#toast").removeClass("show") 
    },4000);


    var btn_close = document.querySelector("#btn_close");
    if(btn_close != null)
    {
        setTimeout(function() {
            btn_close.click();
        },5000);
    }
}) ; 

// footer year script
document.getElementById("year-footer").innerHTML = new Date().getFullYear();

$(document).ready(function(){

    $("#btn-submit").click(function(event){
        event.preventDefault();

        // $.ajax({
        //     type: "POST",
        //     url: Url.Acrion("")

    });

    

});
