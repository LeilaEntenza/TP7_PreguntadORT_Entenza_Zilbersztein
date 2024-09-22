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
        let nivelElegido = document.querySelector('input[name="dificultadElegida"]:checked').value;
        let modoElegido = document.querySelector('input[name="modoElegido"]:checked').value;

        // Enviar resultado al servidor
        enviarResultado(seccionSeleccionada, nivelElegido, modoElegido);

        girando = false; // Reiniciar el estado después de que termine el giro
    }, 4000);
}

function enviarResultado(seccionSeleccionada, nivelElegido, modoDeJuego) {
    console.log(`Enviando: Categoria=${seccionSeleccionada}, Dificultad=${nivelElegido}, Modo=${modoDeJuego}`);

    fetch(`/Home/RecibirCategoria`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            categoriaElegida: seccionSeleccionada,
            dificultadElegida: nivelElegido,
            modoElegido: modoDeJuego
        })
    })
        .then(response => {
            console.log(response); // Inspeccionar la respuesta aquí
            if (!response.ok) {
                return response.text().then(text => { throw new Error(text); });
            }
            return response.json();
        })
        .then(data => {
            if (data.redirectUrl) {
                window.location.href = data.redirectUrl;
            } else {
                console.error('No se recibió URL de redireccionamiento');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

let tiempoRestante = 3;
let temporizador;

// Iniciar el temporizador solo en la página específica
if (window.location.pathname === '/Home/mostrarpregunta') {
    // Iniciar el temporizador
    temporizador = setInterval(function () {
        if (tiempoRestante > 0) {
            tiempoRestante--;
            document.getElementById('tiempo').textContent = tiempoRestante;
        } else {
            clearInterval(temporizador);
            enviarRespuesta(null); // Si el tiempo se agota, enviamos `null`
        }
    }, 1000);
}

// Función para enviar la respuesta al servidor
function enviarRespuesta(respuesta) {
    console.log('Respuesta a enviar:', respuesta); // Verifica el valor aquí

    // Usar FormData para enviar los datos
    const formData = new FormData();
    formData.append('respuesta', respuesta);

    fetch('/Home/VerificarRespuesta', {
        method: 'POST',
        body: formData
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
    .catch(error => {
        console.error('Error en la respuesta del servidor:', error);
    });
}


//TEMPORIZADOR 2
let tiempoRestante2 = 3;
if (window.location.pathname === '/Home/Respuesta') {
    // Iniciar el temporizador
    temporizador2 = setInterval(function () {
        if (tiempoRestante2 > 0) {
            tiempoRestante2--;
            document.getElementById('tiempo2').textContent = tiempoRestante2;
        } else {
            clearInterval(temporizador2);
            fetch('/Home/Pregunta', {
                method: 'GET',
            })
        }
    }, 1000);
}



