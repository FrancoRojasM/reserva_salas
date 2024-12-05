
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class MetaPresupuestal
    {
        public int IdMetaPresupuestal { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public MetaPresupuestal()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaMetaPresupuestal
    {
        public List<MetaPresupuestal> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaMetaPresupuestal()
        {
            lista = new List<MetaPresupuestal>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}