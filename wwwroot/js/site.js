function girar() {
    var ruleta = document.getElementById("ruleta");
    var grados = Math.floor(Math.random() * 360) + 1440;
    ruleta.style.transition = "transform 4s ease-out";
    ruleta.style.transform = `rotate(${grados}deg)`;

    setTimeout(function () {
        ruleta.style.transition = "none"; 
        var degActual = grados % 360; 
        ruleta.style.transform = `rotate(${degActual}deg)`;
        let seccionSeleccionada;
        seccionSeleccionada = Math.floor(degActual / 51,42857142857);
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
//TEMPORIZADOR
// let tiempoRestante = 3; // Tiempo inicial en segundos
// let formularioEnviado = false; // Variable para saber si el formulario fue enviado

// // Iniciar el temporizador
// let temporizador = setInterval(function() {
//     if (tiempoRestante > 0) {
//         tiempoRestante--;
//         document.getElementById('tiempo').textContent = tiempoRestante;
//     } else{
//         clearInterval(temporizador);
//         ejecutarMetodoAlLlegarACero();
//     }
// }, 1000);


// function ejecutarMetodoAlLlegarACero() {
//     console.log("El tiempo ha llegado a 0, ejecutando método...");
    
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
            
//             window.location.href = data.redirectUrl;
//         } else {
//             console.error('No se recibió URL de redireccionamiento');
//         }
//     })
// }











