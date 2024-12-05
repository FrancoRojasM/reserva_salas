using Utilitarios;
using System.Collections.Generic;
using System;
namespace Inventario
{

    public class Articulo
    {
        public int Id { get; set; }

        public string Codigo_Barra { get; set; }

        public string ItemCode { get; set; }

        public string Ubicacion_Region { get; set; }

        public string Ubicacion_Sede { get; set; }

        public string Ubicacion_Area { get; set; }

        public string Ubicacion_Sub_Area { get; set; }

        public string Piso { get; set; }

        public string ItemName { get; set; }

        public string Detalle { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Serie { get; set; }

        public string Material { get; set; }

        public string Medida { get; set; }

        public string Color { get; set; }

        public string Estado { get; set; }

        public string Condicion_Uso { get; set; }

        public string Usuario { get; set; }

        public string Documento { get; set; }

        public string Cargo { get; set; }

        public string Gerencia { get; set; }

        public string GroupName { get; set; }

        public string EstadoConta { get; set; }

        public string Periodo { get; set; }

        public string Tipo_Inventario { get; set; }

        public string Tipo_Asignacion { get; set; }
        
        public int? Sucursal { get; set; }

        public string Image1 { get; set; }

        public string Image2 { get; set; }

        public string Image3 { get; set; }

        public string Image4 { get; set; }

        public DateTime? FE_CREA { get; set; }

        public DateTime? FE_MODI { get; set; }

        public int? US_CREA { get; set; }

        public int? US_MODI { get; set; }

        public int? STATUS { get; set; }

        public bool EstadoAuditoria { get; set; }

        public int IdUsuarioAuditoria { get; set; }

        public string US_Registro { get; set; }

        public Mensaje mensaje { get; set; }

        public Articulo()
        {
            mensaje = new Mensaje();
        }
    }

    public class ListaArticulo
    {

        public List<Articulo> lista { get; set; }

        public Mensaje mensaje { get; set; }

        public Paginacion paginacion { get; set; }
        public ListaArticulo()
        {
            lista = new List<Articulo>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}



