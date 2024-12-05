using Utilitarios;
using System.Collections.Generic;
using Sistema;
namespace Seguridad
{    
    public class PerfilOpcion
    {        
        public int IdPerfilOpcion { get; set; }

        
        public Perfil perfil { get; set; }

        
        public int IdOpcionPadre { get; set; }

        
        public Opcion opcion { get; set; }

        
        public bool Ver { get; set; }

        
        public bool Nuevo { get; set; }

        
        public bool Editar { get; set; }

        
        public bool Guardar { get; set; }

        
        public bool Eliminar { get; set; }

        
        public bool Imprimir { get; set; }

        
        public bool Exportar { get; set; }

        
        public bool VistaPrevia { get; set; }

        
        public bool Consultar { get; set; }

        
        public bool Ver0 { get; set; }

        
        public bool Nuevo0 { get; set; }

        
        public bool Editar0 { get; set; }

        
        public bool Guardar0 { get; set; }

        
        public bool Eliminar0 { get; set; }

        
        public bool Imprimir0 { get; set; }

        
        public bool Exportar0 { get; set; }

        
        public bool VistaPrevia0 { get; set; }

        
        public bool Consultar0 { get; set; }

        
        public int EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~PerfilOpcion()
        //{
        //}
        public PerfilOpcion()
        {
            perfil = new Perfil();
            opcion = new Opcion();
            mensaje = new Mensaje();
        }
    }
    
    public class ListaPerfilOpcion
    {
        
        public List<PerfilOpcion> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaPerfilOpcion()
        {
            lista = new List<PerfilOpcion>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}

	

		