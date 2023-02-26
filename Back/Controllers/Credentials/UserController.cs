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
        private List<User> GetAll()
        {
            List<User> results = (List<User>)
                (from user in Connection.Collection.AsQueryable()
                select user);
            return results;
        }

        private User? FindUser(List<User> users, UserLoginDTO usr)
        {
            User? contained = (User)
                (from u in users.AsQueryable()
                where u.Name == usr.Username &&
                u.ValidatePassword(usr.Password)
                select u);
            return contained;
        }

        [HttpPost("login")]
        public ActionResult<User> ValidateUser(UserLoginDTO usr)
        {
            List<User> users = GetAll();
            if(!users.Any()) return StatusCode(500);

            User? UserCredentials = FindUser(users, usr);
            if(UserCredentials is null) return StatusCode(403);

            UserInfoDTO user = UserCredentials.AsInfoDTO();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserInfoDTO> Insert(NewUserDTO newUser)
        {
            User userToAdd = new(newUser);

            Connector<User> conn = new(0, "people");
            try
            {
                conn.Collection.InsertOne(userToAdd);
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