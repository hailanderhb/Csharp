using System;
using System.Data;
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

                //ListCategories(connection);
                //CreateCategory(connection);
                //UpdateCategory(connection);
                //DeleteCategory(connection);
                //CreateManyCategory(connection);
                //ExecuteProcedure(connection);
                //ExecuteReadProcedure(connection);
                //ExecuteScalar(connection);
                ReadView(connection);


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
                id = new Guid("d11e19e0-745f-417e-8e97-f53e9b370198")
            });
            Console.WriteLine($"{rows} registros excluídos");
        }

        static void CreateManyCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "Aws Cloud";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria Nova";
            category2.Url = "Categoria Nova";
            category2.Description = "Categoria Nova";
            category2.Order = 9;
            category2.Summary = "Categoria Nova";
            category2.Featured = true;

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

            var rows = connection.Execute(insertSql, new[]{
                new{
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                },
                new{
                    category2.Id,
                    category2.Title,
                    category2.Url,
                    category2.Summary,
                    category2.Order,
                    category2.Description,
                    category2.Featured
                }
            });


            Console.WriteLine($"{rows} linhas inseridas");
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var procedure = "[spDeleteStudent]";
            var pars = new { StudentId = "7e4e7f71-5444-473b-83a1-aefaf68f7ea9" };

            var affectedRows = connection.Execute(procedure, pars, commandType: System.Data.CommandType.StoredProcedure);

            Console.WriteLine($"{affectedRows} linhas afetadas");
        }
        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "[spGetCoursesByCategory]";
            var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };

            var courses = connection.Query(procedure, pars, commandType: System.Data.CommandType.StoredProcedure);

            foreach (var item in courses)
            {
                Console.WriteLine(item.Id);
            }
        }

        static void ExecuteScalar(SqlConnection connection)
        {
            var category = new Category();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "Aws Cloud";
            category.Featured = false;

            //SQL Injection

            var insertSql = @"INSERT INTO [Category] 
            OUTPUT inserted.[Id]
            VALUES (
                NEWID(), 
                @Title, 
                @Url, 
                @Summary, 
                @Order, 
                @Description, 
                @Featured)";

            var id = connection.ExecuteScalar<Guid>(insertSql, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });

            Console.WriteLine($"A categoria inserida foi: {id}");
        }

        static void ReadView(SqlConnection connection)
        {
            var sql = "SELECT * FROM [vwCourses]";
            var courses = connection.Query(sql);
            foreach (var item in courses)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }
    }

}
