using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP7_PreguntadORT_Entenza_Zilbersztein.Models;

namespace TP7_PreguntadORT_Entenza_Zilbersztein.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Juego.InicializarJuego();
        return View();
    }
    public IActionResult ConfigurarJuego()
    {
        Juego.InicializarJuego();
        return View();
    }
    public IActionResult Ruleta()
    {
        ViewBag.puntos = Juego.TraerPuntaje();
        return View();
    }
    public IActionResult Comenzar(string username)
    {
        Juego.GuardarUsuario(username);
        return View("ruleta");
    }
    [HttpPost]
    public IActionResult RecibirCategoria(int categoriaElegida, int dificultadElegida)
    {
        categoriaElegida = 1;
        Juego.GuardarCategoria(categoriaElegida);
        Juego.GuardarDificultad(dificultadElegida);
        Thread.Sleep(1000);
        if (categoriaElegida != 5)
        {
            var redirectUrl = Url.Action("Pregunta", "Home");
            return Json(new { redirectUrl });
        }
        else
        {
            var redirectUrl = Url.Action("Comodin", "Home");
            return Json(new {redirectUrl});
        }
        
    }
    public IActionResult Comodin()
    {
        ViewBag.categorias = Juego.ObtenerCategorias();
        ViewBag.categorias.RemoveAt(4);
        ViewBag.direccionImagen = "/images/" + ViewBag.Categoria + ".png";
        return View("elegircategoria");
    }
    public IActionResult Pregunta(int categoria)
    {
        if (Juego.categoriaElegida.IdCategoria == 5)
        Juego.GuardarCategoria(categoria);
        Juego.CargarPregunta();
        return RedirectToAction("mostrarpregunta");
    }
    public IActionResult mostrarPregunta()
    {
        List<Respuestas> opciones = Juego.CargarRespuestas();
        ViewBag.opciones = opciones;
        ViewBag.colorFondo = Juego.ObtenerColor();
        ViewBag.Categoria = Juego.categoriaElegida.Nombre;
        ViewBag.Puntaje = Juego.TraerPuntaje();
        ViewBag.preguntaEnunciado = Juego.pregunta.Enunciado;
        ViewBag.direccionImagen = Juego.TraerFoto();
        return View("pregunta");
    }
    public IActionResult VerificarRespuesta(string respuesta)
    {
        Juego.SeleccionarRespuestaCorrecta();
        bool esCorrecto = Juego.VerificarRespuesta(respuesta);
        string texto, urlImagen;
        if (esCorrecto)
        {
            texto = "¡¡¡¡¡LA RESPUESTA ES CORRECTA!!!!!";
            urlImagen = "/images/GatoFeliz.gif";
        }
        else
        {
            texto = "La respuesta es incorrecta D:";
            urlImagen = "/images/WalterWhiteFalling.gif";
        }
        ViewBag.texto = texto;
        ViewBag.urlImagen = urlImagen;
        return View("respuesta");
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult Jugar()
    {
        return View("juego");
    }
    public IActionResult Respuesta()
    {
        return View("Respuesta");
    }
    public IActionResult arte()
    {
        return View("arte");
    }
    public IActionResult Creditos()
    {
        return View("creditos");
    }

        public IActionResult Buscar()
    {
        return View("pregunta");
    }

    [HttpPost]
    public IActionResult MostrarUnaVista()
    {
        var redirectUrl = Url.Action("Index", "Home");
        return Json(new { redirectUrl });
    }
}
