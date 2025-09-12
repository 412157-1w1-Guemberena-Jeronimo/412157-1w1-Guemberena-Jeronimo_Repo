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
        public IActionResult GetBudgets()//Funciona pero devuelve las facturas 3 y 5 nomas porque son las unicas que cumplen con todos los joins
        {
            try
            {
                List<Budget> Budgets = _service.GetAllBudgets();


                if (Budgets == null || Budgets.Count == 0)
                    return NotFound("No se encontraron facturas");
                Budgets.Count();
                // Proyectamos cada budget con sus detalles
                var result = Budgets.Select(b => new
                {
                    Budget = b,
                    Details = b.GetDetails()
                }).ToList();

                return Ok(result);
                
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al acceder a datos" });
            }
        }

        // GET api/<BudgetsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)//Funciona pero solo sirve con las facturas 3 y 5 porque son las unicas que cumplen con todos los joins
        {
            try
            {
                var budgetById = _service.GetBudgetById(id);
                var details = budgetById.GetDetails();

                var result = new
                {
                    Budget = budgetById,
                    Details = details
                };
                return Ok(result);
                
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Presupuesto no encontrado" });
            }
        }

        // POST api/<BudgetsController>
        [HttpPost]
        public IActionResult Post([FromBody] Budget value)//Funciona, el problema estaba en el mapeo del budget 
        {
            try
            {
                if(value ==null)
                {
                    return BadRequest("Datos de factura incorrectos");
                }
                var budget = _service.SaveBudget(value);
                

                if(budget)
                {
                    return Ok("Presupuesto guardado correctamente");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar el presupuesto" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<BudgetsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Budget value)
        {
            try
            {
                if(value == null || value.Id != id)
                {
                    return BadRequest("Datos de presupuesto incorrectos");
                }
                var budget = _service.UpdateBudget(value);
                if(budget)
                {
                    return Ok("Presupuesto actualizado correctamente");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar el presupuesto" });
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar el presupuesto" });
            }
        }

        // DELETE api/<BudgetsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
