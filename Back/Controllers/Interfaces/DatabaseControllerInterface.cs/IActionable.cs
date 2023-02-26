using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;

namespace Fictichos.Constructora.Models
{
    public interface IActionable<T, U, V>
    {
        public ActionResult<T> Insert(U newData);
        public ActionResult<T> Update(V newData);
        public ActionResult<T> Delete(string Username);
    }
}