using System;
using BaltaDataAccess.Model;
using Dapper;
using Microsoft.Data.SqlClient;


namespace BaltaDataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$";

            // var connection = new SqlConnection();
            // connection.Open();

            // connection.Close();
            // connection.Dispose();


            using (var connection = new SqlConnection(connectionString))
            {
                //Sem dapper
                //  Console.WriteLine("Conectado");
                //  connection.Open();

                //  using (var command = new SqlCommand()) //criando comandos sql
                //  {
                //      command.Connection = connection;
                //      command.CommandType = System.Data.CommandType.Text;
                //      command.CommandText = "SELECT [Id], [Title] FROM [Category]";

                //      var reader = command.ExecuteReader();
                //      while (reader.Read())
                //      {
                //          Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
                //      }

                // }
                //----------------------------------------------------------------------

                ListCategories(connection);
                //CreateCategory(connection);
                //UpdateCategory(connection);
                //DeleteCategory(connection);




            }

        }

        static void ListCategories(SqlConnection connection)
        {//Com dapper
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        static void CreateCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "Aws Cloud";
            category.Featured = false;

            //SQL Injection

            var insertSql = @"INSERT INTO [Category] 
            VALUES (
                @Id, 
                @Title, 
                @Url, 
                @Summary, 
                @Order, 
                @Description, 
                @Featured)";

            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });

            Console.WriteLine($"{rows} linhas inseridas");
        }

        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
                title = "Frontend 2021"
            });
            Console.WriteLine($"{rows} registros atualizados");

        }

        static void DeleteCategory(SqlConnection connection)
        {
            var deleteCategory = "DELETE [Category] WHERE [Id]=@id";
            var rows = connection.Execute(deleteCategory, new
            {
                id = new Guid("4e2925c0-3755-430f-b998-ed1156e60e82")
            });
            Console.WriteLine($"{rows} registros excluídos");
        }
    }

}
