namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    static public class Juego
    {
        private static string username { get; set; }
        private static int puntajeActual { get; set; }
        private static int cantidadPreguntasCorrectas { get; set; }
        private static List<Preguntas> preguntas = new List<Preguntas>();
        private static List<Respuestas> respuestas = new List<Respuestas>();

        static void InicializarJuego()
        {
            username = "";
            puntajeActual = 0;
            cantidadPreguntasCorrectas = 0;
        }
        // static List<Categoria> ObtenerCategorias()
        // {
        //     //completar
        // }
        // static List<Categoria> ObtenerDificultades()
        // {
        //     //completar
        // }
        static void CargarPartida(string Username, int dificultad, int categoria)
        {
            username = Username;
            //completar
        }




    }
}