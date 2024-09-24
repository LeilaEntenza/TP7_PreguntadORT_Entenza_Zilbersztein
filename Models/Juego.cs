namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    static public class Juego
    {
        public static string username { get; set; }
        public static int modo { get; private set; }
        private static int puntajeActual { get; set; } = 0;
        public static int racha { get; private set; } = 0;
        public static bool perdio { get; private set; } = true;
        public static bool categoriaYaElegida { get; private set; }
        private static int cantidadPreguntasCorrectas { get; set; }
        private static List<int> preguntasUtilizadas { get; set; } = new List<int>();
        public static Categorias categoriaElegida { get; set; } = new Categorias();
        private static int numeroAnterior { get; set; }
        private static Dificultades dificultadElegida { get; set; } = new Dificultades();
        public static Preguntas pregunta = new Preguntas();
        private static List<Respuestas> respuestas = new List<Respuestas>();
        public static int SeccionElegida { get; set; }
        public static string respuestaCorrecta { get; set; }
        public static string texto { get; private set; }
        public static string urlImagen { get; private set; }

        public static void InicializarJuego()
        {
            username = "";
            puntajeActual = 0;
            cantidadPreguntasCorrectas = 0;
            respuestaCorrecta = null;
            perdio = true;
            categoriaYaElegida = false;
        }
        public static void RestablecerSegundoModo()
        {
            perdio = false;
        }
        public static void ResetPregunta()
        {
            pregunta = null;
            categoriaYaElegida = false;
            respuestaCorrecta = null;
        }
        public static void ResetPuntaje()
        {
            puntajeActual = 0;
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
            if (modo != mode)
            {
                puntajeActual = 0;
                modo = mode;
            }
            if (modo == 2)
                perdio = false;
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
        public static string TraerDificultad()
        {
            return dificultadElegida.Nombre;
        }
        public static void setUrlImagen(string url)
        {
            urlImagen = url;
        }

        public static Preguntas CargarPregunta()
        {
            int numeroPregunta;
            do
            {
                List<Preguntas> preguntas = new List<Preguntas>();
                preguntas = BD.ObtenerPreguntas(dificultadElegida.IdDificultad, categoriaElegida.IdCategoria);
                Random r = new Random();
                numeroPregunta = r.Next(1, preguntas.Count);
                pregunta = preguntas[numeroPregunta - 1];
            } while (numeroPregunta == numeroAnterior);
            numeroAnterior = numeroPregunta;
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
            categoriaYaElegida = true;
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
        public static List<string> ObtenerImagenesCategorias(List<Categorias> categorias)
        {
            List<string> fotosCategorias = new List<string>();
            for (int i = 0; i < categorias.Count-1; i++)
            {
                fotosCategorias.Add(categorias[i].Foto);
            }
            return fotosCategorias;
        }
        public static string SeleccionarRespuestaCorrecta()
        {
            respuestaCorrecta = BD.ObtenerRespuestaCorrecta(pregunta.IdPregunta);
            return respuestaCorrecta;
        }
        public static bool VerificarRespuesta(string respuesta)
        {
            bool esCorrecto = false;
            categoriaYaElegida = false;
            if (respuesta == respuestaCorrecta)
            {
                if (modo == 2)
                    puntajeActual++;
                else
                {
                    if (dificultadElegida.IdDificultad == 1)
                        puntajeActual += 100;
                    else if (dificultadElegida.IdDificultad == 2)
                        puntajeActual += 150;
                    else
                        puntajeActual += 200;
                }
                esCorrecto = true;
            }
            else
            { perdio = true; if (modo == 1) puntajeActual = 0; }
            return esCorrecto;
        }

    }
}