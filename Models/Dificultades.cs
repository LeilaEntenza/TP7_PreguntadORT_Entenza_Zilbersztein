namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    public class Dificultades
    {
        public int IdDificultad { get; set; }
        public string Nombre { get; set; }
        public Dificultades()
        {
            
        }
        
        public Dificultades(int idDificultad, string nombre)
        {
            IdDificultad = idDificultad;
            Nombre = nombre;
        }
    }
}