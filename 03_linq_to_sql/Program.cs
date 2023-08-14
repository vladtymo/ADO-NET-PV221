using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_linq_to_sql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            ShopDbContext db = new ShopDbContext();

            var all = db.Products;

            var query = db.Products.Where(x => x.Price < 500).OrderByDescending(x => x.Price);

            foreach (var item in query)
            {
                Console.WriteLine($"[{item.Id}] {item.Name} - {item.Price}$");
            }

            // find element by ...
            var prod = db.Products.FirstOrDefault(x => x.Id == 10);

            if (prod != null ) 
                Console.WriteLine(prod.Name + " " + prod.Producer);
            else
                Console.WriteLine("Not Found!");

            // update data
            prod.Price += 35;

            db.Clients.InsertOnSubmit(new Client()
            {
                FullName = "John Bussh",
                Email = "mixer2@gmail.com",
                Gender = "M",
                PercentSale = 5,
                Phone = "44-34-22",
                Subscribe = true
            });

            db.Salles.DeleteOnSubmit(db.Salles.First());

            // submit changes to database (INSERT, UPDATE, DELETE)
            db.SubmitChanges();
        }
    }
}
