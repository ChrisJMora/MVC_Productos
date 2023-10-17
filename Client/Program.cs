#region snippet_all
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientSample
{
    #region snippet_prod
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
    }
    #endregion

    class Program
    {
        #region snippet_HttpClient
        static HttpClient client = new HttpClient();
        #endregion

        static void ShowProduct(Producto product)
        {
            Console.WriteLine($"Name: {product.Nombre}\tPrice: " +
                $"{product.Cantidad}\tCategory: {product.Descripcion}");
        }

        #region snippet_CreateProductAsync
        static async Task<Uri> CreateProductAsync(Producto product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/products", product);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        #endregion

        #region snippet_GetProductAsync
        static async Task<Producto> GetProductAsync(string path)
        {
            Producto product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Producto>();
            }
            return product;
        }
        #endregion

        #region snippet_UpdateProductAsync
        static async Task<Producto> UpdateProductAsync(Producto product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/products/{product.IdProducto}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<Producto>();
            return product;
        }
        #endregion

        #region snippet_DeleteProductAsync
        static async Task<HttpStatusCode> DeleteProductAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }
        #endregion

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        #region snippet_run
        #region snippet5
        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:5129/api");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            #endregion

            try
            {
                // Create a new product
                Producto product = new Producto
                {
                    Nombre = "Gizmo",
                    Cantidad = 100,
                    Descripcion = "Widgets"
                };

                var url = await CreateProductAsync(product);
                Console.WriteLine($"Created at {url}");

                // Get the product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Update the product
                Console.WriteLine("Updating price...");
                product.Cantidad = 80;
                await UpdateProductAsync(product);

                // Get the updated product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Delete the product
                var statusCode = await DeleteProductAsync(product.IdProducto);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
        #endregion
    }
}
#endregion