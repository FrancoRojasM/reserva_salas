using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
namespace General
{
    
    public class Cargo
    {

        
        public int IdCargo { get; set; }
        public Catalogo catalogotipocargo { get; set; }

        public string NombreCargo { get; set; }

        
        public bool Activo { get; set; }

        
        public int EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~Cargo()
        //{
        //}
        public Cargo()
        {
            Activo = true;
            catalogotipocargo = new Catalogo();
            mensaje = new Mensaje();
        }
    }
    
    public class ListaCargo
    {
        
        public List<Cargo> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaCargo()
        {
            lista = new List<Cargo>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}



