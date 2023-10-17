using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ejemplo1.Utils;
using Ejemplo1.Models;
using System.Net.Http.Headers;
using System.IO;

namespace Ejemplo1.Controllers
{
    public class ProductoController : Controller
    {

        static HttpClient client = new HttpClient();

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:5129/api/Producto");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: ProductoController
        public async Task<IActionResult> Index()
        {
            List<Producto>? productos = null;
            HttpResponseMessage response = await client.GetAsync("http://localhost:5129/api/Producto");
            if (response.IsSuccessStatusCode)
            {
                productos = await response.Content.ReadAsAsync<List<Producto>>();
            }
            return View(productos);
        }

        // GET: ProductoController/Details
        public async Task<IActionResult> Details(int IdProducto)
        {
            Producto? producto = null;
            HttpResponseMessage response = await client.GetAsync("http://localhost:5129/api/Producto/" + IdProducto);
            if (response.IsSuccessStatusCode)
            {
                producto = await response.Content.ReadAsAsync<Producto>();
                return View(producto);
            }
            return RedirectToAction("Index");
        }

        // GET: ProductoController/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:5129/api/Producto", producto);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ProductoController/Edit
        public async Task<IActionResult> Edit(int IdProducto)
        {
            Producto? producto = null;
            HttpResponseMessage response = await client.GetAsync("http://localhost:5129/api/Producto/" + IdProducto);
            if (response.IsSuccessStatusCode)
            {
                producto = await response.Content.ReadAsAsync<Producto>();
                return View(producto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producto producto)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
        $"http://localhost:5129/api/Producto/{producto.IdProducto}", producto);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ProductoController/Delete
        public async Task<IActionResult> Delete(int IdProducto)
        {
            HttpResponseMessage response = await client.DeleteAsync(
        $"http://localhost:5129/api/Producto/{IdProducto}");
            return RedirectToAction("Index");
        }

    }
}
