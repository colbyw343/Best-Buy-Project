using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Net;

namespace InClassSandbox
{
    public class ProductRepository
    {
        public ProductRepository()
        {
#if DEBUG
            string jsonText = File.ReadAllText("appsettings.development.json");
#else
           string jsonText = File.ReadAllText("appsettings.development.json");
#endif
            string connString = JObject.Parse(jsonText)["ConnectionStrings"]["DefaultConnection"].ToString();

            this.connStr = connString;
        }

        private string connStr;

        public List<Product> ShowAllProducts()
        {
            Console.WriteLine("ID  Name     Price  Category ID");
            MySqlConnection thing = new MySqlConnection(connStr);
            List<Product> products = new List<Product>();
            using (thing)
            {
                thing.Open();
                MySqlCommand cmd = thing.CreateCommand();
                cmd.CommandText = "SELECT * FROM products";
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = (int)rdr["ProductID"],
                        Name = rdr["Name"].ToString(),
                        Price = (decimal)rdr["Price"],
                        CategoryId = (int)rdr["CategoryID"]
                    };
                    products.Add(product);
                    Console.WriteLine($"{product.ProductId}.....{product.Name}.....{product.Price}.....{product.CategoryId}");
                }
                
                return products;
            }
        }

        public void CreateProduct(string name, decimal price, int catID)
        {
            MySqlConnection thing = new MySqlConnection(connStr);

            using (thing)
            {
                thing.Open();
                MySqlCommand cmd = thing.CreateCommand();
                cmd.CommandText = "INSERT INTO products (Name, Price, CategoryID) Values (@n , @p , @cID);";
                cmd.Parameters.AddWithValue("n", name);
                cmd.Parameters.AddWithValue("p", price);
                cmd.Parameters.AddWithValue("cID", catID);
                cmd.ExecuteNonQuery();
            }
        }
        
        public void UpdateProduct(Product prod)
        {
            var conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE products SET Name = @n, Price = @p WHERE ProductId = @pID;";
                cmd.Parameters.AddWithValue("n", prod.Name);
                cmd.Parameters.AddWithValue("p", prod.Price);
                cmd.Parameters.AddWithValue("pID", prod.ProductId);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int id)
        {
            var conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE ProductID = @id;";
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
