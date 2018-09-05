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
        private readonly KarmaDatabaseContext _context;

        public KarmasController(KarmaDatabaseContext context)
        {
            _context = context;
        }
        // GET api/karmas/abc123
        [HttpGet("{channelId}")]
        public ActionResult<IEnumerable<Karma>> Get(string channelId)
        {
            using (var db = _context)
            {
                return Ok(
                    db.Karmas
                    .Where(k => k.ChannelId.ToString() == channelId)
                    .ToList());
            }
        }

        // GET api/karmas/%23%2fr%2fitaly/amore -> /api/karmas/#/r/italy/amore
        [HttpGet("{channelId}/{karmaName}")]
        public ActionResult<Karma> Get(string channelId,string karmaName)
        {
            using (var db = _context)
            {
                var karma = 
                    db.Karmas
                    .Where(k => k.Name == karmaName && k.ChannelId.ToString() == channelId)
                    .FirstOrDefault();
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
            using (var db = _context)
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
