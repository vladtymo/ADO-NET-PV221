using System.Configuration;
using System.Data.SqlClient;

namespace data_access
{
    public class SportShopManager
    {
        private SqlConnection connection = null;

        private void ShowTable(SqlDataReader reader)
        {
            // відображається назви всіх колонок таблиці
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($"{reader.GetName(i),-10}\t");
            }
            Console.WriteLine("\n-------------------------------------------------------------------------------------");

            // відображаємо всі значення кожного рядка
            while (reader.Read()) // go to the next row
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i],-10}\t");
                }
                Console.WriteLine();
            }
        }

        public SportShopManager(string? connectionStr = null)
        {
            connectionStr ??= ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString;

            connection = new SqlConnection(connectionStr);
            connection.Open();
        }

        // --------- public methods (interface)
        public void ShowAllProducts()
        {
            string cmdText = @"select * from Products";

            SqlCommand command = new SqlCommand(cmdText, connection);

            // ExecuteReader - виконує команду select та повертає результат у вигляді DbDataReader
            using var reader = command.ExecuteReader();
            ShowTable(reader);
        }
        public void ShowAllSalles()
        {
            string cmdText = @"select * from Salles";

            SqlCommand command = new SqlCommand(cmdText, connection);

            // ExecuteReader - виконує команду select та повертає результат у вигляді DbDataReader
            using var reader = command.ExecuteReader();
            ShowTable(reader);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            string cmdText = @"select * from Products";
            SqlCommand command = new SqlCommand(cmdText, connection);

            using var reader = command.ExecuteReader();

            //List<Product> items = new();

            while(reader.Read())
            {
                yield return new Product()
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Price = (int)reader["Price"],
                    CostPrice = (int)reader["CostPrice"],
                    Quantity = (int)reader["Quantity"],
                    Producer = (string)reader["Producer"],
                    Type = (string)reader["TypeProduct"],
                };
            }

            //return items;
        }
        public Product? FindProduct(string name)
        {
            // SQL Injection
            // name: '; drop database; --
            string cmd = $"select * from Products where Name = @name";
            SqlCommand command = new(cmd, connection);

            command.Parameters.AddWithValue("@name", name);
            //command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = name;

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return null;

            return new Product()
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Price = (int)reader["Price"],
                CostPrice = (int)reader["CostPrice"],
                Quantity = (int)reader["Quantity"],
                Producer = (string)reader["Producer"],
                Type = (string)reader["TypeProduct"],
            };
        }
        public void UpdatePrice(decimal price, int productId)
        {
            string cmd = $"update Products" +
                        $"  set Price = @price" +
                        $"  where Id = @id";

            SqlCommand command = new(cmd, connection);
            command.Parameters.AddWithValue("@price", price);
            command.Parameters.AddWithValue("@id", productId);

            command.ExecuteNonQuery();
        }
        public void AddProduct(Product product)
        {
            string cmd = $"insert Products values('{product.Name}', '{product.Type}', {product.Quantity}, {product.CostPrice}, '{product.Producer}', {product.Price})";

            SqlCommand command = new(cmd, connection);
            command.ExecuteNonQuery();
        }
        public void Delete(int productId)
        {
            string cmd = $"delete from Products" +
                        $"  where Id = {productId}";

            SqlCommand command = new(cmd, connection);
            command.ExecuteNonQuery();
        }
    }
}
