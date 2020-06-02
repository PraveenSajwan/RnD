using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VerifyAdharApi.Models;
using VerifyAdharApi.Services;

namespace VerifyAdharApi.Controllers
{
    [Route("api/migrants")]
    [ApiController]
    public class MigrantController : ControllerBase
    {
        private readonly MigrantService _migrantService;

        public MigrantController(MigrantService migrantService)
        {
            _migrantService = migrantService;
        }

        [HttpGet]
        public ActionResult<List<Migrant>> Get() =>
            _migrantService.Get();

        [Route("{id}")]
        [HttpGet("{id:length(24)}", Name = "GetMigrant")]
        public ActionResult<Migrant> Get(string id)
        {
            var migrant = _migrantService.Get(id);

            if (migrant == null)
            {
                return NotFound();
            }

            return migrant;
        }

        [HttpPost]
        public ActionResult<Migrant> Create([FromBody] Migrant migrant)
        {
            _migrantService.Create(migrant);

            return CreatedAtRoute("GetMigrant", new { id = migrant.Id.ToString() }, migrant);
        }

        [Route("{id}")]
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Migrant migrantIn)
        {
            var book = _migrantService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _migrantService.Update(id, migrantIn);

            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var migrant = _migrantService.Get(id);

            if (migrant == null)
            {
                return NotFound();
            }

            _migrantService.Remove(migrant.Id);

            return NoContent();
        }
    }
}