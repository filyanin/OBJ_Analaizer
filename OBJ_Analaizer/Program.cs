using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace OBJ_Analaizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a file path");

            string filePath = Console.ReadLine();
            string line = "";

            List<Vertex> vertices = new List<Vertex>();
            List<Vertex> vertexNormal = new List<Vertex>();
            List<Face> faces = new List<Face>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                int number = 0;
                while (!sr.EndOfStream)
                { 
                    line = sr.ReadLine().Replace("  ", " ").Replace(".", ",");
                    if (!Parser.TryParseStringToVertex(line, vertices))
                        if (!Parser.TryParseStringToVertexNormal(line, vertexNormal))
                            if (Parser.TryParseToFace(line, faces, number))
                                number++;

                }
            }

            foreach (var tmp in faces)
            {
                tmp.Normal = new Normal(vertices[tmp.IndexOfVertex1 - 1], vertices[tmp.IndexOfVertex2 - 1], vertices[tmp.IndexOfVertex3 - 1]);
            }

            List<Face> verticalFaces = FacesAnalizer.SearchVerticalFaces(faces, 0.005);
            List<Face> horizontalFaces = FacesAnalizer.SearchHorizontalFaces(faces, 0.005);
                          
            Console.WriteLine("Вертикальные треугольники " + verticalFaces.Count);
            Console.WriteLine("Горизонтальные треугольники " + horizontalFaces.Count);
            Console.WriteLine("Общее количество " +  faces.Count);
           
            List<List<int>> listOfIndexFacesUsingPoints = FacesAnalizer.GetListOfTrianglesUsingPoint(vertices, faces);
            Console.WriteLine("Из вертикального массива удалено: "+ FacesAnalizer.DeleteLonelyTriangle(vertices, verticalFaces, 12));

            Console.WriteLine("Из горизонтального массива удалено: " + FacesAnalizer.DeleteLonelyTriangle(vertices, horizontalFaces, 5));

            FacesAnalizer.SearchLostHorizontalPoint(listOfIndexFacesUsingPoints, vertices, faces, horizontalFaces, 5);
            Collector.WriteToObjFile(horizontalFaces, vertices, vertexNormal, "horizontal.obj");
            Console.WriteLine("Горизонтальный массив записан");
            FacesAnalizer.SearchLostVerticalPoint(listOfIndexFacesUsingPoints, vertices, faces, verticalFaces, 14);
            Collector.WriteToObjFile(verticalFaces, vertices, vertexNormal, "vertical.obj");
            Console.WriteLine("Вертикальный массив записан");
            List<Face> otherFaces = FacesAnalizer.SearchOtherFaces(faces, horizontalFaces, verticalFaces);
            Collector.WriteToObjFile(otherFaces, vertices, vertexNormal, "other.obj");
            Console.WriteLine("Массив иных треугольников записан");
            

            Collector.ExperementalWriteAllPartOfSurface(faces, verticalFaces, vertices, vertexNormal, listOfIndexFacesUsingPoints,100, "vertival_facies");
            Collector.ExperementalWriteAllPartOfSurface(faces, horizontalFaces, vertices, vertexNormal, listOfIndexFacesUsingPoints,100, "horizontal_facies");
            Collector.ExperementalWriteAllPartOfSurface(faces, otherFaces, vertices, vertexNormal, listOfIndexFacesUsingPoints,100, "other_facies");
            /* Collector.WriteAllPartOfSurface(verticalFaces, vertices, vertexNormal, 50, "vertival_facies");
             Collector.WriteAllPartOfSurface(horizontalFaces, vertices, vertexNormal, 50, "horizontal_facies");
             Collector.WriteAllPartOfSurface(otherFaces, vertices, vertexNormal, 50, "other_facies");*/

        }
    }
}
