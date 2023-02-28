using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization;


namespace ReceiptScannerMCA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PrintReceipt();

        }
        static void PrintReceipt()
        {
            decimal totalPriceDomestic = 0;
            decimal totalPriceImport = 0;
            int totalCountDomestic = 0;
            int totalCountImport = 0;

            string json = new WebClient().DownloadString("https://interview-task-api.mca.dev/qr-scanner-codes/alpha-qr-gFpwhsQ8fkY1");
            //Console.WriteLine(json); //Printing the JSON file

            var products = JsonConvert.DeserializeObject<List<Product>>(json);
            foreach (var product in products)
            {
                if (product.Domestic)
                { 
                    Console.WriteLine(". Domestic");
                    totalPriceDomestic += product.Price;
                    totalCountDomestic++;
                }
                else
                {
                    Console.WriteLine(". Imported");
                    totalPriceImport += product.Price;
                    totalCountImport++;
                }
                
                Console.WriteLine("... " + product.Name);
                Console.WriteLine($"    Price: ${product.Price}");
                Console.WriteLine("    " + product.Description[..10] + "...");

                if (product.Weight == 0)
                {
                    Console.WriteLine($"    Weight: N/A");
                }
                else
                {
                    Console.WriteLine($"    Weight: {product.Weight}g");
                }
            }

            Console.WriteLine("Domestic cost: " + "$" + totalPriceDomestic);
            Console.WriteLine("Imported cost: " + "$" + totalPriceImport);
            Console.WriteLine("Domestic count: " + totalCountDomestic);
            Console.WriteLine("Imported count: " + totalCountImport);

            Console.Read();
        }
        

        [DataContract]
        class Product
        {
            [DataMember]
            internal string Name { get; set; }
            [DataMember]
            internal bool Domestic { get; set; }
            [DataMember]
            internal decimal Price { get; set; }
            [DataMember]
            internal int Weight { get; set; }
            [DataMember]
            internal string Description { get; set; }
        }
    }
}