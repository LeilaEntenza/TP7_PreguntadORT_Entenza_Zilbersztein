function girar() {
    var ruleta = document.getElementById("ruleta");
    var grados = Math.floor(Math.random() * 360) + 1440;
    ruleta.style.transition = "transform 4s ease-out";
    ruleta.style.transform = `rotate(${grados}deg)`;

    setTimeout(function () {
        ruleta.style.transition = "none"; 
        var degActual = grados % 360; 
        ruleta.style.transform = `rotate(${degActual}deg)`;

        
        var seccionSeleccionada = Math.floor(degActual / 51,42857142857); 
        
        enviarResultado(seccionSeleccionada);

    }, 4000);
}

function enviarResultado(seccionSeleccionada) {
    fetch('/Home/RecibirCategoria', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ categoriaElegida: seccionSeleccionada })
    })
    .then(response => response.json()) 
    .then(data => {
        if (data.redirectTo) {
            window.location.href = data.redirectTo; 
        }
    })
    .catch(error => {
        console.error('Error al enviar la solicitud:', error);
    });
}











