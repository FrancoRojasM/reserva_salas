﻿
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class SubgenericaDet
    {
        public int IdSubgenericaDet { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public SubgenericaDet()
        {
            mensaje = new Mensaje();
        }
    }
    public class ListaSubgenericaDet
    {
        public List<SubgenericaDet> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaSubgenericaDet()
        {
            lista = new List<SubgenericaDet>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}