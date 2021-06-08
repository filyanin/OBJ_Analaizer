using System;
using System.Collections.Generic;
using System.Text;

namespace OBJ_Analaizer
{
    public class Face
    {

        public int IndexOfVertex1 { get; set; }
        public int IndexOfVertex2 { get; set; }
        public int IndexOfVertex3 { get; set; }
        public int IndexOfVertexNormal1 { get; set; }
        public int IndexOfVertexNormal2 { get; set; }
        public int IndexOfVertexNormal3 { get; set; }

        internal Normal Normal { get; set; }

        public Face(int indexOfVertex1, int indexOfVertex2, int indexOfVertex3, int indexOfVertexNormal1, int indexOfVertexNormal2, int indexOfVertexNormal3)
        {
            this.IndexOfVertex1 = indexOfVertex1;
            this.IndexOfVertex2 = indexOfVertex2;
            this.IndexOfVertex3 = indexOfVertex3;
            this.IndexOfVertexNormal1 = indexOfVertexNormal1;
            this.IndexOfVertexNormal2 = indexOfVertexNormal2;
            this.IndexOfVertexNormal3 = indexOfVertexNormal3;
            this.Normal = null;

        }

        public void  CountNormalVector(Vertex vertex1, Vertex vertex2, Vertex vertex3)
        {
            this.Normal = new Normal(vertex1, vertex2, vertex3);
        }

        public override string ToString()
        {
            return "f " + Convert.ToString(IndexOfVertex1) + "//" + Convert.ToString(IndexOfVertexNormal1) + " " + Convert.ToString(IndexOfVertex2) + "//" + Convert.ToString(IndexOfVertexNormal2) + " " + Convert.ToString(IndexOfVertex3) + "//" + Convert.ToString(IndexOfVertexNormal3);
        }
    }
}
