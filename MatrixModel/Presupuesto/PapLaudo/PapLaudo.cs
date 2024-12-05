using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class PapLaudo
    {
        public int Id { get; set; }
        public int IdPap { get; set; }
        public int IdPapLaudo { get; set; }
        public decimal Remuneracion { get; set; }
        public decimal Bonificacion { get; set; }
        public decimal AsignacionFam { get; set; }
        public string  porcentaje1 { get; set;}
        public decimal perlaudo1 { get; set;}
        public decimal mntlaudo1 { get; set;}
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }

        public PapLaudo()
        {
            Id = 0;
            IdPap = 0;
            IdPapLaudo = 0;
            Remuneracion = 0;
            Bonificacion = 0;
            AsignacionFam = 0;
            porcentaje1 = string.Empty;
            perlaudo1 = 0;
            mntlaudo1 = 0;
            EstadoAuditoria = true;
            IdUsuarioAuditoria = 0;
            mensaje = new Mensaje();
        }
    }
    public class ListaPapLaudo
    {
        public List<PapLaudo> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaPapLaudo()
        {
            lista = new List<PapLaudo>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}
