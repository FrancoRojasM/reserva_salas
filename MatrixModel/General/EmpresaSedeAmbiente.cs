		using System;
		using System.Runtime.Serialization;
		using Utilitarios;
		using System.Data;
		using System.Collections.Generic;
		using General;
namespace General
{
    
    public class EmpresaSedeAmbiente
    {

        
        public int IdEmpresaSedeAmbiente { get; set; }

        
        public EmpresaSede empresasede { get; set; }

        
        public string CodigoAmbiente { get; set; }

        
        public string NombreAmbiente { get; set; }

        
        public string DescripcionAmbiente { get; set; }

        
        public bool Activo { get; set; }

        
        public bool EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~EmpresaSedeAmbiente()
        //{
        //}
        public EmpresaSedeAmbiente()
        {
            empresasede = new EmpresaSede();
            Activo = true;
            mensaje = new Mensaje();
        }
    }
    
    public class ListaEmpresaSedeAmbiente
    {
        
        public List<EmpresaSedeAmbiente> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaEmpresaSedeAmbiente()
        {
            lista = new List<EmpresaSedeAmbiente>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}

	

		