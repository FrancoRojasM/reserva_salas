using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Proyeccion
    {
        public int IdProyeccion { get; set; }
        public string Periodo { get; set; }
        public string Fuente { get; set; }
        public string Producto { get; set; }
        public string Meta { get; set; }
        public string Cod_Meta { get; set; }
        public string TipoTrabajador { get; set; }
        public string TipoTransaccion { get; set; }
        public string Generica { get; set; }
        public string Subgenerica { get; set; }
        public string Subgenericadetalle { get; set; }
        public string Especifica { get; set; }
        public string Clasificador { get; set; }
        public string Cod_Clasificador { get; set; }
        public string Concepto { get; set; }
        public string Situacion { get; set; }
        public string Cantidad { get; set; }
        public string Monto_pia { get; set; }
        public string Monto_pim { get; set; }
        public string Monto_eje { get; set; }
        public string Monto { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Proyeccion()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaProyeccion
    {
        public List<Proyeccion> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaProyeccion()
        {
            lista = new List<Proyeccion>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}