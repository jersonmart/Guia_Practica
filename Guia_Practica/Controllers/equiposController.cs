using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Guia_Practica.Models;
using Microsoft.EntityFrameworkCore;

namespace Guia_Practica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContex _equiposContexo;
        public equiposController(equiposContex equiposContexto)
        {
            _equiposContexo = equiposContexto;
        }
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<equipos> listadoEquipo = (from e in _equiposContexo.equipos
                                           select e).ToList();

            if (listadoEquipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);

        }
    }
}
