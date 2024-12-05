
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class EspecificaDet
    {
        public int IdEspecificaDet { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public EspecificaDet()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaEspecificaDet
    {
        public List<EspecificaDet> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaEspecificaDet()
        {
            lista = new List<EspecificaDet>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}