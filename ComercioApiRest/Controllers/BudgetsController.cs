using Ejercicio_1._5__Comercio_.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComercioApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {

        IBudgetService _service;

        public BudgetsController(IBudgetService service)
        {
            _service = service;
        }
        // GET: api/<BudgetsController>
        [HttpGet]
        public IActionResult GetBudgets()
        {
            try
            {
                return Ok(_service.GetAllBudgets());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al acceder a datos" });
            }
        }

        // GET api/<BudgetsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BudgetsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BudgetsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<BudgetsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
