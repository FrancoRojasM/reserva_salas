using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
namespace Seguridad
{   
    public class Perfil
    {
        
        public int IdPerfil { get; set; }

        
        public string NombrePerfil { get; set; }

        
        public string DetallePerfil { get; set; }

        
        public bool Activo { get; set; }

        
        public int EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~Perfil()
        //{
        //}
        public Perfil()
        {
            Activo = true;
            mensaje = new Mensaje();
        }
    }
    
    public class ListaPerfil
    {
        
        public List<Perfil> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaPerfil()
        {
            lista = new List<Perfil>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}



