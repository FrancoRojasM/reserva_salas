using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
namespace General
{
    
    public class EmpresaSede
    {
                
        public int IdEmpresaSede { get; set; }

        
        public Empresa empresa { get; set; }

              
        public string DireccionSede { get; set; } 

        
        public string NombreSede { get; set; }

        
        public bool Activo { get; set; }

        
        public bool EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~EmpresaSede()
        //{
        //}
        public EmpresaSede()
        {
            empresa = new Empresa();
            Activo = true;
            mensaje = new Mensaje();
        }
        
    }
    
    public class ListaEmpresaSede
    {
        
        public List<EmpresaSede> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaEmpresaSede()
        {
            lista = new List<EmpresaSede>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}



