using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
namespace RecursoHumano
{

    public class EmpleadoPerfil
    {
        public int IdEmpleadoPerfil { get; set; }


        public Empleado empleado { get; set; }


        public EmpresaSede empresasede { get; set; } 


        public Area area { get; set; }


        public Cargo cargo { get; set; }

        
        public bool DestinoTodos { get; set; }
        public bool Activo { get; set; }
        public string ActivoTexto { get; set; }
        
        public string Actual { get; set; }
        
        public int EstadoAuditoria { get; set; }

        public int IdUsuarioAuditoria { get; set; }

        public Mensaje mensaje { get; set; }


        public string NombreEmpleadoPerfil { get; set; }

        //~EmpleadoPerfil()
        //{
        //}
        public EmpleadoPerfil()
        {
            empleado = new Empleado();
            empresasede = new EmpresaSede();
            area = new Area();
            cargo = new Cargo();
            Activo = true;
            mensaje = new Mensaje();
        }
    }
    
    public class ListaEmpleadoPerfil
    {
        
        public List<EmpleadoPerfil> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaEmpleadoPerfil()
        {
            lista = new List<EmpleadoPerfil>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}