using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProvisionMeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Get();
        }

        public static void Get()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/WebApi/ProvisionMe/");
                //client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                var response = client.GetAsync("api/provision");

                response.Wait();

                //Console.WriteLine(response.StatusCode);

                if (response.Result.IsSuccessStatusCode)
                {
                    var responseContent = response.Result.Content.ReadAsStringAsync();
                    responseContent.Wait();

                    Console.WriteLine(responseContent.Result);
                    //Product product = await response.Content.ReadAsAsync<Product>();
                    // Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
                }

            }
        }
    }
}
