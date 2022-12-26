using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Menus
    {
        private string menusID = string.Empty;
	    private string parentMenusID = string.Empty;
	    private string _menus = string.Empty;
	    private string imageTiny = string.Empty;
	    private string uRLink = string.Empty;
	    private string folder = string.Empty;
	    private string fileName = string.Empty;
        private int formType = 0;

        public string MenusID
        {
            get
            {
                return menusID;
            }
            set
            {
                menusID = value;
            }
        }

        public string ParentMenusID
        {
            get
            {
                return parentMenusID;
            }
            set
            {
                parentMenusID = value;
            }
        }

        public string menus
        {
            get
            {
                return _menus;
            }
            set
            {
                _menus = value;
            }
        }

        public string ImageTiny
        {
            get
            {
                return imageTiny;
            }
            set
            {
                imageTiny = value;
            }
        }

        public string URLink
        {
            get
            {
                return uRLink;
            }
            set
            {
                uRLink = value;
            }
        }

        public string Folder
        {
            get
            {
                return folder;
            }
            set
            {
                folder = value;
            }
        }

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        public int FormType
        {
            get
            {
                return formType;
            }
            set
            {
                formType = value;
            }
        }
        
    

    }
}
