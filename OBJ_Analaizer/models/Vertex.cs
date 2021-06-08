using System;
using System.Collections.Generic;
using System.Text;

namespace OBJ_Analaizer
{
    public class Vertex
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public Vertex(double _x, double _y, double _z)
        {
            X = _x;
            Y = _y;
            Z = _z;
        }
        
        public override string ToString()
        {
            string tmp = Convert.ToString(X) + " " + Convert.ToString(Y) + " " + Convert.ToString(Z);
            return tmp.Replace(",", ".");
        }
    }


}
