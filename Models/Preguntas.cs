namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    public class Preguntas
    {
        public int IdPregunta { get; private set; }
        public int IdCategoria { get; private set; }
        public int IdDificultad {get; private set; }
        private string Enunciado { get; set; }
        private string Foto { get; set; }

    }
}