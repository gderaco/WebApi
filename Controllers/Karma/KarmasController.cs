using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AndreaDipreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KarmasController : ControllerBase
    {
        private readonly KarmaDatabaseContext _karmaDbContext;

        public KarmasController(KarmaDatabaseContext context)
        {
            _karmaDbContext = context;
        }

        // GET api/karmas
        [HttpGet("{channelId}")]
        public ActionResult<IEnumerable<Karma>> Get(string channelId)
        {
            return Ok(_karmaDbContext.Karmas
                .Where(k => k.ChannelId == channelId)
                .ToList());
        }

        // GET api/karmas/C724506744E47F2B50FED0AE55857449/amore
        [HttpGet("{channelId}/{karmaName}")]
        public ActionResult<Karma> Get(string channelId, string karmaName)
        {
            var retreviedKarma =
                _karmaDbContext.Karmas
                .Where(k => k.Name == HttpUtility.UrlEncode(karmaName))
                .Where(k => k.ChannelId == channelId)
                    .FirstOrDefault();

            if (retreviedKarma != null)
            {
                return Ok(retreviedKarma);
            }
            else
            {
                return Ok();
            }
        }

        // POST api/karmas
        [HttpPost]
        public ActionResult<int> Post([FromBody] KarmaRequest value)
        {
            var karma = _karmaDbContext.Karmas.Where(k => k.Name == value.Karma.Name).FirstOrDefault();
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
                _karmaDbContext.SaveChanges();
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

                _karmaDbContext.Karmas.Add(newKarma);
                _karmaDbContext.SaveChanges();
                return Ok(newKarma);
            }
        }
    }
}
