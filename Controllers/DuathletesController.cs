using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestDuathlon.Managers;
using ZealandDuathlon;

namespace RestDuathlon.Controllers
{
    [Route("api/Duathlete")]
    [ApiController]
    public class DuathletesController : ControllerBase
    {
        private DuathletesManager _manager = new DuathletesManager();

        // GET: api/<DuathletesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Duathlete>> Get()
        {
            IEnumerable<Duathlete> list = _manager.GetAll();
            if (list == null || list.Count() == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(list);
            }
        }

        // GET: api/<DuathletesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{bib}")]
        public ActionResult<Duathlete> Get(int bib)
        {
            Duathlete foundDuathlete = _manager.GetByBib(bib);
            if (foundDuathlete == null)
            {
                return NotFound("No such Duathlete, Bib: " + bib);
            }
            else
            {
                return Ok(foundDuathlete);
            }
        }

        // POST: api/<DuathletesController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Duathlete> Post([FromBody] Duathlete newDuathlete)
        {
            try
            {
                Duathlete createdDuathlete = _manager.Add(newDuathlete);
                return Created("/" + createdDuathlete.Bib, createdDuathlete);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/<DuathletesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{bib}")]
        public ActionResult<Duathlete> Put(int bib, [FromBody] Duathlete updates)
        {
            try
            {
                Duathlete updatedDuathlete = _manager.Update(bib, updates);
                if (updatedDuathlete == null)
                {
                    return NotFound("No such Duathlete, Bib: " + bib);
                }
                return Ok(updatedDuathlete);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/<DuathletesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{bib}")]
        public ActionResult<Duathlete> Delete(int bib)
        {
            Duathlete deletedDuathlete = _manager.Delete(bib);
            if (deletedDuathlete == null)
            {
                return NotFound("No such Duathlete, Bib: " + bib);
            }
            else
            {
                return Ok(deletedDuathlete);
            }
        }
    }

}
