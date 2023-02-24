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
            Person testPerson = new("Mauricio", "López Cházaro");
            Connector conn = new();
            IMongoCollection<Person> peopleCollection = 
                conn.Client.GetDatabase("cbs").GetCollection<Person>("people");
            
            peopleCollection.InsertOne(testPerson);
        }
        [HttpGet("{person}")]
        public ActionResult<Person> GetPerson(string first)
        {
            Connector conn = new();
            IMongoCollection<Person> peopleCollection = 
                conn.Client.GetDatabase("cbs").GetCollection<Person>("people");
            
            IMongoQueryable<Person> list =
                from peeps in peopleCollection.AsQueryable()
                where peeps.Name == first
                select peeps;
            if (list is null) return NotFound();

            return Ok(list);
        }

        [HttpGet("{name}/{last}")]
        public ActionResult<Person> GetByNameLastName(string first, string last)
        {
            Connector conn = new();
            IMongoCollection<Person> peopleCollection = 
                conn.Client.GetDatabase("cbs").GetCollection<Person>("people");
            
            IMongoQueryable<Person> list =
                from peeps in peopleCollection.AsQueryable()
                where peeps.Name == first || peeps.LastName == last
                select peeps;
            if (list is null) return NotFound();

            return Ok(list);
        }

        [HttpGet("jobs")]
        public Dictionary<string,int> GetAllPositions()
        {
            PersonConnector conn = new();
            List<Job> list = (List<Job>)
                        (from peeps in conn.Collection.AsQueryable()
                        select peeps.Charges);
            Dictionary<string, int> charges = (Dictionary<string, int>)list.GroupBy(jobs => jobs.Name)
                .Select(group => new {
                    Metric = group.First(),
                    Count = group.Count()
                });
            
            return charges;
        }

        [HttpGet("{id}")]
        public List<Job> GetJobsById(ObjectId personId)
        {
            PersonConnector conn = new();
            List<Job> job =(List<Job>)
                (from peeps in conn.Collection.AsQueryable()
                where peeps.Id == personId
                select peeps.Charges);
            return job;
        }

        [HttpPut("update/{id}")]
        public ActionResult UpdateJob(ObjectId personId, Job newJob)
        {
            PersonConnector conn = new();
            var filter = Builders<Person>.Filter.Eq("Id", personId);
            var update = Builders<Person>.Update.Set("Charges", newJob);
            conn.Collection.UpdateOne(filter, update);
            return Ok($"Document with Id:{personId}\thas been updated");
        }
        
        private class PersonConnector : Connector
        {
            public IMongoCollection<Person> Collection { get; init; }
            public PersonConnector() : base()
            {
                Collection = Client.GetDatabase("cbs").GetCollection<Person>("people");
            }
        }
    }
}
