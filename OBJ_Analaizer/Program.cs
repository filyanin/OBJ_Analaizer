using System;
using System.Collections.Generic;
using System.IO;

namespace OBJ_Analaizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a file name");
            string str = Console.ReadLine();
            Dictionary<int, Vertex> dictVertex = Parser.parseToVOrVn(str, "v");
            Dictionary<int, Vertex> dictVertexNormal = Parser.parseToVOrVn(str, "vn");
            Dictionary<int, Face> dictFace = Parser.parseToFace(str);
            Dictionary<int, Face> dictFaceVertical = new Dictionary<int, Face>(1);
            Dictionary<int, Face> dictFaceHorizontal = new Dictionary<int, Face>(1);
            int counter1 = 0;
            int counter2 = 0;
            int counter3 = 0;
            foreach (var temp in dictFace)
            {
                Vertex v1;
                Vertex v2;
                Vertex v3;
                dictVertex.TryGetValue(temp.Value.IndexOfVertex1, out v1);
                dictVertex.TryGetValue(temp.Value.IndexOfVertex2, out v2);
                dictVertex.TryGetValue(temp.Value.IndexOfVertex3, out v3);

                temp.Value.CountNormalVector(v1, v2, v3);

                if (temp.Value.Normal.NearToVertical(0.1))
                {
                    counter1++;
                    dictFaceVertical.Add(counter1 + 1, temp.Value);
                } else if (temp.Value.Normal.NearToHorizontal(0.1)){
                    counter2++;
                    dictFaceHorizontal.Add(counter2 + 1, temp.Value);
                }
                else
                    counter3++;
            }
            Console.WriteLine("Вертикальные треугольники " + counter1);
            Console.WriteLine("Горизонтальные треугольники " + counter2);
            Console.WriteLine("Другие треугольники " + counter3);
            Console.WriteLine("Общее количество " + dictFace.Count);


            Collector.CreateObjFile("Vertical.obj", dictFaceVertical, dictVertex, dictVertexNormal);
            Collector.CreateObjFile("Horizontal.obj", dictFaceHorizontal, dictVertex, dictVertexNormal);

           
           
            Console.ReadLine();
        }
    }
}
