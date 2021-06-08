using System;
using System.Collections.Generic;
using System.Text;

namespace OBJ_Analaizer
{
    class Normal
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public  Normal(Vertex vertex1, Vertex vertex2, Vertex vertex3)
        {
            try
            {
                if (vertex1 is null)
                {
                    throw new ArgumentNullException(nameof(vertex1));
                }

                if (vertex2 is null)
                {
                    throw new ArgumentNullException(nameof(vertex2));
                }

                if (vertex3 is null)
                {
                    throw new ArgumentNullException(nameof(vertex3));
                }
                Vertex v1 = new Vertex(vertex1.X - vertex2.X, vertex1.Y - vertex2.Y, vertex1.Z - vertex2.Z);
                Vertex v2 = new Vertex(vertex3.X - vertex2.X, vertex3.Y - vertex2.Y, vertex3.Z - vertex2.Z);

                Vertex v3 = new Vertex(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);

                double length = Math.Sqrt(v3.X * v3.X + v3.Z * v3.Z + v3.Y * v3.Y);

                this.X = v3.X / length;
                this.Y = v3.Y / length;
                this.Z = v3.Z / length;
            }
            catch
            {

            }
        }
        public bool NearToHorizontal(double accuracy)
        {
            double xBase = 0;
            double yBase = 1;
            double zBase = 0;

            double scalar = Math.Abs(xBase * X + yBase * Y + zBase * Z);

            if (scalar >= 1 - accuracy)
                return true;
            return false;
        }

        public bool NearToVertical(double accuracy)
        {
            double xBase = 0;
            double yBase = 1;
            double zBase = 0;


            double scalar = Math.Abs(xBase * X + yBase * Y + zBase * Z);

            if (scalar <= accuracy)
                 return true;
            return false;
        }
    }
}
