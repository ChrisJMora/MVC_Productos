using Microsoft.AspNetCore.Mvc;

namespace MVC_Productos.API_Service
{
    public class API_Service<T> : Controller, IAPI_Service<T>
    {
        //Atributos
        private static API_Service<T>? _instancia;
        
        static HttpClient client = new HttpClient();

        private string _url = "http://localhost:5129/api/Producto";
        
        //Constructor
        private API_Service() { }

        //Métodos
        public static API_Service<T> Instacia()
        {
            if (_instancia == null)
            {
                _instancia = new API_Service<T>();
            }
            return _instancia;
        }

        public async Task<bool> Post(T entity)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(_url, entity);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<T>?> Get()
        {
            List<T>? lista = null;
            HttpResponseMessage response = await client.GetAsync(_url);
            if (response.IsSuccessStatusCode)
            {
                lista = await response.Content.ReadAsAsync<List<T>>();
            }
            return lista;
        }

        public async Task<T?> Get(int id)
        {
            T? entity;
            HttpResponseMessage response = await client.GetAsync($"{_url}/{id}");
            if (response.IsSuccessStatusCode)
            {
                entity = await response.Content.ReadAsAsync<T>();
                return entity;
            }
            return default;
        }

        public async Task<bool> Put(int id, T entity)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"{_url}/{id}", entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{_url}/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
