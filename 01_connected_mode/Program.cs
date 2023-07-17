using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace _01_connected_mode
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public string Producer { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Price}$ - {Quantity} items";
        }
    }

    class SportShopManager
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

            reader.Close();
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
            SqlDataReader reader = command.ExecuteReader();
            ShowTable(reader);
        }
        public void ShowAllSalles()
        {
            string cmdText = @"select * from Salles";

            SqlCommand command = new SqlCommand(cmdText, connection);

            // ExecuteReader - виконує команду select та повертає результат у вигляді DbDataReader
            SqlDataReader reader = command.ExecuteReader();
            ShowTable(reader);
        }
       
        public Product? FindProduct(string name)
        {
            string cmd = $"select * from Products where Name = '{name}'";
            SqlCommand command = new(cmd, connection);

            var reader = command.ExecuteReader();

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
                        $"  set Price = {price}" +
                        $"  where Id = {productId}";

            SqlCommand command = new(cmd, connection);
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

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            SportShopManager manager = new();

            manager.ShowAllProducts();
            manager.ShowAllSalles();

            //var item = manager.FindProduct("Ремінь");
            //Console.WriteLine(item);

            //manager.UpdatePrice(3420, 1);
            //manager.AddProduct(new Product()
            //{
            //    Name = "White Kvass",
            //    CostPrice = 90,
            //    Price = 135,
            //    Producer = "Germany",
            //    Quantity = 23,
            //    Type = "Sport Drinks"
            //});
            //manager.Delete(9);

            //manager.ShowAllProducts();

            Console.ReadKey();
        }
    }
}