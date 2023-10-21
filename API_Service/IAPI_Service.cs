using Microsoft.AspNetCore.Mvc;

namespace MVC_Productos.API_Service
{
    public interface IAPI_Service<T>
    {
        //método GET                (index)
        public Task<IActionResult> Get();

        //método GET {id}           (details)
        public Task<IActionResult> Get(int id); 

        //método POST {T}           (create)
        public Task<IActionResult> Post(T entity);

        //método POST {id}          (edit)
        public Task<IActionResult> Post(int id);

        //método DELETE {id}        (delete)
        public Task<IActionResult> Delete(int id);

    }
}
