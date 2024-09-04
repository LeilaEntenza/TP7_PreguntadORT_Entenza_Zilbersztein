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
    public IActionResult Comenzar(string username)
    {
        Juego.GuardarUsuario(username);
        ViewBag.categoria = Juego.ObtenerCategoria();
        return View("ruleta");
    }
    [HttpPost]
    public IActionResult RecibirCategoria([FromBody] int categoriaElegida)
    {
        ViewBag.categoriaElegida = Juego.GuardarCategoria(categoriaElegida);
        Thread.Sleep(1000);
        return Json(new { redirectTo = Url.Action("pregunta", "Home") });
    }

    public IActionResult Pregunta()
    {
        Preguntas preguntaElegida = Juego.CargarPregunta();
        List<Respuestas> opciones = Juego.CargarRespuestas();
        ViewBag.preguntaElegida = preguntaElegida;
        ViewBag.opciones = opciones;
        return View("pregunta");
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
}
