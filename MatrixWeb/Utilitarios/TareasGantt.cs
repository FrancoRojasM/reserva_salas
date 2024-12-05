using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Text;

namespace Utilitarios
{
    [DataContract]
    public class TareasGantt
    {
        [DataMember]
        public int pID { get; set; }

        [DataMember]
        public string pName { get; set; }

        [DataMember]
        public string pStart { get; set; }

        [DataMember]
        public string pEnd { get; set; }

        [DataMember]
        public string pPlanStart { get; set; }

        [DataMember]
        public string pPlanEnd { get; set; }

        [DataMember]
        public string pClass { get; set; }

        [DataMember]
        public string pLink { get; set; }

        [DataMember]
        public int pMile { get; set; }

        [DataMember]
        public string pRes { get; set; }

        [DataMember]
        public decimal pComp { get; set; }

        [DataMember]
        public int pGroup { get; set; }

        [DataMember]
        public int pParent { get; set; }

        [DataMember]
        public int pOpen { get; set; }

        [DataMember]
        public string pDepend { get; set; }

        [DataMember]
        public string pCaption { get; set; }

        [DataMember]
        public string pNotes { get; set; }


    }


    [DataContract]
    public class ListaTareasGantt
    {
        [DataMember]
        public List<TareasGantt> listaTarea { get; set; }

        public ListaTareasGantt()
        {
            listaTarea = new List<TareasGantt>();
        }
    }

}
