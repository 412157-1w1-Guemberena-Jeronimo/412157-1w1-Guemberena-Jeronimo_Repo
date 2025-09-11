using Ejercicio_1._5__Comercio_.Domain;
using Ejercicio_1._5__Comercio_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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
        public IActionResult GetBudgets()//Funciona pero no devuelve detalle
        {
            try
            {
                var Budgets = _service.GetAllBudgets();
                
                foreach (Budget b in Budgets)
                {
                    
                    var details = b.GetDetails();
                    if (details.Count > 0)
                    {
                        foreach (var d in details)
                        {
                            b.AddDetail(d);
                            return Ok(b);
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                    
                }
                throw new Exception("No se encontraron facturas con ese id");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al acceder a datos" });
            }
        }

        // GET api/<BudgetsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)//Era nomas que estaba buscando una factura que no existia, igual me di cuenta que no estoy devolviendo el detalle
        {
            try
            {
                var budgetById = _service.GetBudgetById(id);
                if (budgetById != null)
                {
                    return Ok(budgetById);
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Presupuesto no encontrado" });
            }
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
        }

        // DELETE api/<BudgetsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
