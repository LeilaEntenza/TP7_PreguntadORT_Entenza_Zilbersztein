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
        if (Juego.modo == 2)
        Juego.ResetPuntaje();
        ViewBag.puntos = Juego.TraerPuntaje();
        Juego.ResetPregunta();
        return View();
    }
    public IActionResult Comenzar(string Nombre)
    {
        Juego.GuardarUsuario(Nombre);
        return RedirectToAction("ruleta");
    }
    [HttpPost]
    public IActionResult RecibirCategoria(int categoriaElegida, int dificultadElegida, int modoElegido)
    {
        categoriaElegida = 5;
        Juego.GuardarModo(modoElegido);
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
            return Json(new { redirectUrl });
        }

    }
    public IActionResult Comodin()
    {
        ViewBag.categorias = Juego.ObtenerCategorias();
        ViewBag.categorias.RemoveAt(4);
        ViewBag.direccionesImagen = Juego.ObtenerImagenesCategorias(ViewBag.categorias);
        return View("elegircategoria");
    }
    public IActionResult Pregunta(int categoria)
    {
        if (Juego.categoriaElegida.IdCategoria == 5)
            Juego.GuardarCategoria(categoria);
        if (Juego.modo == 1)
        {
            if (!Juego.categoriaYaElegida)
                return RedirectToAction("ruleta");
            else
            {
                Juego.CargarPregunta();
                return RedirectToAction("mostrarpregunta");
            }
        }
        else if (Juego.modo == 2)
        {
            if (!Juego.perdio)
            {
                Juego.CargarPregunta();
                return RedirectToAction("mostrarpregunta");
            }
            else
                return RedirectToAction("vistafinracha");
        }
        else
            return View("creditos");
    }
    public IActionResult mostrarPregunta()
    {
        List<Respuestas> opciones = Juego.CargarRespuestas();
        ViewBag.opciones = opciones;
        ViewBag.colorFondo = Juego.ObtenerColor();
        ViewBag.Categoria = Juego.categoriaElegida.Nombre;
        ViewBag.preguntaEnunciado = Juego.pregunta.Enunciado;
        ViewBag.direccionImagen = Juego.TraerFoto();
        ViewBag.Nombre = Juego.TraerUsuario();
        ViewBag.puntaje = Juego.TraerPuntaje();
        return View("pregunta");
    }
    [HttpPost]
    public IActionResult VerificarRespuesta(string respuesta)
    {
        Juego.SeleccionarRespuestaCorrecta();
        bool esCorrecto = Juego.VerificarRespuesta(respuesta);
        if (esCorrecto)
        {
            Juego.setTexto("¡¡¡¡¡LA RESPUESTA ES CORRECTA!!!!!");
            Juego.setUrlImagen("/images/GatoFeliz.gif");
        }
        else if (respuesta != null && respuesta != "null")
        {
            Juego.setTexto("La respuesta es incorrecta D:");
            Juego.setUrlImagen("/images/WalterWhiteFalling.gif");
        }
        else if (respuesta == null || respuesta == "null")
        {
            Juego.setTexto("Te quedaste sin tiempo!!");
            Juego.setUrlImagen("/images/WalterWhiteFalling.gif");
        }
        else
        {
            Juego.setTexto("tenemos un problema");
            Juego.setUrlImagen("");
        }
        var redirectUrl = Url.Action("Respuesta", "Home");
        return Json(new { redirectUrl });
    }
    public IActionResult RestablecerSegundoModo()
    {
        Juego.RestablecerSegundoModo();
        return RedirectToAction("Pregunta");
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
        ViewBag.texto = Juego.texto;
        ViewBag.urlImagen = Juego.urlImagen;
        if (Juego.modo == 2 && !Juego.perdio)
        {
            ViewBag.siguienteUrl = Url.Action("Pregunta", "Home");
            ViewBag.urlJS = "/Home/Pregunta";
        }
        else if (Juego.modo == 1) {ViewBag.siguienteUrl = Url.Action("Ruleta", "Home");ViewBag.urlJS = "/Home/Ruleta";}
        else if (Juego.modo == 2 && Juego.perdio) {ViewBag.siguienteUrl = Url.Action("vistafinracha", "Home"); ViewBag.urlJS = "/Home/vistafinracha";};
        return View("Respuesta");
    }
    public IActionResult Creditos()
    {
        return View("creditos");
    }

    public IActionResult Tutorial()
    {
        return View("Tutorial");
    }

        public IActionResult Respuestas()
    {
        return View("Respuesta");
    }
    public IActionResult VistaFinRacha()
    {
        ViewBag.racha = Juego.TraerPuntaje();
        return View("findeljuego");
    }
}
