using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
namespace General
{
    
    public class Area
    {

        
        public int IdArea { get; set; }
        
        public Empresa empresa { get; set; }
        
        public string NombreAreaPadre { get; set; }

        
        
        public int IdAreaPadre { get; set; } 

        
        public string NombreArea { get; set; }

        
        public string Abreviatura { get; set; }

        
        public string Sigla { get; set; }

        
        public bool Activo { get; set; }

        public bool VerRecepcion { get; set; }
        public Catalogo catalogotipoarea { get; set; }

        
        public int EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~Area()
        //{
        //}
        public Area()
        {
            Activo = true;
            catalogotipoarea = new Catalogo();
            mensaje = new Mensaje();
            empresa = new Empresa();
        }
    }
    
    public class ListaArea
    {
        
        public List<Area> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaArea()
        {
            lista = new List<Area>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}



