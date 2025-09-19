using ComercioApiRest.Data;
using ComercioApiRest.Models;
using ComercioApiRest.Services;
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
            _service =service;
        }

        // GET: api/<BudgetsController>
        [HttpGet]
        public IActionResult Get()
        {
            var lst = _service.GetAll();
            if(lst != null && lst.Count > 0)
            {
                return Ok(lst);
            }
            return NotFound("No se encontraron facturas");
        }

        

        // POST api/<BudgetsController>
        [HttpPost]
        public IActionResult Post([FromBody] Factura factura)
        {
            var fact = _service.Save(factura);
            if (fact)
            {
                return Ok("Factura guardada correctamente");
            }
            return BadRequest("No se pudo guardar la factura");
        }

        // PUT api/<BudgetsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Factura value)
        {
            var factura = _service.Save(value);
            if (factura)
            {
                return Ok("Factura actualizada correctamente");
            }
            return BadRequest("No se pudo actualizar la factura");
        }

        // DELETE api/<BudgetsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)//este metodo tira un error debido a que en el repository no tengo la baja logica
                                           //y conflictea con la relacion entre facturas y detallesfacturas :(
        {
            var factura = _service.Delete(id);
            if (factura)
            {
                return Ok("Factura eliminada correctamente");
            }
            return BadRequest("No se pudo eliminar la factura");
        }
    }
}
