using Microsoft.AspNetCore.Mvc;
using Fictichos.Constructora.Models;
using Fictichos.Constructora.Database;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Fictichos.Credentials.Controller
{
    [ApiController]
    [Route("accounts")]
    public class AccountsController : ControllerBase
    {
        private List<Account> _repo = new List<Account>();
        
        
    }
}