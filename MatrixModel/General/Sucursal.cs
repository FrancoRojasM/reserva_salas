using Utilitarios;
using System.Collections.Generic;
using System;
namespace General
{
    public class Sucursal
    {
        public int Id { get; set; }

        public int BPLId { get; set; }

        public string BPLName { get; set; }

        public DateTime? FE_CREA { get; set; }

        public DateTime? FE_MODI { get; set; }

        public int? US_CREA { get; set; }

        public int? US_MODI { get; set; }

        public int? STATUS { get; set; }

        public Mensaje mensaje { get; set; }

        public Sucursal()
        {
            mensaje = new Mensaje();
        }
    }

    public class ListaSucursal
    {

        public List<Sucursal> lista { get; set; }

        public Mensaje mensaje { get; set; }

        public Paginacion paginacion { get; set; }
        public ListaSucursal()
        {
            lista = new List<Sucursal>();
            mensaje = new Mensaje();
            paginacion = new Paginacion();
        }
    }

}




