using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MyWallet
{
    public class Category
    {
        private Guid _guid;
        private string _name;
        private string _description;
        private string _colore;
        private Object? _icon;


        public Guid Guid
        {
            get { return _guid; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Colore
        {
            get { return _colore; }
            set { _colore = value; }
        }
        public Object? Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public Category()
        {
            _guid = Guid.NewGuid();
        }

        public Category(Guid guid, string name, string desc, string col)
        {
            _guid = guid;
            _name = name;
            _description = desc;
            _colore = col;
        }

        public Category(Guid guid, string name, string desc, string col, Object ic)
        {
            _guid = guid;
            _name = name;
            _description = desc;
            _colore = col;
            _icon = ic;

        }


    }
}