
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Clasificador
    {
        public int IdClasificador { get; set; }
        public string NombreClasificador { get; set; }
        public TipoTransaccion tipotransaccion { get; set; }
        public Generica generica { get; set; }
        public Subgenerica subgenerica { get; set; }
        public SubgenericaDet subgenericadet { get; set; }
        public Especifica especifica { get; set; }
        public EspecificaDet especificadet { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Clasificador()
        {
            tipotransaccion = new TipoTransaccion();
            generica = new Generica();
            subgenerica = new Subgenerica();
            subgenericadet = new SubgenericaDet();
            especifica = new Especifica();
            especificadet = new EspecificaDet();
            mensaje = new Mensaje();
        }
    }
    public class ListaClasificador
    {
        public List<Clasificador> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaClasificador()
        {
            lista = new List<Clasificador>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}