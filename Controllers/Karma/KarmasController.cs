using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AndreaDipreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KarmasController : ControllerBase
    {
        private string _connectionString;

        public KarmasController(IConfiguration config) 
        {
            _connectionString = config.GetConnectionString("KarmaContext");
        }

        // GET api/karmas
        [HttpGet]
        public ActionResult<IEnumerable<Karma>> Get()
        {
            using (var db = new KarmaDatabaseContext(_connectionString))
            {
                return Ok(db.Karmas.Select(k => k).ToList());
            }
        }

        // GET api/karmas/%23%2fr%2fitaly/amore -> /api/karmas/#/r/italy/amore
        [HttpGet("{channelName}/{karmaName}")]
        public ActionResult<Karma> Get(string channelName,string karmaName)
        {
            var decodedChannelName = WebUtility.UrlDecode(channelName);
            
            using (var db = new KarmaDatabaseContext(_connectionString))
            {
                var karma = db.Karmas.Where(k => k.Name == karmaName).FirstOrDefault();
                if (karma != null)
                {
                    return Ok(karma);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // POST api/karmas
        [HttpPost]
        public ActionResult<int> Post([FromBody] KarmaRequest value)
        {
            using (var db = new KarmaDatabaseContext(_connectionString))
            {
                var karma = db.Karmas.Where(k => k.Name == value.Karma.Name).FirstOrDefault();
                if (karma != null)
                {
                    if (value.Action == KarmaAction.Increment)
                    {
                        karma.Score = karma.Score + 1;
                    }
                    else
                    {
                        karma.Score = karma.Score - 1;
                    }

                    db.SaveChanges();
                    return Ok(karma);
                }
                else
                {
                    var newKarma = new Karma { Name = value.Karma.Name };
                    if (value.Action == KarmaAction.Increment)
                    {
                        newKarma.Score = 1;
                    }
                    else
                    {
                        newKarma.Score = -1;
                    }

                    db.Karmas.Add(newKarma);
                    db.SaveChanges();
                    return Ok(newKarma);
                }
            }
        }
    }
}
