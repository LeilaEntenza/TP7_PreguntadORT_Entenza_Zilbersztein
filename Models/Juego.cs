namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    static public class Juego
    {
        private static string username { get; set; }
        private static int puntajeActual { get; set; }
        private static int cantidadPreguntasCorrectas { get; set; }
        private static List<int> preguntasUtilizadas { get; set; } = new List<int>();
        private static Categorias categoriaElegida{get;set;} = new Categorias();
        private static Preguntas pregunta = new Preguntas();
        private static List<Respuestas> respuestas = new List<Respuestas>();
        public static int SeccionElegida{get;set;}

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
        public static Preguntas CargarPregunta(string dificultad, string categoria)
        {
            List<Preguntas> preguntas = BD.ObtenerPreguntas(dificultad, categoria);
            Random r = new Random();
            int numeroPregunta = r.Next(1, preguntas.Count);
            return preguntas[numeroPregunta];
            //no se ha utilizado el atributo respuestas
        }
        public static Categorias ObtenerCategoria()
        {
            Random r = new Random();
            int numero = r.Next(1,8);
            categoriaElegida = BD.ObtenerCategoria(numero);
            return categoriaElegida;
        }
        public static Categorias GuardarCategoria(int categoria)
        {
            categoriaElegida = BD.ObtenerCategoria(categoria);
            return categoriaElegida;
        }
        public static int SeleccionarRespuestaCorrecta(int IdPregunta, List<Respuestas> respuestas)
        {
            int contador = 0;
            bool encontrado = false;
            do{
                contador++;
                if (respuestas[contador].Correcta)
                encontrado = true;
            }while(contador <= respuestas.Count && !encontrado);
            return contador;
        }
        public static bool VerificarRespuesta(int IdPregunta, int IdRespuesta)
        {
            bool esCorrecto = false;
            respuestas = BD.ObtenerSiguientesRespuestas(IdPregunta);
            int IdRespuestaCorrecta = SeleccionarRespuestaCorrecta(IdPregunta, respuestas);
            if (IdRespuestaCorrecta == IdRespuesta)
            {
                esCorrecto = true;//sumar puntos
            }
            return esCorrecto;
        }

    }
}