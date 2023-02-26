using Microsoft.AspNetCore.Mvc;
using Fictichos.Constructora.Models;
using Fictichos.Constructora.Database;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace Fictichos.Constructora.Controller
{
    [ApiController]
    [Route("people")]
    public class PersonController : ControllerBase
    {
        private List<Person> _repo = new List<Person>();

        [HttpPost("new")]
        public void CreateUser()
        {
            Person testPerson = new("Mauricio", "López Cházaro", null);
            Connector<Person> conn = new("cbs", "people");
            
            conn.Collection.InsertOne(testPerson);
        }
        [HttpGet("{person}")]
        public ActionResult<Person> GetPerson(string first)
        {
            Connector<Person> conn = new("cbs", "people");
            
            IMongoQueryable<Person> list =
                from peeps in conn.Collection.AsQueryable()
                where peeps.Name == first
                select peeps;
            if (list is null) return NotFound();

            return Ok(list);
        }

        [HttpGet("{name}/{last}")]
        public ActionResult<Person> GetByNameLastName(string first, string last)
        {
            Connector<Person> conn = new("cbs", "people");            
            
            IMongoQueryable<Person> list =
                from peeps in conn.Collection.AsQueryable()
                where peeps.Name == first || peeps.LastName == last
                select peeps;
            if (list is null) return NotFound();

            return Ok(list);
        }

        [HttpGet("jobs")]
        public Dictionary<string,int> GetAllPositions()
        {
            Connector<Person> conn = new("cbs", "people");
            List<Job> list = (List<Job>)
                        (from peeps in conn.Collection.AsQueryable()
                        select peeps.Employed);
            Dictionary<string, int> charges = (Dictionary<string, int>)list.GroupBy(jobs => jobs.Name)
                .Select(group => new {
                    Metric = group.First(),
                    Count = group.Count()
                });
            
            return charges;
        }

        [HttpGet("jobs/{id}")]
        public List<Job> GetJobsById(string personId)
        {
            Connector<Person> conn = new("cbs", "people");
            List<Job> job =(List<Job>)
                (from peeps in conn.Collection.AsQueryable()
                where peeps.Id == ObjectId.Parse(personId)
                select peeps.Employed);
            return job;
        }

        [HttpPut("jobs/update")]
        public ActionResult UpdateJob()
        {
            Connector<Person> conn = new("cbs", "people");
            // Need to figure out how to post, fix later.
            ObjectId personId = ObjectId.Parse("63f828a0cc4ca163fba421dc");
            List<Job> newJob = new List<Job>();
            newJob.Add(new Job("Barrendero","Employee","Intendencia"));
            newJob.Add(new Job("Asesor","Admin","Gerencia"));
            // Remove above block when possible.

            var filter = Builders<Person>.Filter.Eq("Id", personId);
            var update = Builders<Person>.Update.Set("Charges", newJob);
            conn.Collection.UpdateOne(filter, update);
            return Ok($"Document with Id:{personId}\thas been updated");
        }

        [HttpGet("address/{personId}")]
        public void AddAddress(string personId)
        {
            ObjectId id = ObjectId.Parse(personId);
            Connector<Person> conn = new("cbs", "people");
            // Need to figure out how to post, fix later.
            List<Address> newAddress = new List<Address>();
            string[] args = {"Nieto", "Sin Número", "Colonia", "11111", "Aguascalientes", "Aguascalientes", "México"};
            newAddress.Add(new Address(args));
            // Remove above block when possible.

            var filter = Builders<Person>.Filter.Eq("Id", personId);
            var update = Builders<Person>.Update.Set("Charges", newAddress);
            conn.Collection.UpdateOne(filter, update);
        }
    }
}
