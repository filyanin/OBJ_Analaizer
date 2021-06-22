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


        public static List<Face> SearchOtherFaces(List<Face> faces, List<Face> horizontalFaces, List<Face> verticalFaces)
        {
            List<Face> otherFaces = new List<Face>();

            bool[] allFaces = new bool[faces.Count];
            for (int i = 0; i < allFaces.Length; i++)
            {
                allFaces[i] = true;
            }
            foreach (var tmp in horizontalFaces)
            {
                allFaces[tmp.BaseNumber] = false;
            }
            foreach (var tmp in verticalFaces)
            {
                allFaces[tmp.BaseNumber] = false;
            }
            for (int i = 0; i < allFaces.Length; i++)
            {
                if (allFaces[i])
                {
                    otherFaces.Add(faces[i]);
                }
            }
            return otherFaces;
        }


        //Функция возвращает коллекцию листов, где индекс листа - это номер точки, а содержимое - то, в каких треугольниках используется данная точка
        public static List<List<int>> GetListOfTrianglesUsingPoint(List<Vertex> vertices, List<Face> faces)
        {
            List<List<int>> buffer = new List<List<int>>();

            foreach (var tmp in vertices)
            {
                buffer.Add(new List<int>());
            }

            for (int j = 0; j < faces.Count; j++)
            {

                if (!buffer[faces[j].IndexOfVertex1 - 1].Contains(j))
                {
                    buffer[faces[j].IndexOfVertex1 - 1].Add(j);
                }
                if (!buffer[faces[j].IndexOfVertex2 - 1].Contains(j))
                {
                    buffer[faces[j].IndexOfVertex2 - 1].Add(j);
                }
                if (!buffer[faces[j].IndexOfVertex3 - 1].Contains(j))
                {
                    buffer[faces[j].IndexOfVertex3 - 1].Add(j);
                }
            }

            return buffer;
        }

        //Функция возвращает массив, где индекс - это номер точки в массиве точек поданом на вход, а значение - количество использований данной точки в конкретной плоскости
        public static int[] CountUsingOfPoints(List<Vertex> vertices, List<Face> faces)
        {
            int[] listOfVertices = new int[vertices.Count];

            foreach (var tmp in faces)
            {
                listOfVertices[tmp.IndexOfVertex1 - 1]++;
                listOfVertices[tmp.IndexOfVertex2 - 1]++;
                listOfVertices[tmp.IndexOfVertex3 - 1]++;
            }

            return listOfVertices;
        }
       
        public static int DeleteLonelyTriangle(List<Vertex> vertices, List<Face> faces, int depth)
        {
            int countOfDeletingFaces = 0;
            for (int i = 0; i < depth; i++)
            {
                List<Face> forDeleting = new List<Face>();
                int[] buffer = CountUsingOfPoints(vertices, faces);
                //получение номеров одиноких треугольников
                for (int j = 0; j < faces.Count; j++)
                {
                    bool IsLonely = ((buffer[faces[j].IndexOfVertex1 - 1] < 2) && (buffer[faces[j].IndexOfVertex2 - 1] < 2) && (buffer[faces[j].IndexOfVertex3 - 1] < 2));
                    if (depth > 1)
                    {
                        IsLonely = IsLonely || ((buffer[faces[j].IndexOfVertex1 - 1] < 2) && (buffer[faces[j].IndexOfVertex2 - 1] < 2));
                        IsLonely = IsLonely || ((buffer[faces[j].IndexOfVertex2 - 1] < 2) && (buffer[faces[j].IndexOfVertex3 - 1] < 2));
                        IsLonely = IsLonely || ((buffer[faces[j].IndexOfVertex1 - 1] < 2) && (buffer[faces[j].IndexOfVertex3 - 1] < 2));
                    }
                    if (IsLonely)
                    {
                        forDeleting.Add(faces[j]);
                        buffer[faces[j].IndexOfVertex1 - 1]--;
                        buffer[faces[j].IndexOfVertex2 - 1]--;
                        buffer[faces[j].IndexOfVertex3 - 1]--;
                    }
                }
                //удаление одинокостоящих треугольников
                foreach (var tmp in forDeleting)
                {
                    faces.Remove(tmp);
                }
                countOfDeletingFaces += forDeleting.Count;
            }
            return countOfDeletingFaces;
        }

        public static void SearchLostVerticalPoint(List<List<int>> listOfIndexFacesUsingPoints, List<Vertex> vertieces, List<Face> faces, List<Face> verticalFaces, int depth)
        {
            bool[] containsCheck = new bool[faces.Count];
            for (int i = 0; i < containsCheck.Length; i++)
            {
                containsCheck[i] = false;
            }
            foreach (var tmp in verticalFaces)
            {
                containsCheck[tmp.BaseNumber] = true;
            }
            for (int i = 0; i < depth; i++) {
                List<int> buffer = new List<int>();
                int[] usingOfPoints = CountUsingOfPoints(vertieces, verticalFaces);

                for (int j = 0; j < listOfIndexFacesUsingPoints.Count; j++)
                {
                    if (usingOfPoints[j] > 0)
                    {
                        foreach (var part in listOfIndexFacesUsingPoints[j])
                        {
                            if (!buffer.Contains(part))
                            {
                                buffer.Add(part);
                            }
                        }
                    }
                }
                foreach (var tmp in buffer)
                {
                    if (!containsCheck[tmp])
                    {
                        if (faces[tmp].Normal.NearToVertical(0.2))
                        {
                            verticalFaces.Add(faces[tmp]);
                            containsCheck[tmp] = true;
                        }
                    }
                }
            }
        }

        public static void SearchLostHorizontalPoint(List<List<int>> listOfIndexFacesUsingPoints, List<Vertex> vertieces, List<Face> faces, List<Face> HorizontalFaces, int depth)
        {
            bool[] containsCheck = new bool[faces.Count];
            for (int i = 0; i < containsCheck.Length; i++)
            {
                containsCheck[i] = false;
            }
            foreach (var tmp in HorizontalFaces)
            {
                containsCheck[tmp.BaseNumber] = true;
            }
            for (int i = 0; i < depth; i++)
            {
                List<int> buffer = new List<int>();
                int[] usingOfPoints = CountUsingOfPoints(vertieces, HorizontalFaces);

                for (int j = 0; j < listOfIndexFacesUsingPoints.Count; j++)
                {
                    if (usingOfPoints[j] > 0)
                    {
                        foreach (var part in listOfIndexFacesUsingPoints[j])
                        {
                            if (!buffer.Contains(part))
                            {
                                buffer.Add(part);
                            }
                        }
                    }
                }

                foreach (var tmp in buffer)
                {
                    if (!containsCheck[tmp])
                    {
                        if (faces[tmp].Normal.NearToHorizontal(0.2))
                        {
                            HorizontalFaces.Add(faces[tmp]);
                            containsCheck[tmp] = true;
                        }
                    }
                }
            }
        }


        
        public static List<List<Face>> ExperementalGetAllSurface(List<List<int>> listOfIndexFacesUsingPoints,List<Face> faces, List<Face> currentFaces, int accuracy)
        {
            bool[] mapOfUsingFaces = new bool[faces.Count];
            for (int i = 0; i < mapOfUsingFaces.Length; i++)
            {
                mapOfUsingFaces[i] = false;
            }
            foreach (var tmp in currentFaces)
            {
                mapOfUsingFaces[tmp.BaseNumber] = true;
            }

            List<List<Face>> result = new List<List<Face>>();

            for (int i = 0; i < mapOfUsingFaces.Length; i++)
            {
                if (mapOfUsingFaces[i])
                {
                    mapOfUsingFaces[i] = false;
                    List<Face> tmp = new List<Face>();
                    Queue<int> quenue = new Queue<int>();
                    quenue.Enqueue(i);
                    int workInt;
                    while (quenue.TryDequeue(out workInt))
                    {
                        tmp.Add(faces[workInt]);
                        foreach (var buf in listOfIndexFacesUsingPoints[faces[workInt].IndexOfVertex1 - 1])
                        {
                            if (mapOfUsingFaces[buf])
                            {
                                mapOfUsingFaces[buf] = false;
                                quenue.Enqueue(buf);
                            }
                        }
                        foreach (var buf in listOfIndexFacesUsingPoints[faces[workInt].IndexOfVertex2 - 1])
                        {
                            if (mapOfUsingFaces[buf])
                            {
                                mapOfUsingFaces[buf] = false;
                                quenue.Enqueue(buf);
                            }
                        }
                        foreach (var buf in listOfIndexFacesUsingPoints[faces[workInt].IndexOfVertex3 - 1])
                        {
                            if (mapOfUsingFaces[buf])
                            {
                                mapOfUsingFaces[buf] = false;
                                quenue.Enqueue(buf);
                            }
                        }
                    }
                    if (tmp.Count > accuracy)
                        result.Add(tmp);
                }
                

            }
            return result;
        }

    }
}
