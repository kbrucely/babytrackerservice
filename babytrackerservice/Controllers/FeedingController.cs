using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babytrackerservice.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using babytrackerservice.Models;

namespace babytrackerservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedingController : ControllerBase
    {
        private readonly FeedingRepository feedingRepository;

        public FeedingController()
        {
            feedingRepository 
                = new FeedingRepository();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Feeding> Get()
        {
            return feedingRepository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Feeding Get(int id)
        {
            return feedingRepository.GetByID(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Feeding feed)
        {
            if (ModelState.IsValid)
                feedingRepository.Add(feed);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Feeding feed)
        {
            feed.feed_id = id;
            if (ModelState.IsValid)
                feedingRepository.Update(feed);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            feedingRepository.Delete(id);
        }
    }
}