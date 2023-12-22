window.onload = (event) => {

    setTimeout(function(){
        window.location.href = "http://localhost:5282/hesap/girisyap";
    },5000);

    var time = 5;

    setInterval(() => {
        time = (time > 1 ? time - 1 : 1);
        document.getElementById('time').innerText = time;
      }, 1000)
};