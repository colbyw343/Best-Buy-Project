using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Linq;
using MySql.Data.MySqlClient;
using System.IO;

namespace InClassSandbox
{ 
    class Program
    {
        
        public static void Main(string[] args)
        {
            BestBuyShowing();
            BestBuyAdding();
            BestBuyUpdating();
            BestBuyDeleting();
        }

        static void BestBuyAdding()
        {
            ProductRepository thing = new ProductRepository();
            Console.WriteLine("What product would you like Best Buy to start selling?");
            string response = Console.ReadLine();

            Console.WriteLine("How much is this item approximately?");
            string response2 = Console.ReadLine();
            decimal price = decimal.Parse(response2);

            Console.WriteLine("What category ID would this item fall under?");
            Console.WriteLine("1) Computers");
            Console.WriteLine("2) Appliances");
            Console.WriteLine("3) Cell Phones");
            Console.WriteLine("4) Headphones/Earbuds");
            Console.WriteLine("5) Tv's and Accessories");
            Console.WriteLine("6) Printer's and Accessories");
            string response3 = Console.ReadLine();
            int catIdResponse = int.Parse(response3);

            var prod3 = new Product()
            {
                Name = response,
                Price = price,
                CategoryId = catIdResponse
            };
            thing.CreateProduct(response, price, catIdResponse);
            Console.WriteLine("Item has been added!");
            Console.ReadLine();
        }

        static void BestBuyShowing()
        {
            
            ProductRepository thing = new ProductRepository();
            Console.WriteLine("These are all the products that this Best Buy carries.");
            thing.ShowAllProducts();
        }

        static void BestBuyUpdating()
        {
            ProductRepository thing = new ProductRepository();

            Console.WriteLine("Is there any product that you would like to update?");
            string response = Console.ReadLine();

            if(response.ToUpper() == "YES")
            {
                Console.WriteLine("Please type the product number of the item you would like to update:");
                thing.ShowAllProducts();
                string response2 = Console.ReadLine();
                int prodResponse = int.Parse(response2);

                var product = new Product()
                {
                    ProductId = prodResponse
                };

                Console.WriteLine("What would you like to update about this item?");
                Console.WriteLine(product.ProductId); Console.Write(product.Name); Console.Write(product.Price); Console.Write(product.CategoryId);
                string response3 = Console.ReadLine();

                if(response3.ToUpper() == "NAME")
                {
                    Console.WriteLine("Please give it a new name:");
                    string nameResponse = Console.ReadLine();
                    nameResponse = product.Name;
                    Console.WriteLine("Name has been changed!");
                }

                if(response3.ToUpper() == "PRICE")
                {
                    Console.WriteLine("Please give it a new price:");
                    string priceResponse = Console.ReadLine();
                    decimal price = decimal.Parse(priceResponse);
                    price = product.Price;
                    Console.WriteLine("Price has been changed!");
                    Console.ReadLine();
                }

                if(response3.ToUpper() == "CATEGORYID")
                {
                    Console.WriteLine("Please give it a new category ID:");
                    string catResponse = Console.ReadLine();
                    int catID = int.Parse(catResponse);
                    catID = product.CategoryId;
                }

                if(response3.ToUpper() == "PRODUCTID")
                {
                    Console.WriteLine("Cannot change the Product ID");
                    Console.ReadLine();
                }
            }
            if(response.ToUpper() == "No")
            {
                return;
            }
        }

        static void BestBuyDeleting()
        {
            ProductRepository thing = new ProductRepository();

            Console.WriteLine("Is there any item you would like Best Buy to stop selling?");
            string deleteResponse = Console.ReadLine();
            if (deleteResponse.ToUpper() == "YES")
            {
                Console.WriteLine("Please input the Product ID number of the product you would like to delete.");
                thing.ShowAllProducts(); 
                string prodResponse = Console.ReadLine();
                int prodResponse1 = int.Parse(prodResponse);

                var product = new Product()
                {
                    ProductId = prodResponse1
                };

                thing.DeleteProduct(prodResponse1);
                Console.WriteLine("Item has been deleted!");
            }
            if(deleteResponse.ToUpper() == "no")
            {
                return;
            }
        }
    }
}
