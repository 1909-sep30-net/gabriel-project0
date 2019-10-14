using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    /* SCRAPPING THIS IDEA FOR NOW */
    public class Color
    {
        //private int _colorId;
        private string _name;

        // An instance name is based on its ID, which maps to an array given by the db
        public string Name 
        {
            get
            {
                return _name;
            }

            set
            {
                string[] colorName = value.Split(' ');
                string fullName = "";

                // Auto-capitalize the color name
                foreach (string s in colorName)
                {
                    fullName += s[0].ToString().ToUpper() + s.Substring(1).ToLower() + " ";
                }
                _name = fullName;
            }
        }


        /// <summary>
        /// Determines the Name that this color represents
        /// </summary>
        public int Color_id { get; set; }

    }
}
