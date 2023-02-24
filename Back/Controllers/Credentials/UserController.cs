using Microsoft.AspNetCore.Mvc;
using Fictichos.Constructora.Models;
using Fictichos.Constructora.Database;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fictichos.Credentials.Controller
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private List<User> _repo = new List<User>();

        /// <summary>
        /// Populates controller list from Database.
        /// </summary>
        /// <returns>List of all usernames.</returns>
        private List<string> GetAll()
        {
            Connector conn = new();
            IMongoCollection<string> userCollection =
                conn.Client.GetDatabase("cbs").GetCollection<string>("users");
            IMongoQueryable<string> results =
                from user in userCollection.AsQueryable()
                select user;
            List<string> res = (List<string>)results;
            return res;
        }

        private bool FindUser(List<string> users, string username)
        {
            bool contained = users.Contains(username);
            return contained;
        }

        private User? GetCredentials(string usr, string pwd)
        {
          // Optimize here
            Connector conn = new();
            IMongoCollection<User> userCollection =
              conn.Client.GetDatabase("cbs").GetCollection<User>("users");
            IMongoQueryable<User> results =
                from user in userCollection.AsQueryable()
                where user.Username == usr &&
                user.ValidatePassword(pwd)
                select user;
            return results as User;
        }

        [HttpGet("{usr}")]
        public ActionResult<bool> ValidateUser(string usr, string pwd)
        {
            if (!GetAll().Contains(usr)) return NotFound();
            User? user = GetCredentials(usr, pwd);
            if (user is null) return StatusCode(500);
            return Ok(user);
        }

        [HttpPost("new/")]
        public ActionResult<bool> CreateUser()
        {
            User testUser = new(
                "Moe",
                "$argon2i$v=19$m=32,t=2,p=2$MWUwYm5RVElKUTQzZDRHMg$wxKe3XwvdnHz8pxy7wL9GA",
                "210804@utags.edu.mx"
            );
            Connector conn = new();
            IMongoCollection<User> userCollection =
              conn.Client.GetDatabase("cbs").GetCollection<User>("users");
            userCollection.InsertOne(testUser);
            return Ok("Usuario añadido a la base de datos");
        }
    }
}