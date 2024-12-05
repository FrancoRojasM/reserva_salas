
using System;
using Utilitarios;
using System.Collections.Generic;
using General;

namespace RecursoHumano
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public Empresa empresapadre { get; set; }
        public Persona persona { get; set; }
        public bool Activo { get; set; }
        public string AbreviaturaArea { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public Catalogo catalogoestadoempleado { get; set; }
        public Catalogo catalogotipoempleado { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public Empleado()
        {
            empresapadre = new Empresa();
            persona = new Persona();
            Activo = true;
            catalogoestadoempleado = new Catalogo();
            catalogotipoempleado = new Catalogo();
            mensaje = new Mensaje();
        }
    }
    public class ListaEmpleado
    {
        public List<Empleado> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaEmpleado()
        {
            lista = new List<Empleado>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}