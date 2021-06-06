using System;
using System.Collections.Generic;
using System.Text;

namespace OBJ_Analaizer
{
    public class Vertex
    {
        private double x;
        private double y;
        private double z;

        public Vertex(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }    
        
        public override string ToString()
        {
            string tmp = Convert.ToString(X) + " " + Convert.ToString(Y) + " " + Convert.ToString(Z);
            return tmp.Replace(",", ".");
        }
    }


}
