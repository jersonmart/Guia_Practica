using Guia_Practica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Guia_Practica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class marcasController : ControllerBase
    {
        private readonly equiposContex _marcaContexto;
        public marcasController(equiposContex marcaContexto)
        {
            _marcaContexto = marcaContexto;
        }
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            var listadoMarcas = (from m in _marcaContexto.marcas
                                           select m).ToList();

            if (listadoMarcas.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoMarcas);

        }
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            marcas? marca = (from m in _marcaContexto.marcas
                               where m.id_marcas == id
                               select m).FirstOrDefault();

            if (marca == null)
            {
                return NotFound();
            }
            return Ok(marca);

        }
        [HttpGet]
        [Route("Find/{filtro mediante marca}")]

        public IActionResult FindbyDescription(String filtro)
        {
            marcas? marca = (from m in _marcaContexto.marcas
                               where m.nombre_marca.Contains(filtro)
                               select m).FirstOrDefault();

            if (marca == null)
            {
                return NotFound();
            }
            return Ok(marca);
        }
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarMarca([FromBody] marcas marca)
        {

            try
            {
                _marcaContexto.marcas.Add(marca);
                _marcaContexto.SaveChanges();
                return Ok(marca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarMarca(int id, [FromBody] marcas marcaModificar)
        {
        marcas? marcaActual = (from m in _marcaContexto.marcas
                               where m.id_marcas == id
                               select m).FirstOrDefault();
        if (marcaActual == null)
            {
                return NotFound(id);
            }

        marcaActual.nombre_marca = marcaModificar.nombre_marca;
        marcaActual.estados = marcaModificar.estados;

        _marcaContexto.Entry(marcaActual).State = EntityState.Modified;
        _marcaContexto.SaveChanges();
            return Ok(marcaModificar);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarMarca(int id)
        {

            marcas? marca = (from m in _marcaContexto.marcas
                               where m.id_marcas == id
                               select m).FirstOrDefault();

            if (marca == null)
                return NotFound();

            _marcaContexto.marcas.Attach(marca);
            _marcaContexto.marcas.Remove(marca);
            _marcaContexto.SaveChanges();

            return Ok(marca);
        }
    }
}
