using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Airhsp
    {
        public int IdAirhsp { get; set; }
        public string Codigo { get; set; }
        public string Estado { get; set; }
        public string CodGrupoOcupacional { get; set; }
        public string GrupoOcupacional { get; set; }
        public string CodCargo { get; set; }
        public string CargoFuncional { get; set; }
        public DateTime? FechaAuditoria { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Airhsp()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaAirhsp
    {
        public List<Airhsp> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaAirhsp()
        {
            lista = new List<Airhsp>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}