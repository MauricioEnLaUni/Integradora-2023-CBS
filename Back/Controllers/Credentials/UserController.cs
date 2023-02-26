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
    public class UserController : ControllerBase, IActionable<UserInfoDTO, NewUserDTO, UpdatedUserDTO>
    {
        private List<User> _repo = new List<User>();
        private Connector<User> Connection = new(0, "people");

        /// <summary>
        /// Populates controller list from Database.
        /// </summary>
        /// <returns>List of all usernames.</returns>
        private IMongoQueryable<User> GetAll()
        {
            IMongoQueryable<User> results = 
                from user in Connection.Collection.AsQueryable()
                select user;
            return results;
        }

        /// <summary>
        /// Checks if the user is in the Data
        /// </summary>
        /// <returns>User if exists, null otherwise</returns>
        private User? FindUser(IMongoQueryable<User> users, UserLoginDTO usr)
        {
            User? contained =
                (from u in users
                where u.Name == usr.Username
                select u).First();

            return contained;
        }

        [HttpPost("login")]
        public ActionResult<User> ValidateUser(UserLoginDTO usr)
        {
            IMongoQueryable<User> users = GetAll();
            if(!users.Any()) return StatusCode(500);

            User? FoundUser = FindUser(users, usr);
            if(FoundUser is null) return StatusCode(404);

            if(!FoundUser.Active) return StatusCode(403);
            if(!FoundUser.ValidatePassword(usr.Password)) return StatusCode(400);

            UserInfoDTO user = FoundUser.AsInfoDTO();
            return Ok(user);
        }

        [HttpPost("new")]
        public ActionResult<UserInfoDTO> Insert(NewUserDTO newUser)
        {
            User userToAdd = new(newUser);
            
            try
            {
                Connection.Collection.InsertOne(userToAdd);
                return CreatedAtAction(nameof(ValidateUser), userToAdd.AsLoginDTO(), userToAdd.AsInfoDTO());
            } catch(Exception)
            {
                Console.WriteLine("An error has occurred while processing your request!");
                return StatusCode(500);
            }
        }

        [HttpPut]
        public ActionResult<UserInfoDTO> Update(UpdatedUserDTO newData)
        {
            return new UserInfoDTO();
        }

        [HttpDelete]
        public ActionResult<UserInfoDTO> Delete(ObjectId id)
        {
            return new UserInfoDTO();
        }
    }
}