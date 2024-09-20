let girando = false; // Variable de estado

function girar() {
    if (girando) return; // Salir si ya está girando
    girando = true; // Establecer el estado a girando
    var ruleta = document.getElementById("ruleta");

    // Generar un número aleatorio entre 0 y 6 (para las 7 secciones)
    let seccionSeleccionada = Math.floor(Math.random() * 7) + 1; // De 1 a 7

    // Cada sección ocupa 51.43 grados (360 / 7)
    let gradosPorSeccion = 360 / 7;

    // Calcular el giro total: 5 vueltas completas (5 * 360) + el ángulo para la sección seleccionada
    // Aquí restamos (gradosPorSeccion / 2) para que termine en el medio de la sección
    let vueltasCompletas = 5 * 360; // 5 vueltas completas
    var grados = vueltasCompletas + (8 - seccionSeleccionada) * gradosPorSeccion; // Ajuste para sentido horario

    // Aplicar el giro con animación
    ruleta.style.transition = "transform 4s ease-out";
    ruleta.style.transform = `rotate(${grados}deg)`;

    setTimeout(function () {
        // Obtener la dificultad seleccionada
        let dificultadElegida = document.querySelector('input[name="dificultadElegida"]:checked').value;

        // Enviar resultado al servidor
        enviarResultado(seccionSeleccionada, dificultadElegida);

        girando = false; // Reiniciar el estado después de que termine el giro
    }, 4000);
}

function enviarResultado(seccionSeleccionada, dificultadElegida) {
    console.log(`Sección: ${seccionSeleccionada}, Dificultad: ${dificultadElegida}`);

    fetch(`/Home/RecibirCategoria`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ categoriaElegida: seccionSeleccionada, dificultadElegida: dificultadElegida }) // sin +1 aquí
    })
        .then(response => response.json())
        .then(data => {
            if (data.redirectUrl) {
                window.location.href = data.redirectUrl;
            } else {
                console.error('No se recibió URL de redireccionamiento');
            }
        });
}








// //TEMPORIZADOR
let tiempoRestante = 3; // Tiempo inicial en segundos
let formularioEnviado = false; // Variable para saber si el formulario fue enviado

// // Iniciar el temporizador
if (window.location.pathname === '/Home/mostrarpregunta') {
    let temporizador = setInterval(function () {
        if (tiempoRestante > 0) {
            tiempoRestante--;
            document.getElementById('tiempo').textContent = tiempoRestante;
        } else {
            clearInterval(temporizador);
            ejecutarMetodoAlLlegarACero(); // Llamar método si no se envió el formulario
        }
    }, 1000);
}

// Función para llamar al método del servidor cuando el tiempo llegue a 0
function ejecutarMetodoAlLlegarACero() {
    console.log("El tiempo ha llegado a 0, ejecutando método...");

    // Ejemplo de llamada a un método del controlador usando fetch
    fetch('/Home/mostrarunavista', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ mensaje: 'Tiempo agotado' })
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