using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Microsoft.AspNetCore.Mvc;

using Fictichos.Constructora.DTOs;
using Fictichos.Constructora.Models;
using Fictichos.Constructora.Database;
using Isopoh.Cryptography.Argon2;

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
            return 
                from user in Connection.Collection.AsQueryable()
                select user;
        }

        /// <summary>
        /// Checks if the user is in the Data
        /// </summary>
        /// <returns>User if exists, null otherwise</returns>
        private User? FindUser(string usr)
        {
            IMongoQueryable<User> list = GetAll();
            return 
                (from u in list
                where u.Name == usr
                select u).SingleOrDefault<User>();
        }

        [HttpPost("login")]
        public ActionResult<User> ValidateUser(UserLoginDTO usr)
        {
            User? FoundUser = FindUser(usr.Username);
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
            if (newData.Password is null && newData.Avatar is null &&
                    newData.Active is null && newData.Email is null)
            {
                return StatusCode(400);
            }

            User? userToUpdate = FindUser(newData.Username);
            if(userToUpdate is null) return NotFound();

            var filter = Builders<User>.Filter.Eq("name", userToUpdate.Name);
            var update = Builders<User>.Update
                .Set(u => u.Password, newData.Password is not null ? Argon2.Hash(newData.Password) : userToUpdate.Password)
                .Set(u => u.Avatar, newData.Avatar is not null ? newData.Avatar : userToUpdate.Avatar)
                .Set(u => u.Active, newData.Active is not null ? newData.Active : userToUpdate.Active)
                .Set(u => u.Email, newData.Email is not null ? newData.Email : userToUpdate.Email);
            Connection.Collection.UpdateOne(filter, update);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult<UserInfoDTO> Delete(string usr)
        {
            User? userToDelete = FindUser(usr);
            if(userToDelete is null) return NotFound();

            var deleteFilter = Builders<User>.Filter.Eq("name", usr);
            Connection.Collection.DeleteOne(deleteFilter);

            return NoContent();
        }
    }
}