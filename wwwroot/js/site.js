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
        let dificultadElegida = document.querySelector('input[name="dificultadElegida"]:checked').value;
        enviarResultado(seccionSeleccionada, dificultadElegida);

    }, 4000);
}

function enviarResultado(seccionSeleccionada, dificultadElegida) {
    console.log(seccionSeleccionada);
    console.log("dificultad elegida: " + dificultadElegida)
    fetch(`/Home/RecibirCategoria?categoriaElegida=${seccionSeleccionada}&dificultadElegida=${dificultadElegida}`, {
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
// //TEMPORIZADOR
// let tiempoRestante = 3; // Tiempo inicial en segundos
// let formularioEnviado = false; // Variable para saber si el formulario fue enviado

// // Iniciar el temporizador
// let temporizador = setInterval(function() {
//     if (tiempoRestante > 0) {
//         tiempoRestante--;
//         document.getElementById('tiempo').textContent = tiempoRestante;
//     } else {
//         clearInterval(temporizador);
//         ejecutarMetodoAlLlegarACero(); // Llamar método si no se envió el formulario
//     }
// }, 1000);

// // Función para llamar al método del servidor cuando el tiempo llegue a 0
// function ejecutarMetodoAlLlegarACero() {
//     console.log("El tiempo ha llegado a 0, ejecutando método...");
    
//     // Ejemplo de llamada a un método del controlador usando fetch
//     fetch('/Home/mostrarunavista', {
//         method: 'POST',
//         headers: {
//             'Content-Type': 'application/json'
//         },
//         body: JSON.stringify({ mensaje: 'Tiempo agotado' })
//     })
//     .then(response => response.json())
//     .then(data => {
//         if (data.redirectUrl) {
//             // Redirige a la URL proporcionada por el servidor
//             window.location.href = data.redirectUrl;
//         } else {
//             console.error('No se recibió URL de redireccionamiento');
//         }
//     })
// }