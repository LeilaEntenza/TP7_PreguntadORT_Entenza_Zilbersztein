namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    public class Categorias
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Foto { get; set; }
        public string Color { get; set; }

        public Categorias()
        {

        }

        public Categorias(int idCategoria, string nombre, string foto, string color)
        {
            IdCategoria = idCategoria;
            Nombre = nombre;
            Foto = foto;
            Color = color;
        }
    }
}