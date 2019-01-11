using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HeadlessAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IRepository _repo;
        public ValuesController(IRepository repository) => _repo = repository;

        // GET api/values
        [HttpGet]
        public ActionResult<ICollection<ContentType>> Get() => new ActionResult<ICollection<ContentType>>(_repo.GetAllContentTypes());

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ContentType> Get(int id) => _repo.GetContentType(id);

        // POST api/values
        [HttpPost]
        public void Post([FromBody] ContentType value) => _repo.UpdateContentType(value);

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ContentType value) => _repo.CreateContentType(value);

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _repo.DeleteContentType(id);
    }
}
