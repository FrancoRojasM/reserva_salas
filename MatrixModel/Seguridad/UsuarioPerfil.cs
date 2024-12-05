using Utilitarios;
using System.Collections.Generic;
using RecursoHumano;

namespace Seguridad
{

    public class UsuarioPerfil
    {


        public int IdUsuarioPerfil { get; set; }


        public Usuario usuario { get; set; }


        public EmpleadoPerfil empleadoperfil { get; set; }


        public Perfil perfil { get; set; }


        public bool Activo { get; set; }


        public int EstadoAuditoria { get; set; }

        public int IdUsuarioAuditoria { get; set; }

        public Mensaje mensaje { get; set; }
        public UsuarioPerfil()
        {
            usuario = new Usuario();
            empleadoperfil = new EmpleadoPerfil();
            perfil = new Perfil();
            Activo = true;
            mensaje = new Mensaje();
        }
    }
    public class ListaUsuarioPerfil
    {

        public List<UsuarioPerfil> lista { get; set; }

        public Mensaje mensaje { get; set; }

        public Paginacion paginacion { get; set; }
        public ListaUsuarioPerfil()
        {
            lista = new List<UsuarioPerfil>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}



