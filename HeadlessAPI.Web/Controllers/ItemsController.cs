using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HeadlessAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private IRepository _repo;
        public ItemsController(IRepository repository) => _repo = repository;

        // GET api/items/tablename
        [HttpGet("{tablename}")]
        public ActionResult<ICollection<string>> Get(string tablename) => new ActionResult<ICollection<string>>(_repo.GetAllItems(tablename));

        // GET api/items/tablename/5
        [HttpGet("{tablename}/{id}")]
        public ActionResult<string> Get(string tablename, int id) => _repo.GetItem(tablename, id);

        // POST api/items/tablename
        [HttpPost]
        public void Post(string tablename, [FromBody] string value) => _repo.CreateItem(tablename, value);

        // PUT api/items/tablename/5
        [HttpPut("{tablename}/{id}")]
        public void Put(int id, string tablename, [FromBody] string value) => _repo.UpdateItem(tablename, id, value);

        // DELETE api/items/tablename/5
        [HttpDelete("{tablename}/{id}")]
        public void Delete(int id, string tablename) => _repo.DeleteItem(tablename, id);
    }
}
