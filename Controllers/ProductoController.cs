using Microsoft.AspNetCore.Mvc;
using Ejemplo1.Models;
using MVC_Productos.API_Service;

namespace Ejemplo1.Controllers
{
    public class ProductoController : Controller
    {
        private API_Service<Producto> _servicioAPI = API_Service<Producto>.Instacia();
        
        // GET: ProductoController
        public async Task<IActionResult> Index()
        {
            List<Producto>? lista = await _servicioAPI.Get();
            if (lista != null)
            {
                return View(lista);
            }
            return View("Error");
        }

        // GET: ProductoController/Details
        public async Task<IActionResult> Details(int IdProducto)
        {
            Producto? producto = await _servicioAPI.Get(IdProducto);
            if (producto != null)
            {
                return View(producto);
            }
            return View("Error");
        }

        // GET: ProductoController/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (await _servicioAPI.Post(producto))
            {
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        // GET: ProductoController/Edit
        public async Task<IActionResult> Edit(int IdProducto)
        {
            Producto? producto = await _servicioAPI.Get(IdProducto);
            if (producto != null)
            {
                return View(producto);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producto producto)
        {
            if (await _servicioAPI.Put(producto.IdProducto, producto))
            {
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        // GET: ProductoController/Delete
        public async Task<IActionResult> Delete(int IdProducto)
        {
            if (await _servicioAPI.Delete(IdProducto))
            {
                return RedirectToAction("Index");
            }
            return View("Error");
        }

    }
}
