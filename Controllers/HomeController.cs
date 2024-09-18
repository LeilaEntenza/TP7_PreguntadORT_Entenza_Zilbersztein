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
        Juego.GuardarCategoria(categoriaElegida);
        /*Juego.GuardarDificultad(dificultadElegida);*/
        Thread.Sleep(1000);
        var redirectUrl = Url.Action("Pregunta", "Home");
        return Json(new { redirectUrl });
    }
    public IActionResult Pregunta()
    {
        Juego.CargarPregunta();
        return RedirectToAction("mostrarpregunta");
    }
    public IActionResult mostrarPregunta()
    {
        List<Respuestas> opciones = Juego.CargarRespuestas();
        ViewBag.opciones = opciones;
        ViewBag.colorFondo = Juego.ObtenerColor();
        ViewBag.Categoria = Juego.GuardarCategoria(Juego.SeccionElegida).Nombre;
        ViewBag.Puntaje = Juego.TraerPuntaje();
        ViewBag.preguntaEnunciado = Juego.pregunta.Enunciado;
        ViewBag.direccionImagen = "/images/" + ViewBag.Categoria + ".png";
        return View("pregunta");
    }
    public IActionResult SeleccionarRespuestaCorrecta(int respuesta)
    {
        Juego.SeleccionarRespuestaCorrecta();
        bool esCorrecto = Juego.VerificarRespuesta(respuesta);
        ViewBag.esCorrecto = esCorrecto;
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
    public IActionResult arte(){
        return View("arte");
    }
    public IActionResult Creditos(){
        return View("creditos");
    }
}
