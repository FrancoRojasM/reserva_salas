using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
namespace Sistema
{
    
    public class OpcionFormulario
    {

        
        public int IdOpcionFormulario { get; set; }

        
        public Opcion opcion { get; set; }

        
        public string NombreFormulario { get; set; }

        
        public string RutaFormulario { get; set; }

        
        public string Controlador { get; set; }

        
        public string Accion { get; set; }

        
        public string Parametros { get; set; }

        
        public bool Ver { get; set; }

        
        public bool Nuevo { get; set; }

        
        public bool Editar { get; set; }

        
        public bool Guardar { get; set; }

        
        public bool Eliminar { get; set; }

        
        public bool Imprimir { get; set; }

        
        public bool Exportar { get; set; }

        
        public bool VistaPrevia { get; set; }

        
        public bool Consultar { get; set; }

        
        public bool Activo { get; set; }

        
        public int EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~OpcionFormulario()
        //{
        //}
        public OpcionFormulario()
        {
            opcion = new Opcion();
            Activo = true;
            mensaje = new Mensaje();
        }      
    }
    
    public class ListaOpcionFormulario
    {
        
        public List<OpcionFormulario> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaOpcionFormulario()
        {
            lista = new List<OpcionFormulario>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}



