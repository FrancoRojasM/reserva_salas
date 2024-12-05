using General;
using System;
using System.Collections.Generic;
using Utilitarios;

namespace Seguridad
{
    
    public class Usuario
    {

        
        public int IdUsuario { get; set; }

        
        public Persona persona { get; set; }

        
        public Catalogo catalogotipousuario { get; set; }

        
        public string Logueo { get; set; }


        public string Clave { get; set; } = "e10adc3949ba59abbe56e057f20f883e";

        public string Token { get; set; } = "e10adc3949ba59abbe56e057f20f883e";

        public bool EsInstitucion { get; set; }
        public bool Bloqueado { get; set; }

        public string Email { get; set; }
        public int CantidadToken { get; set; }
        public int CantidadPerfil { get; set; }
        
        public string RutaArchivoFoto { get; set; }

        
        public int EstadoAuditoria { get; set; }
        
        public int IdUsuarioAuditoria { get; set; }
        public int IdAdministrado { get; set; }
        
        public Mensaje mensaje { get; set; }
        //~Usuario()
        //{
        //}
        public Usuario()
        {
            persona = new Persona();
            catalogotipousuario = new Catalogo();
            catalogotipousuario.IdCatalogo = 4;
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 7;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            Clave = contraseniaAleatoria;
            mensaje = new Mensaje();
        }
    }
    
    public class ListaUsuario
    {
        
        public List<Usuario> lista { get; set; }
        
        public Mensaje mensaje { get; set; }
        
        public Paginacion paginacion { get; set; }
        public ListaUsuario()
        {
            lista = new List<Usuario>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}

	

		