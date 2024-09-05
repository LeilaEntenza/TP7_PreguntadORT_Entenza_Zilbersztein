namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    static public class Juego
    {
        private static string username { get; set; }
        private static int puntajeActual { get; set; }
        private static int cantidadPreguntasCorrectas { get; set; }
        private static List<int> preguntasUtilizadas { get; set; } = new List<int>();
        private static Categorias categoriaElegida{get;set;} = new Categorias();
        private static Dificultades dificultadElegida{get;set;} = new Dificultades();
        public static Preguntas pregunta = new Preguntas();
        private static List<Respuestas> respuestas = new List<Respuestas>();
        public static int SeccionElegida{get;set;}
        public static int numeroRespuestaCorrecta{get;set;}

        public static void InicializarJuego()
        {
            username = "";
            puntajeActual = 0;
            cantidadPreguntasCorrectas = 0;
            pregunta = null;
            numeroRespuestaCorrecta = 0;
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
        public static Preguntas CargarPregunta()
        {
            dificultadElegida.IdDificultad = 1;
            categoriaElegida.IdCategoria = 1;//para probar
            List<Preguntas> preguntas = BD.ObtenerPreguntas(dificultadElegida.IdDificultad, categoriaElegida.IdCategoria);
            Random r = new Random();
            int numeroPregunta = r.Next(1, preguntas.Count);
            pregunta = preguntas[numeroPregunta-1];
            return pregunta;
        }
        public static List<Respuestas> CargarRespuestas()
        {
            int idPregunta = pregunta.IdPregunta;
            List<Respuestas> opciones = BD.ObtenerRespuestas(idPregunta);
            return opciones;
        }
        public static Categorias GuardarCategoria(int categoria)
        {
            categoriaElegida = BD.ObtenerCategoria(categoria);
            return categoriaElegida;
        }
        public static int SeleccionarRespuestaCorrecta()
        {
            numeroRespuestaCorrecta = BD.ObtenerRespuestaCorrecta(pregunta.IdPregunta);
            return numeroRespuestaCorrecta;
        }
        public static bool VerificarRespuesta(int respuesta)
        {
            bool esCorrecto = false;
            if (respuesta == numeroRespuestaCorrecta)
            esCorrecto = true;           
            return esCorrecto;
        }

    }
}