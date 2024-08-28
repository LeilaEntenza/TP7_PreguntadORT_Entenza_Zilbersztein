namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    public class Categorias
    {
        private int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Foto { get; set; }
        public Categorias(int idCategoria, string nombre, string foto)
        {
            IdCategoria = idCategoria;
            Nombre = nombre;
            Foto = foto;
        }
    }
}