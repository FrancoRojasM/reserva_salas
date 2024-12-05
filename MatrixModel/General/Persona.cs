using Utilitarios;
using System.Collections.Generic;
namespace General
{
    
    public class Persona
    {

        
        public int IdPersona { get; set; }

        
        public Catalogo catalogotipopersona { get; set; }

        
        public string NombreCompleto { get; set; }

        
        public string Nombres { get; set; }

        
        public string ApellidoPaterno { get; set; }

        
        public string ApellidoMaterno { get; set; }

        
        public Catalogo catalogotipodocumentopersonal { get; set; }

        
        public string NumeroDocumento { get; set; }
        public int TipoDocumento { get; set; }


        public Ubigeo ubigeo { get; set; }

        public Pais paisnacimiento { get; set; }
        public string LugarNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }  
        public string Anexo { get; set; }
        
       public string RutaArchivoFoto { get; set; }
        public string FechaNacimiento { get; set; }

        
        public bool Sexo { get; set; }
        public string TelefonoFijo { get; set; }

        
        public int EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        
        public Mensaje mensaje { get; set; }

        public string Email { get; set; }
        public string CorreoPersonal { get; set; }

        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public int IdGenero { get; set; }
        public int Edad { get; set; }

        //~Persona()
        //{
        //}
        public Persona()
        {
            catalogotipopersona = new Catalogo();
            catalogotipodocumentopersonal = new Catalogo();
            ubigeo = new Ubigeo();
            paisnacimiento = new Pais();
            mensaje = new Mensaje();
        }
    }
    
    public class ListaPersona
    {
        
        public List<Persona> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaPersona()
        {
            lista = new List<Persona>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}



