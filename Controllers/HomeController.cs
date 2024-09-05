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
        ViewBag.categorias = Juego.ObtenerCategorias();
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
    [HttpPost]
    public IActionResult RecibirCategoria(int categoriaElegida)
    {
        Juego.SeccionElegida = categoriaElegida;
        Juego.GuardarCategoria(categoriaElegida);
        ViewBag.categoria = categoriaElegida;
        Thread.Sleep(2000);
        var redirectUrl = Url.Action("Pregunta", "Home");
        return Json(new { redirectUrl });
    }
    public IActionResult Comenzar(string username)
    {
        Juego.GuardarUsuario(username);
        ViewBag.categoria = Juego.ObtenerCategoria();
        return View("ruleta");
    }
    public IActionResult Pregunta()
    {
        Juego.CargarPregunta();
        return RedirectToAction("mostrarpregunta");
    }
    public IActionResult mostrarPregunta()
    {
        List<Respuestas> opciones = Juego.CargarRespuestas();
        ViewBag.preguntaElegida = Juego.pregunta;
        ViewBag.opciones = opciones;
        return View();
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
}
