using System.Data.SqlClient;
using Dapper;
namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    public static class BD
    {
        private static string _connectionString { get; set; } = @"Server=DESKTOP-CV8MP7S\SQLEXPRESS;DataBase=PreguntadORT;Trusted_Connection=true;";

        //YA USADAS
        public static Categorias ObtenerCategoria(int numero)
        {
            Categorias categoriaElegida;
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "select top 1 * from Categorias where IdCategoria = @pnumero";
                categoriaElegida = db.QueryFirstOrDefault<Categorias>(sql, new {pnumero = numero});
            }
            return categoriaElegida;
        }
        
        
        //DE LA CONSIGNA
        
        public static List<Categorias> ObtenerCategorias()
        {
            List<Categorias> categorias = new List<Categorias>();
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "select * from Categorias";
                categorias = db.Query<Categorias>(sql).ToList();
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
        public static List<Preguntas> ObtenerPreguntas(string dificultad, string categoria)
        {
            List<Preguntas> preguntas = new List<Preguntas>();
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql;
                sql = $"select * from Preguntas p inner join Categorias c on p.IdCategoria = c.IdCategoria inner join Dificultades d on p.IdDificultad = d.IdDificultad where IdDificultad COLLATE Latin1_General_CI_AI = @pdificultad";
                db.Query<Preguntas>(sql).ToList();
            }
            return preguntas;
        }
        public static List<Respuestas> ObtenerSiguientesRespuestas(int IdPregunta)
        {
            List<Respuestas> respuestasPregunta = new List<Respuestas>();
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * from Respuestas r inner join Preguntas p on r.IdPregunta = p.IdPregunta where r.IdPregunta = @pidpregunta";
                respuestasPregunta = db.Query<Respuestas>(sql, new {pidpregunta = IdPregunta}).ToList();
            }
            return respuestasPregunta;
        }

    }
}