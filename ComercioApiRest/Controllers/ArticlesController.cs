using ComercioApiRest.Models;
using ComercioApiRest.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComercioApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        IArticleService _service;
        public ArticlesController(IArticleService service)
        {
            _service=service;
        }

        // GET: api/<ArticlesController>
        [HttpGet]
        public IActionResult Get()
        {
            var lst = _service.GetAll();
            if(lst != null && lst.Count > 0)
            {
                return Ok(lst);
            }
            return NotFound("No se encontraron articulos");
        }

        

        // POST api/<ArticlesController>
        [HttpPost]
        public IActionResult Post([FromBody] Articulo articulo)
        {
            try
            {
                var art = _service.Save(articulo);
                if (art)
                {
                    return Ok("Articulo guardado correctamente");
                }
                return BadRequest("No se pudo guardar el articulo");
            }
            catch (Exception ex)
            {

                throw new Exception (ex.Message);
            }
        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Articulo articulo)
        {
            try
            {
                if(id != articulo.CodArticulo)
                {
                    return BadRequest("El id del articulo no coincide");
                }
                var art = _service.Save(articulo);
                if (art)
                {
                    return Ok("Articulo actualizado correctamente");
                }
                return BadRequest("No se pudo actualizar el articulo");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var art = _service.Delete(id);
                if (art)
                {
                    return Ok("Articulo eliminado correctamente");
                }
                return BadRequest("No se pudo eliminar el articulo");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
