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

        private IMongoQueryable<ILinqSearchable> GetList(string usr)
        {
            Connector conn = new Connector();
            return conn.Query("cbs", "users", "Username", usr);
        }
        
        [HttpGet("{usr}/{pwd}")]
        public ActionResult<bool> ValidateUser(string usr, string pwd)
        {
            IMongoQueryable<ILinqSearchable> chosen = GetList(usr);
            var j = chosen.ToJson();
            var model = j.ToList();
            var executionModel = ((IMongoQueryable<User>) model).GetExecutionModel();
            var queryString = executionModel.ToString();
            return true;
        }
    }
}