using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Models
{
    /* SCRAPPING THIS IDEA FOR NOW */
    public class Color
    {
        private int _colorId;

        // To be fetched from the db of available colors
        private static List<string> colors = new List<string>();

        Color(int id, List<string> cList)
        {
            Color_id = id;
            colors = cList;
        }

        // An instance name is based on its ID, which maps to an array given by the db
        public string Name
        {
            get
            {
                return colors[Color_id];
            }
        }


        /// <summary>
        /// Determines the Name that this color represents
        /// </summary>
        public int Color_id
        {
            get
            {
                return _colorId;
            }
            set
            {
                // Throw exception if color_id tries to be set outside of colors range
                if (value >= colors.Count || value < 0)
                {
                    throw new ArgumentException("Id must be valid color_id within available color range", nameof(value));
                }

                _colorId = value;
            }
        }

    }
}
