using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using babytrackerservice.Models;
using babytrackerservice.Repositories;

namespace babytrackerservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangingController : ControllerBase
    {
        private readonly ChangingRepository changingRepository;

        public ChangingController()
        {
            changingRepository
                = new ChangingRepository();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Changing> Get()
        {
            return changingRepository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Changing Get(int id)
        {
            return changingRepository.GetByID(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Changing poop)
        {
            if (ModelState.IsValid)
                changingRepository.Add(poop);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Changing poop)
        {
            poop.poop_id = id;
            if (ModelState.IsValid)
                changingRepository.Update(poop);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            changingRepository.Delete(id);
        }
    }
}