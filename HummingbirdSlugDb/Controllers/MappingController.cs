using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HummingbirdSlugDb.Models;

namespace HummingbirdSlugDb.Controllers
{
    public class MappingController : ApiController
    {
        public IHttpActionResult GetMapping(int id)
        {
            Mapping m = null;
            using (var dal = new Dal())
            {
                var mapping = dal.RetrieveMappings(id).ToList();
                if (!mapping.Any()) return BadRequest("No mappings with that id");

                m = mapping.First();
            }
            if(m != null)
                return Ok(m);
            return BadRequest("Something went wrong retrieving mapping from database");
        }

        [Route("api/mapping/bulk/{ids}")]
        public Mapping[] GetBulk(string ids)
        {
            using (var dal = new Dal())
            {
                var tvDbIds = ids.Split(',').Select(x => x.Trim()).Select(int.Parse).ToArray();
                return dal.RetrieveMappings(tvDbIds).ToArray();
            }
        }

        public Mapping[] GetMappings()
        {
            using (var dal = new Dal())
            {
                return dal.RetrieveMappings().ToArray();
            }
        }

        public void PostMapping(Mapping m)
        {
            using (var dal = new Dal())
            {
                if (dal.RetrieveMappings(m.TvDBId).Any()) return;
                dal.InsertMapping(m);
            }    
        }
    }
}
