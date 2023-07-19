using data_access;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace _01_connected_mode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            SportShopManager manager = new();

            manager.ShowAllProducts();
            manager.ShowAllSalles();

            string name = Console.ReadLine();
            var item = manager.FindProduct(name);
            Console.WriteLine(item);

            manager.UpdatePrice(3420, 1);
            manager.AddProduct(new Product()
            {
                Name = "White Kvass",
                CostPrice = 90,
                Price = 135,
                Producer = "Germany",
                Quantity = 23,
                Type = "Sport Drinks"
            });
            manager.Delete(9);

            manager.ShowAllProducts();

            Console.ReadKey();
        }
    }
}