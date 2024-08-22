namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    public class Respuestas
    {
        private int IdRespuesta { get; set; }
        private int IdPregunta { get; set; }
        private int Opcion { get; set; }
        private string Contenido { get; set; }
        public bool Correcta { get; set; }
        private string Foto { get; set; }
    }
}