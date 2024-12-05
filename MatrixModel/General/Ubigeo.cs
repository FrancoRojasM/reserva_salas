using System;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
namespace General
{
    
    public class Ubigeo
    {

        
        public int IdUbigeo { get; set; }

        
        public string CodigoUbigeo { get; set; }

        
        public int CodigoDepartamento { get; set; }
        
        public string Departamento { get; set; }
        
        public string Provincia { get; set; }
        
        public string Distrito { get; set; }
        public GobiernoRegional gobiernoregional{ get; set; }


        public int CodigoProvincia { get; set; }

        
        public int CodigoDistrito { get; set; }

        
        public string Descripcion { get; set; }

        
        public int EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~Ubigeo()
        //{
        //}
        public Ubigeo()
        {
            mensaje = new Mensaje();
            gobiernoregional = new GobiernoRegional();
        }
    }
    
    public class ListaUbigeo
    {
        
        public List<Ubigeo> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaUbigeo()
        {
            lista = new List<Ubigeo>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}



