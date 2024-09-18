function girar() {
    var ruleta = document.getElementById("ruleta");
    var grados = Math.floor(Math.random() * 360) + 1440;
    ruleta.style.transition = "transform 4s ease-out";
    ruleta.style.transform = `rotate(${grados}deg)`;

    setTimeout(function () {
        ruleta.style.transition = "none"; 
        var degActual = grados % 360; 
        ruleta.style.transform = `rotate(${degActual}deg)`;

        // Calcular la sección seleccionada
        let seccionSeleccionada;
        seccionSeleccionada = Math.floor(degActual / 51, 42857142857); // Cada sección ocupa 60 grados
        // Llamar a la función para enviar el resultado
        enviarResultado(seccionSeleccionada);

    }, 4000);
}

function enviarResultado(seccionSeleccionada) {
    console.log(seccionSeleccionada);
    fetch(`/Home/RecibirCategoria?categoriaElegida=${seccionSeleccionada}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(data => {
        if (data.redirectUrl) {
            // Redirige a la URL proporcionada por el servidor
            window.location.href = data.redirectUrl;
        } else {
            console.error('No se recibió URL de redireccionamiento');
        }
    })
}











