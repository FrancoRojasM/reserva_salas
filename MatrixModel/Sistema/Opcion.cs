using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
namespace Sistema
{
    
    public class Opcion
    {

        
        public int IdOpcion { get; set; }

        
        public Modulo modulo { get; set; }

        
        public int IdOpcionPadre { get; set; }
        
        public string NombreFormulario { get; set; }
        
        public string Parametros { get; set; }
        
        public string RutaFormulario { get; set; }
        
        public string Controlador { get; set; }
        
        public string Accion { get; set; }
        
        public string NombreOpcion { get; set; }
        
        public string NombreOpcionPadre { get; set; }
        
        public string DetalleOpcion { get; set; }

        
        public Catalogo catalogotipoopcion { get; set; }

        
        public int OrdenOpcion { get; set; }

        
        public string RutaImagenOpcion { get; set; }

        
        public bool Activo { get; set; }

        
        public int EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~Opcion()
        //{
        //}
        public Opcion()
        {
            modulo = new Modulo();         
            catalogotipoopcion = new Catalogo();
            Activo = true;
            mensaje = new Mensaje();
        }
    }
    
    public class ListaOpcion
    {
        
        public List<Opcion> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaOpcion()
        {
            lista = new List<Opcion>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}



