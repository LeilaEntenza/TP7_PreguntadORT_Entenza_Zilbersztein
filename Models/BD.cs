using System.Data.SqlClient;
using Dapper;
namespace TP7_PreguntadORT_Entenza_Zilbersztein.Models
{
    public static class BD
    {
        private static string _connectionString { get; set; } = @"Server=A-PHZ2-CIDI-19;DataBase=PreguntadORT;Trusted_Connection=true;";

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
        
        public static string ObtenerRespuestaCorrecta(int idPregunta)
        {
            string respuestaCorrecta;
            Respuestas respuesta = new Respuestas();
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "select top 1 * from Respuestas where IdPregunta = @pidpregunta AND Correcta = 1";
                respuesta = db.QueryFirstOrDefault<Respuestas>(sql, new {pidpregunta = idPregunta});
            }
            return respuesta.Contenido;
        }
        public static string ObtenerColor(int seccionElegida)
        {
            Categorias categoria = new Categorias();
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "select * from Categorias where IdCategoria = @pseccionelegida";
                categoria = db.QueryFirstOrDefault<Categorias>(sql, new {pseccionelegida = seccionElegida});
            }
            return categoria.Color;
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
        public static Dificultades ObtenerDificultad(int idDificultad)
        {
            Dificultades dificultades = new Dificultades();
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "select * from Dificultades where idDificultad = @piddificultad";
                db.QueryFirstOrDefault<Dificultades>(sql, new {piddificultad = idDificultad});
            }
            return dificultades;
        }
        public static List<Preguntas> ObtenerPreguntas(int dificultad, int categoria)
        {
            List<Preguntas> preguntas = new List<Preguntas>();
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql;
                sql = $"select * from Preguntas p inner join Categorias c on p.IdCategoria = c.IdCategoria inner join Dificultades d on p.IdDificultad = d.IdDificultad where d.IdDificultad = @pdificultad AND c.idCategoria = @pcategoria";
                preguntas = db.Query<Preguntas>(sql, new {pdificultad = 1, pcategoria = categoria}).ToList();
            }
            return preguntas;
        }
        public static List<Respuestas> ObtenerRespuestas(int IdPregunta)
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