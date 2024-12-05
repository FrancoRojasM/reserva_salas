
using System;
using Utilitarios;
using System.Collections.Generic;
namespace Presupuesto
{
    public class MetaFPAF
    {
        public int idMetaFPAF { get; set; }
        public MetaPresupuestal metapresupuestal { get; set; }
        public FuenteProductoActividadFinalidad fuenteproductoactividadfinalidad { get; set; }
        public bool EstadoAuditoria { get; set; }
        public int IdUsuarioAuditoria { get; set; }
        public Mensaje mensaje { get; set; }
        public MetaFPAF()
        {
            metapresupuestal = new MetaPresupuestal();
            fuenteproductoactividadfinalidad = new FuenteProductoActividadFinalidad();
            mensaje = new Mensaje();
        }
    }
    public class ListaMetaFPAF
    {
        public List<MetaFPAF> lista { get; set; }
        public Mensaje mensaje { get; set; }
        public Paginacion paginacion { get; set; }
        public ListaMetaFPAF()
        {
            lista = new List<MetaFPAF>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }
}