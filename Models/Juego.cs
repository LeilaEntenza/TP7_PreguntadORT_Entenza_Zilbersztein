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
        static List<Categorias> ObtenerCategorias()
        {
            return BD.ObtenerCategorias();
        }
        static List<Dificultades> ObtenerDificultades()
        {
            return BD.ObtenerDificultades();
        }
        static void CargarPartida(string Username, int dificultad, int categoria)
        {
            username = Username;
            preguntas = BD.ObtenerPreguntas(dificultad, categoria);
            //no se ha utilizado el atributo respuestas
        }
        static Preguntas ObtenerProximaPregunta()
        {
            Random r = new Random();
            int numero = r.Next(1,preguntas.Count);//no se incluye el último número
            return preguntas[numero];
        }
        static bool VerificarRespuesta(int IdPregunta, int IdRespuesta)
        {
            bool esCorrecto = false;
            respuestas = BD.ObtenerSiguientesRespuestas(IdPregunta);
            int IdRespuestaCorrecta = BD.SeleccionarRespuestaCorrecta(IdPregunta, respuestas);
            if (IdRespuestaCorrecta == IdRespuesta)
            {
                esCorrecto = true;
            }
            return esCorrecto;
        }




    }
}