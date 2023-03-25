using System.ComponentModel.DataAnnotations;

namespace Guia_Practica.Models
{
    public class estados_equipos
    {
        [Key]
        public int id_estados_equipo { get; set; }
        public string? descripcion { get; set; }
        public string? estado { get; set; }
    }
}
