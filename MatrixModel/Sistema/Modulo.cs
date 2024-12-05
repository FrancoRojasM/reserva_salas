using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
namespace Sistema
{
    
    public class Modulo
    {

        
        public int IdModulo { get; set; }

        public bool NuevoModulo { get; set; } = true;
        public string NombreModulo { get; set; }

        
        public string DetalleModulo { get; set; }

        
        public int OrdenModulo { get; set; }

        
        public Catalogo catalogotipomodulo { get; set; }

        
        public bool Activo { get; set; }

        
        public string RutaImagenModulo { get; set; }

        
        public int EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~Modulo()
        //{
        //}
        public Modulo()
        {
            catalogotipomodulo = new Catalogo();
            Activo = true;
            mensaje = new Mensaje();
        }       
    }
    
    public class ListaModulo
    {
        
        public List<Modulo> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaModulo()
        {
            lista = new List<Modulo>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}