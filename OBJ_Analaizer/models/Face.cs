using System;
using System.Collections.Generic;
using System.Text;

namespace OBJ_Analaizer
{
    public class Face
    {
        private int indexOfVertex1;
        private int indexOfVertex2;
        private int indexOfVertex3;

        private int indexOfVertexNormal1;
        private int indexOfVertexNormal2;
        private int indexOfVertexNormal3;

        private int indexOfTexture1;
        private int indexOfTexture2;
        private int indexOfTexture3;

        private Normal normal;

        public Face(int indexOfVertex1, int indexOfVertex2, int indexOfVertex3, int indexOfVertexNormal1, int indexOfVertexNormal2, int indexOfVertexNormal3, int indexOfTexture1, int indexOfTexture2, int indexOfTexture3)
        {
            this.IndexOfVertex1 = indexOfVertex1;
            this.IndexOfVertex2 = indexOfVertex2;
            this.IndexOfVertex3 = indexOfVertex3;
            this.IndexOfVertexNormal1 = indexOfVertexNormal1;
            this.IndexOfVertexNormal2 = indexOfVertexNormal2;
            this.IndexOfVertexNormal3 = indexOfVertexNormal3;
            this.IndexOfTexture1 = indexOfTexture1;
            this.IndexOfTexture2 = indexOfTexture2;
            this.IndexOfTexture3 = indexOfTexture3;
            this.Normal = null;

        }

        public int IndexOfVertex1 { get => indexOfVertex1; set => indexOfVertex1 = value; }
        public int IndexOfVertex2 { get => indexOfVertex2; set => indexOfVertex2 = value; }
        public int IndexOfVertex3 { get => indexOfVertex3; set => indexOfVertex3 = value; }
        public int IndexOfVertexNormal1 { get => indexOfVertexNormal1; set => indexOfVertexNormal1 = value; }
        public int IndexOfVertexNormal2 { get => indexOfVertexNormal2; set => indexOfVertexNormal2 = value; }
        public int IndexOfVertexNormal3 { get => indexOfVertexNormal3; set => indexOfVertexNormal3 = value; }
        public int IndexOfTexture1 { get => indexOfTexture1; set => indexOfTexture1 = value; }
        public int IndexOfTexture2 { get => indexOfTexture2; set => indexOfTexture2 = value; }
        public int IndexOfTexture3 { get => indexOfTexture3; set => indexOfTexture3 = value; }
        internal Normal Normal { get => normal; set => normal = value; }

        public void  CountNormalVector(Vertex vertex1, Vertex vertex2, Vertex vertex3)
        {
            this.Normal = new Normal(vertex1, vertex2, vertex3);
        }
    }
}
