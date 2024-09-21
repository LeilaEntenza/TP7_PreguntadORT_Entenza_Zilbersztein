namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    static public class Juego
    {
        public static string username { get; set; }
        public static int modo{get;private set;}
        private static int puntajeActual { get; set; } = 0;
        public static int racha{get; private set;} = 0;
        public static bool perdio{get; private set;} = false;
        private static int cantidadPreguntasCorrectas { get; set; }
        private static List<int> preguntasUtilizadas { get; set; } = new List<int>();
        public static Categorias categoriaElegida{get;set;} = new Categorias();
        private static Dificultades dificultadElegida{get;set;} = new Dificultades();
        public static Preguntas pregunta = new Preguntas();
        private static List<Respuestas> respuestas = new List<Respuestas>();
        public static int SeccionElegida{get;set;}
        public static string respuestaCorrecta{get;set;}
        public static string texto{get;private set;}
        public static string urlImagen{get;private set;}

        public static void InicializarJuego()
        {
            username = "";
            puntajeActual = 0;
            racha = 0;
            cantidadPreguntasCorrectas = 0;
            respuestaCorrecta = null;
            perdio = false;
        }
        public static List<Categorias> ObtenerCategorias()
        {
            return BD.ObtenerCategorias();
        }
        public static void GuardarUsuario(string usuario)
        {
            username = usuario;
        }
        public static void GuardarModo(int mode)
        {
            modo = mode; 
        }
        public static string TraerUsuario()
        {
            return username;
        }
        public static int TraerPuntaje()
        {
            return puntajeActual;
        }
        public static string TraerFoto()
        {
            return categoriaElegida.Foto;
        }
        public static int TraerRacha()
        {
            return racha;
        }
        public static void setTexto(string ptexto)
        {
            texto = ptexto;
        }
        public static void setUrlImagen(string url)
        {
            urlImagen = url;
        }
        
        public static Preguntas CargarPregunta()
        {
            List<Preguntas> preguntas = new List<Preguntas>();
            preguntas = BD.ObtenerPreguntas(dificultadElegida.IdDificultad, categoriaElegida.IdCategoria);
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
        public static void GuardarDificultad(int dificultad)
        {
            dificultadElegida = BD.ObtenerDificultad(dificultad);
        }
        public static string ObtenerColor()
        {
            string color;
            color = BD.ObtenerColor(categoriaElegida.IdCategoria);
            color = "background-color: " + color;
            return color;
        }
        public static string ObtenerEnunciado()
        {
            return pregunta.Enunciado; 
        }
        public static string SeleccionarRespuestaCorrecta()
        {
            respuestaCorrecta = BD.ObtenerRespuestaCorrecta(pregunta.IdPregunta);
            return respuestaCorrecta;
        }
        public static bool VerificarRespuesta(string respuesta)
        {
            bool esCorrecto = false;
            if (respuesta == respuestaCorrecta)
            {
                esCorrecto = true;
                if (dificultadElegida.IdDificultad == 1)
                puntajeActual+=100;
                else if (dificultadElegida.IdDificultad == 2)
                puntajeActual+=150;
                else
                puntajeActual+=200;     
            }
            else
            puntajeActual = 0;
            return esCorrecto;
        }

    }
}