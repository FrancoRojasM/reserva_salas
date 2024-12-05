
using System.Runtime.Serialization;

namespace Utilitarios
{
    
    public class Paginacion
    {
        
        public string CampoOrdenado { get; set; }
        
        public string TipoOrdenacion { get; set; }
        
        public int NumeroPagina { get; set; }
        
        public int DimensionPagina { get; set; }

        public int TotalRegistros { get; set; } = 0;
       
      
    }
}
