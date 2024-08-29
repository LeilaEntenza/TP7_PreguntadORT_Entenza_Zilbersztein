namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    static public class Juego
    {
        private static string username { get; set; }
        private static int puntajeActual { get; set; }
        private static int cantidadPreguntasCorrectas { get; set; }
        private static List<int> preguntasUtilizadas { get; set; } = new List<int>();
        private static List<Preguntas> preguntas = new List<Preguntas>();
        private static List<Respuestas> respuestas = new List<Respuestas>();

        public static void InicializarJuego()
        {
            username = "";
            puntajeActual = 0;
            cantidadPreguntasCorrectas = 0;
        }
        public static List<Categorias> ObtenerCategorias()
        {
            return BD.ObtenerCategorias();
        }
        public static List<Dificultades> ObtenerDificultades()
        {
            return BD.ObtenerDificultades();
        }

        public static void GuardarUsuario(string usuario)
        {
            username = usuario;
        }
        public static List<Preguntas> CargarPartida(string Username, int dificultad)
        {
            username = Username;
            preguntas = BD.ObtenerPreguntas(dificultad, categoria);
            return preguntas;
            //no se ha utilizado el atributo respuestas
        }
        public static int ObtenerCategoria()
        {
            Random r = new Random();
            int numero = r.Next(1,8);
            return numero;
        }
        public static Preguntas ObtenerProximaPregunta()
        {
            Random r = new Random();
            int numero = r.Next(1, preguntas.Count);//no se incluye el último número
            while (preguntasUtilizadas.IndexOf(numero) != -1)
            {
                numero = r.Next(1, preguntas.Count);
            }
            preguntasUtilizadas.Add(numero);
            preguntasUtilizadas.Sort();
            return preguntas[numero];
        }
        public static bool VerificarRespuesta(int IdPregunta, int IdRespuesta)
        {
            bool esCorrecto = false;
            respuestas = BD.ObtenerSiguientesRespuestas(IdPregunta);
            int IdRespuestaCorrecta = BD.SeleccionarRespuestaCorrecta(IdPregunta, respuestas);
            if (IdRespuestaCorrecta == IdRespuesta)
            {
                esCorrecto = true;//sumar puntos
            }
            return esCorrecto;
        }

    }
}