using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fictichos.Constructora.DTOs;
using Fictichos.Constructora.Models;
using Fictichos.Constructora.Database;

namespace Fictichos.Constructora.Controller
{
    [ApiController]
    [Route("person")]
    public class PersonController : ControllerBase, IActionable<PersonInfoDTO, NewPersonDTO, UpdatedPersonDTO>
    {
        private Connector<Person> _conn = new(0, "people");

        private List<Person> SelectAll()
        {
            return
                (from p in _conn.Collection.AsQueryable()
                select p).ToList();
        }

        private Person? SelectById(string id)
        {
            ObjectId _id = new ObjectId(id);
            List<Person> people = SelectAll();

            return
                (from p in people
                where p.Id == _id
                select p).SingleOrDefault<Person>();
        }

        [HttpGet]
        public ActionResult<List<PersonInfoDTO>> GetAll()
        {
            List<Person> list = SelectAll();
            List<PersonInfoDTO> res = list.Select(p => p.AsDTO()).ToList();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public ActionResult<PersonInfoDTO> GetById(string id)
        {
            Person? found = SelectById(id);
            if (found is null) return NotFound();

            return Ok(found.AsDTO());
        }

        [HttpPost]
        public ActionResult<PersonInfoDTO> Insert(NewPersonDTO newData)
        {
            Person person = new(newData);

            try
            {
                _conn.Collection.InsertOne(person);
                return CreatedAtAction(nameof(GetById), new { id = person.Id.ToString() }, person.AsDTO());
            } catch(Exception)
            {
                Console.WriteLine("An error has occurred while processing your request!");
                return StatusCode(500);
            }
        }

        [HttpPut]
        public ActionResult<PersonInfoDTO> Update(UpdatedPersonDTO newData)
        {
            if (newData.Name is null && newData.LastName is null &&
                newData.Employed is null && newData.Contacts is null)
            {
                return StatusCode(400);
            }

            Person? personToUpdate = SelectById(newData.Id);
            if (personToUpdate is null) return NotFound();

            var filter = Builders<Person>.Filter.Eq("Id", personToUpdate.Id);
            var update = Builders<Person>.Update
                .Set(p => p.Name, newData.Name is not null ? newData.Name : personToUpdate.Name)
                .Set(p => p.LastName, newData.LastName is not null ? newData.LastName : personToUpdate.LastName);
            _conn.Collection.UpdateOne(filter, update);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult<PersonInfoDTO> Delete(string id)
        {
            Person? personToDelete = SelectById(id);
            if (personToDelete is null) return NotFound();

            var filter = Builders<Person>.Filter.Eq("Id", personToDelete);
            _conn.Collection.DeleteOne(filter);

            return NoContent();
        }

        #region Contact Controller
        
        #endregion
    }
}