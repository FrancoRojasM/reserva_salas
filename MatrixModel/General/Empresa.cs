using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
namespace General
{

    public class Empresa
    {


        public int IdEmpresa { get; set; }

        public int IdEmpresaPadre { get; set; }


        public Persona persona { get; set; }


        public string NombreEmpresa { get; set; } 

        public string NombreEmpresaPadre { get; set; }

        public bool Activo { get; set; }


        public bool EstadoAuditoria { get; set; }

        public int IdUsuarioAuditoria { get; set; }

        public Mensaje mensaje { get; set; }
        //~Empresa()
        //{
        //}
        public Empresa()
        {
            persona = new Persona();
            Activo = true;
            mensaje = new Mensaje();
        }
    }

    public class ListaEmpresa
    {

        public List<Empresa> lista { get; set; }

        public Mensaje mensaje { get; set; }

        public Paginacion paginacion { get; set; }
        public ListaEmpresa()
        {
            lista = new List<Empresa>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}



