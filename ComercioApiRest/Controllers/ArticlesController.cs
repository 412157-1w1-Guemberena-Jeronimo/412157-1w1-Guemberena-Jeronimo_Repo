using Ejercicio_1._5__Comercio_.Data;
using Ejercicio_1._5__Comercio_.Domain;
using Ejercicio_1._5__Comercio_.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComercioApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private IArticleService _service;

        public ArticlesController(IArticleService service)
        {
            _service = service;
        }

        

        // GET: api/<ArticlesController>
        [HttpGet]
        public IActionResult GeArticles() //Funciona
        {
            try
            {
                return Ok(_service.GetArticles());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al acceder a datos" });
            }
        }

        // GET api/<ArticlesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)//Funciona
        {

            try
            {
                var article = _service.GetArticleById(id);
                if (article != null)
                {
                    return Ok(article);
                }
                else
                {
                    throw new Exception("No se encontro un articulo con ese id");
                }

            }

            catch (Exception)
            {

                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Articulo no encontrado" });
            }
        }

        // POST api/<ArticlesController>
        [HttpPost]
        public IActionResult Post([FromBody] Article value)//Funciona
        {
            try
            {
                if(value == null)
                {
                    return BadRequest(new { mensaje = "El articulo no puede ser nulo" });
                }
                bool isSaved = _service.Save(value);
                if (isSaved)
                {
                    return Ok(new { mensaje =  "Articulo guardado correctamente" });
                }
                else
                {
                    throw new Exception("No se pudo guardar el articulo");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Article value)//Funciona
        {
            try
            {
                if (id != 0 && value!=null)
                {
                    var newArticle = _service.Save(value);
                    if(newArticle)
                    {
                        return Ok(new {mensaje = "Articulo actualizado correctamente"});
                    }
                    else
                    {
                        throw new Exception ("No se pudo actualizar el articulo");
                    }
                }
                else
                {
                    throw new Exception("Articulo no encontrado");
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Articulo no encontrado" });
            }
        }

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)//Funciona
        {
            try
            {
                if(id==0)
                {
                    return BadRequest(new {mensaje = "Debe ingresar el id del articulo a eliminar"});
                }
                bool DeletedArticle= _service.Delete(id);
                if(DeletedArticle)
                {
                    return Ok(new {mensaje = "Articulo eliminado con exito"});
                }
                else
                {
                    throw new Exception("No se pudo eliminar el articulo (devuelve false?)");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Articulo no encontrado" });

            }
        }
    }
}
