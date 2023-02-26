using Microsoft.AspNetCore.Mvc;
using Fictichos.Constructora.Models;
using Fictichos.Constructora.Database;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Fictichos.Constructora.DTOs;
using MongoDB.Bson;

namespace Fictichos.Credentials.Controller
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase, IActionable<UserDTO, NewUserDTO, UpdatedUserDTO>
    {
        private List<User> _repo = new List<User>();

        /// <summary>
        /// Populates controller list from Database.
        /// </summary>
        /// <returns>List of all usernames.</returns>
        private List<string> GetAll()
        {
            Connector<User> conn = new("cbs", "people");
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
            Connector<User> conn = new("cbs", "people");
            IMongoCollection<User> userCollection =
              conn.Client.GetDatabase("cbs").GetCollection<User>("users");
            IMongoQueryable<User> results =
                from user in userCollection.AsQueryable()
                where user.Name == usr &&
                user.ValidatePassword(pwd)
                select user;
            return results as User;
        }

        /// <summary>
        /// Creates a new User.
        /// </summary>
        /// <returns>The Created User</returns>

        [HttpGet("{usr}")]
        public ActionResult<bool> ValidateUser(string usr, string pwd)
        {
            if (!GetAll().Contains(usr)) return NotFound();
            User? user = GetCredentials(usr, pwd);
            if (user is null) return StatusCode(500);
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserDTO> Insert(NewUserDTO newUser)
        {
            User userToAdd = new(newUser);

            Connector<User> conn = new("cbs", "people");
            try
            {
                conn.Collection.InsertOne(userToAdd);
                return CreatedAtAction(nameof(GetAll), new { id = userToAdd.Id }, userToAdd.AsDTO());
            } catch(Exception)
            {
                Console.WriteLine("An error has occurred while processing your request!");
                return StatusCode(500);
            }
        }

        public ActionResult<UserDTO> Update(ObjectId id, UpdatedUserDTO newData)
        {
            return new UserDTO();
        }
        public ActionResult<UserDTO> Delete(ObjectId id)
        {
            return new UserDTO();
        }
    }
}