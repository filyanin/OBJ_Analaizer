using System;
using System.Collections.Generic;
using System.IO;

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
               
                while (!sr.EndOfStream)
                { 
                    line = sr.ReadLine().Replace("  ", " ").Replace(".", ",");
                    if (!Parser.TryParseStringToVertex(line, vertices))
                        if (!Parser.TryParseStringToVertexNormal(line, vertexNormal))
                            Parser.TryParseToFace(line, faces);

                }
            }

            foreach (var tmp in faces)
            {
                tmp.Normal = new Normal(vertices[tmp.IndexOfVertex1 - 1], vertices[tmp.IndexOfVertex2 - 1], vertices[tmp.IndexOfVertex3 - 1]);
            }

            List<Face> verticalFaces = FacesAnalizer.SearchVerticalFaces(faces, 0.4);
            List<Face> horizontalFaces = FacesAnalizer.SearchHorizontalFaces(faces, 0.01);

            //Начало теста применения

            //Удаление


            Console.WriteLine("Удалено из горизонтального массива" + FacesAnalizer.DeleteLonelyTriangle(horizontalFaces, 2));
            Console.WriteLine("Удалено из вертикального массива" + FacesAnalizer.DeleteLonelyTriangle(verticalFaces, 2));

            //Восстановление
             for (int i = 0; i < 1; i++)
             {
                 Console.WriteLine("hor: " + FacesAnalizer.SearchLostHorizontalPoint(faces, horizontalFaces));
                 Console.WriteLine("vert: " + FacesAnalizer.SearchLostVerticalPoint(faces, verticalFaces));
             }
            
            Console.WriteLine("Вертикальные треугольники " + verticalFaces.Count);
            Console.WriteLine("Горизонтальные треугольники " + horizontalFaces.Count);
            Console.WriteLine("Общее количество " +  faces.Count);

            Collector.WriteToObjFile(verticalFaces, vertices, vertexNormal, "vertical.obj");
            Collector.WriteToObjFile(horizontalFaces, vertices, vertexNormal, "horizontal.obj");

        }
    }
}
