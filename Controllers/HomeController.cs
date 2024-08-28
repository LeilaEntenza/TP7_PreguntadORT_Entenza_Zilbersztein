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

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult configurarJuego()
    {
        Juego.InicializarJuego();
        ViewBag.categoria = Juego.ObtenerCategorias();
        ViewBag.dificultad = Juego.ObtenerDificultades();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Comenzar(string username, int dificultad, int categoria) {
        int cantPreguntas;
        string ret;
        Juego.CargarPartida(username, dificultad, categoria);
        cantPreguntas=BD.ObtenerPreguntas(dificultad, categoria).Count;
        if(cantPreguntas>0){
            ret="Jugar";
        }
        else{
            ret="ConfigurarJuego";
        }
        return View(ret);
    }
}
