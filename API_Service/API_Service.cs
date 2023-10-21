using Ejemplo1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace MVC_Productos.API_Service
{
    public class API_Service<T> : Controller, IAPI_Service<T>
    {
        //Atributos
        static HttpClient client = new HttpClient();
        private string _url = "http://localhost:5129/api/Producto";
        
        //Propiedades
        public string URL { get { return _url; } set {  _url = value; } }
        
        //Constructor
        public API_Service() { }

        //Métodos
        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:5129/api/Producto");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<IActionResult> Get()
        {
            List<T>? lista = null;
            HttpResponseMessage response = await client.GetAsync(_url);
            if (response.IsSuccessStatusCode)
            {
                lista = await response.Content.ReadAsAsync<List<T>>();
            }
            return View(lista);
        }

        public async Task<IActionResult> Get(int id)
        {
            T? entity;
            HttpResponseMessage response = await client.GetAsync(_url + id);
            if (response.IsSuccessStatusCode)
            {
                entity = await response.Content.ReadAsAsync<T>();
                return View(entity);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Post(T entity)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(_url, entity);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Post(int id)
        {
            T? entity;
            HttpResponseMessage response = await client.GetAsync(_url+ id);
            if (response.IsSuccessStatusCode)
            {
                entity = await response.Content.ReadAsAsync<T>();
                return View(entity);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(_url + id);
            return RedirectToAction("Index");
        }

    }
}
