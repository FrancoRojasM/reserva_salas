using System;
using System.Collections.Generic;

namespace Sistema
{
    public class Menu
    {
        private int menuPadre = 0;

        public int MenuPadre
        {
            get { return menuPadre; }
            set { menuPadre = value; }
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Link { get; set; }


        public Menu(int _id, string _itemNombre, string _itemLink, int _menuPadre)
        {
            Id = _id;
            Nombre = _itemNombre;
            Link = _itemLink;
            MenuPadre = _menuPadre;
        }
       
    }
}