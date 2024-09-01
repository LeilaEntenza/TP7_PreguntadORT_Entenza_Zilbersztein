function girar() {
    var ruleta = document.getElementById("ruleta");
    var grados = Math.floor(Math.random() * 360) + 1440; // Asegura al menos 4 vueltas completas
    ruleta.style.transition = "transform 4s ease-out";
    ruleta.style.transform = `rotate(${grados}deg)`;

    setTimeout(function () {
        ruleta.style.transition = "none"; // Eliminar transición para el siguiente giro
        var degActual = grados % 360; // Obtener el ángulo actual después de que se detenga
        ruleta.style.transform = `rotate(${degActual}deg)`; // Ajustar la posición final

        // Calcular la sección seleccionada
        var seccionSeleccionada = Math.floor(degActual / 51,42857142857); // Cada sección ocupa 60 grados
        // Llamar a la función para enviar el resultado
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
    .then(response => response.json())  // Convertir la respuesta a JSON
    .then(data => {
        if (data.redirectTo) {
            window.location.href = data.redirectTo;  // Redirige a la URL proporcionada
        }
    })
    .catch(error => {
        console.error('Error al enviar la solicitud:', error);
    });
}




//RULETA 2:






