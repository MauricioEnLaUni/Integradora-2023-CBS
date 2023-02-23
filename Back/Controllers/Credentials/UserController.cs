using Microsoft.AspNetCore.Mvc;
using Fictichos.Constructora.Database;
using Fictichos.Constructora.Utils.Generics;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace Fictichos.Credentials.Controller
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly List<User> repo = new List<User>();

        [HttpGet]
        public ActionResult<IMongoQueryable<ILinqSearchable>> GetList(string usr)
        {
            Connector conn = new Connector();
            return Ok(conn.Query("cbs", "users", "Username", usr));
        }
    }
}