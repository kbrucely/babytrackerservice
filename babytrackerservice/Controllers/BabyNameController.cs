using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babytrackerservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using babytrackerservice.Repositories;

namespace babytrackerservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BabyNameController : ControllerBase
    {
        private readonly BabyRepository babyRepository;
        public BabyNameController()
        {
            babyRepository = new BabyRepository();
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<BabyName> Get()
        {
            return babyRepository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public BabyName Get(int id)
        {
            return babyRepository.GetByID(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]BabyName baby)
        {
            if (ModelState.IsValid)
                babyRepository.Add(baby);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]BabyName baby)
        {
            baby.baby_id = id;
            if (ModelState.IsValid)
                babyRepository.Update(baby);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            babyRepository.Delete(id);
        }
    }
}