using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fitichos.Constructora.Repository;
using Fitichos.Constructora.Dto;
using Fitichos.Constructora.Model;

namespace Fitichos.Constructora.Controllers
{
    [ApiController]
    [Route("u")]
    public class UserController : ControllerBase
    {
        private readonly Repository<User, NewUserDto> _repo;
        private readonly IMongoCollection<User> _col;
        public UserController(IMongoClient mongoClient)
        {
            _col = mongoClient.GetDatabase("cbs").GetCollection<User>("users");
            _repo = new(mongoClient);
        }

        [HttpPost("new")]
        public async Task<ActionResult<User>> CreateNewUser(NewUserDto newUser)
        {
            User toInsert = new(newUser);
            await _repo.CreateAsync(toInsert);
            return Ok(toInsert);
        }

        [HttpPost("login")]
        public ActionResult<LoginSuccessDto> Login(LoginDto user)
        {
            User? exists =
                (from u in _col.AsQueryable()
                where u.Name == user.Name
                select u).SingleOrDefault();
            if (exists is null) return NotFound();

            if (!exists.ValidatePassword(user.Password)) return StatusCode(400);
            LoginSuccessDto success = new(exists);
            return Ok(success);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UserChangesDto changes)
        {
            ObjectId _id = new(changes.Id);
            User? exists =
                (from u in _col.AsQueryable()
                where u.Id == _id
                select u).SingleOrDefault();
            if (exists is null) return NotFound();

            exists.Change(changes);

            await _repo.UpdateAsync(exists);
            return Ok();
        }

        [HttpPut("activation")]
        public async Task<ActionResult> Activate(string id)
        {
            User? exists = _repo.GetById(id);
            if (exists is null) return NotFound();

            exists.Active = !exists.Active;
            await _repo.UpdateAsync(exists);
            return Ok();
        }

        [HttpPut("email")]
        public ActionResult ChangeEmail(UserEmailDto mail)
        {
            User? usr = _repo.GetById(mail.Id);
            if (usr is null) return NotFound();

            if (mail.method is true)
            {
                if (usr.Email.Contains(mail.Email)) return StatusCode(400);
                usr.Email.Add(mail.Email);
                return Ok();
            }
            if (!usr.Email.Contains(mail.Email)) return NotFound();
            usr.Email.Remove(mail.Email);
            return Ok();
        }
    }
}