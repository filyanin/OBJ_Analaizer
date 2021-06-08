using System;
using System.Collections.Generic;
using System.Text;

namespace OBJ_Analaizer
{
    class FacesAnalizer
    {
        public static List<Face> SearchVerticalFaces(List<Face> faces, double _accuracy)
        {
            List<Face> vertivalFaces = new List<Face>();

            foreach (var tmp in faces)
            {
                if (tmp.Normal.NearToVertical(_accuracy))
                {
                    vertivalFaces.Add(tmp);
                }
            }
            return vertivalFaces;
        }

        public static List<Face> SearchHorizontalFaces(List<Face> faces, double _accuracy)
        {
            List<Face> vertivalFaces = new List<Face>();

            foreach (var tmp in faces)
            {
                if (tmp.Normal.NearToHorizontal(_accuracy))
                {
                    vertivalFaces.Add(tmp);
                }
            }
            return vertivalFaces;
        }

        public static List<int> GetVertexList(List<Face> faces)
        {
            List<int> vertices = new List<int>();

            foreach (var tmp in faces)
            {
                if (!vertices.Contains(tmp.IndexOfVertex1))
                {
                    vertices.Add(tmp.IndexOfVertex1);
                }
                if (!vertices.Contains(tmp.IndexOfVertex2))
                {
                    vertices.Add(tmp.IndexOfVertex2);
                }
                if (!vertices.Contains(tmp.IndexOfVertex3))
                {
                    vertices.Add(tmp.IndexOfVertex3);
                }
            }
            return vertices;
        }

        public static int DeleteLonelyTriangle(List<Face> FacesToRemove, int depth)
        {
            int counter = 0;

            for (int z = 0; z < depth; z++)
            {
                for (int i = 0; i < FacesToRemove.Count; i++)
                {
                    bool IfNotExist = true;
                    for (int j = i + 1; j < FacesToRemove.Count; j++)
                    {
                        if ((FacesToRemove[i].IndexOfVertex1 == FacesToRemove[j].IndexOfVertex1) || (FacesToRemove[i].IndexOfVertex2 == FacesToRemove[j].IndexOfVertex2) || (FacesToRemove[i].IndexOfVertex3 == FacesToRemove[j].IndexOfVertex3))
                        {
                            IfNotExist = false;
                        }
                    }
                    if (IfNotExist)
                    {
                        FacesToRemove.RemoveAt(i);
                        counter++;
                    }
                }
            }
            return counter;
        }

        public static int SearchLostVerticalPoint(List<Face> faces, List<Face> verticalFaces)
        {
            int counter = 0;
            List<int> temp = GetVertexList(verticalFaces);
            foreach (var tmp in temp)
            {
                foreach (var face in faces)
                {
                    if (((face.IndexOfVertex1 == tmp) || (face.IndexOfVertex2 == tmp) || (face.IndexOfVertex3 == tmp)))
                    {
                        if (face.Normal.NearToVertical(0.7)) {
                            if (!verticalFaces.Contains(face))
                            {
                                verticalFaces.Add(face);
                                counter++;
                            }
                        }
                    }
                }
            }
            return counter;
        }

        public static int SearchLostHorizontalPoint(List<Face> faces, List<Face> horizontalFaces)
        {
            int counter = 0;
            List<int> temp = GetVertexList(horizontalFaces);
            foreach (var tmp in temp)
            {
                foreach (var face in faces)
                {
                    if (((face.IndexOfVertex1 == tmp) || (face.IndexOfVertex2 == tmp) || (face.IndexOfVertex3 == tmp)))
                    {
                        if (face.Normal.NearToHorizontal(0.7))
                        {
                            if (!horizontalFaces.Contains(face))
                            {
                                horizontalFaces.Add(face);
                                counter++;
                            }
                        }
                    }
                }
            }
            return counter;
        }
        
    }
}
