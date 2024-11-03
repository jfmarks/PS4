using Microsoft.AspNetCore.Mvc;
using WebRestEF.EF.Models;

namespace WebRest.Interfaces
{
    public interface iController<T>
    {
        Task<IActionResult> Delete(string id);
        Task<ActionResult<T>> Get(string id);
        Task<ActionResult<IEnumerable<T>>> Get();
        Task<ActionResult<T>> Post(T _item);
        Task<IActionResult> Put(string id, T _item);
    }
}