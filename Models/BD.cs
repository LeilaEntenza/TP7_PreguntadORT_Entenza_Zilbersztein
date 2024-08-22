using System.Data.SqlClient;
using Dapper;
namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    public static class BD
    {
        private static string _connectionString { get; set; } = @"Server=A-PHZ2-CIDI-19;DataBase=PreguntadORT;Trusted_Connection=true;";

        public static List<Categorias> ObtenerCategorias()
        {
            List<Categorias> categorias = new List<Categorias>();
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "select * from Categorias";
                db.Query<Categorias>(sql).ToList();
            }
            return categorias;
        }
        public static List<Dificultades> ObtenerDificultades()
        {
            List<Dificultades> dificultades = new List<Dificultades>();
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "select * from Dificultades";
                db.Query<Dificultades>(sql).ToList();
            }
            return dificultades;
        }
        public static List<Preguntas> ObtenerPreguntas(int dificultad, int categoria)
        {
            List<Preguntas> preguntas = new List<Preguntas>();
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "select * from Preguntas p inner join Categorias c on p.IdCategoria = c.IdCategoria inner join Dificultades d on p.IdDificultad = d.IdDificultad  where IdDificultad = @pdificultad and IdCategoria = @pcategoria";
                db.Query<Preguntas>(sql).ToList();
            }
            return preguntas;
        }
    }
}