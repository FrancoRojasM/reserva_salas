using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class Siaf
    {
        public int IdSiaf { get; set; }
        public int Periodo { get; set; }
        public string Mnemonico { get; set; }
        public string Meta { get; set; }
        public string Partida { get; set; }
        public string Clasificador { get; set; }
        public decimal Mt_pia { get; set; }
        public decimal Mt_pim { get; set; }
        public decimal Mt_cert{ get; set; }
        public decimal Mt_eje { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Siaf()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaSiaf
    {
        public List<Siaf> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaSiaf()
        {
            lista = new List<Siaf>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}