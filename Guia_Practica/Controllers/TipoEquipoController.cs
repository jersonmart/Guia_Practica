using Guia_Practica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Guia_Practica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEquipoController : ControllerBase
    {
        private readonly equiposContex _TipoEquipoContexto;
        public TipoEquipoController(equiposContex TipoEquipoContexto)
        {
            _TipoEquipoContexto = TipoEquipoContexto;
        }
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            var listadoTipoEquipos = (from t in _TipoEquipoContexto.tipo_equipo
                                 select t).ToList();

            if (listadoTipoEquipos.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoTipoEquipos);

        }
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            tipo_equipo? tipo_Equipo = (from tp in _TipoEquipoContexto.tipo_equipo
                             where tp.id_tipo_equipo == id
                             select tp).FirstOrDefault();

            if (tipo_Equipo == null)
            {
                return NotFound();
            }
            return Ok(tipo_Equipo);

        }
        [HttpGet]
        [Route("Find/{filtro mediante tipo de equipo}")]

        public IActionResult FindbyDescription(String filtro)
        {
            tipo_equipo? tipo_Equipo = (from tp in _TipoEquipoContexto.tipo_equipo
                             where tp.descripcion.Contains(filtro)
                             select tp).FirstOrDefault();

            if (tipo_Equipo == null)
            {
                return NotFound();
            }
            return Ok(tipo_Equipo);
        }
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarTipoEquipo([FromBody] tipo_equipo tipo_Equipo)
        {

            try
            {
                _TipoEquipoContexto.tipo_equipo.Add(tipo_Equipo);
                _TipoEquipoContexto.SaveChanges();
                return Ok(tipo_Equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarTipoEquipo(int id, [FromBody] tipo_equipo tipoEquipoModificar)
        {
            tipo_equipo? tipoEquipoActual = (from m in _TipoEquipoContexto.tipo_equipo
                                   where m.id_tipo_equipo == id
                                   select m).FirstOrDefault();
            if (tipoEquipoActual == null)
            {
                return NotFound(id);
            }

            tipoEquipoActual.descripcion = tipoEquipoModificar.descripcion;
            tipoEquipoActual.estado = tipoEquipoModificar.estado;

            _TipoEquipoContexto.Entry(tipoEquipoActual).State = EntityState.Modified;
            _TipoEquipoContexto.SaveChanges();
            return Ok(tipoEquipoModificar);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarTipoEquipo(int id)
        {

            tipo_equipo? tipoEquipo = (from m in _TipoEquipoContexto.tipo_equipo
                             where m.id_tipo_equipo == id
                             select m).FirstOrDefault();

            if (tipoEquipo == null)
                return NotFound();

            _TipoEquipoContexto.tipo_equipo.Attach(tipoEquipo);
            _TipoEquipoContexto.tipo_equipo.Remove(tipoEquipo);
            _TipoEquipoContexto.SaveChanges();

            return Ok(tipoEquipo);
        }
    }
}
