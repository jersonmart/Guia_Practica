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
        private readonly equiposContex _equiposContexto;
        public equiposController(equiposContex equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            var listadoEquipo = (from e in _equiposContexto.equipos
                                 join m in _equiposContexto.marcas on e.marca_id equals m.id_marcas
                                 join te in _equiposContexto.tipo_equipo on e.tipo_equipo_id equals te.id_tipo_equipo
                                 select new
                                 {
                                     e.id_equipos,
                                     e.nombre,
                                     e.tipo_equipo_id,
                                     tipo_descripcion = te.descripcion,
                                     e.marca_id,
                                     m.nombre_marca
                                 }
                                 ).ToList();

            if (listadoEquipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);

        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);

        }
        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindbyDescription(String filtro)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.descripcion.Contains(filtro)
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEquipo([FromBody] equipos equipo)
        {

            try
            {
                _equiposContexto.equipos.Add(equipo);
                _equiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] equipos equipoModificar)
        {
            equipos? equiposActual = (from e in _equiposContexto.equipos
                                      where e.id_equipos == id
                                      select e).FirstOrDefault();
            if (equiposActual == null)
            {
                return NotFound(id);
            }

            equiposActual.nombre = equipoModificar.nombre;
            equiposActual.descripcion = equipoModificar.descripcion;
            equiposActual.marca_id = equipoModificar.marca_id;
            equiposActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
            equiposActual.anio_compra = equipoModificar.anio_compra;
            equiposActual.costo = equipoModificar.costo;

            _equiposContexto.Entry(equiposActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();
            return Ok(equipoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id)
        {

            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
                return NotFound();

            _equiposContexto.equipos.Attach(equipo);
            _equiposContexto.equipos.Remove(equipo);
            _equiposContexto.SaveChanges();

            return Ok(equipo);
        }
    }
}
