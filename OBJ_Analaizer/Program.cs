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
            Dictionary<int, Vertex> dictVertex =  Parser.parseToVertex(str);
            Dictionary<int, Vertex> dictVertexNormal = Parser.parseToVertex(str);
            Dictionary<int, Face> dictFace = Parser.parseToFace(str);
            Dictionary<int, Face> dictFaceTest = new Dictionary<int, Face>(1);
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

                if (temp.Value.Normal.NearToHorizontal(0.1))
                {
                    counter1++;
                    dictFaceTest.Add(counter1 + 1, temp.Value);
                } else if (temp.Value.Normal.NearToVertical(0.1)){
                    counter2++;
                }
                else
                    counter3++;
            }
            Console.WriteLine(counter1);
            Console.WriteLine(counter2);
            Console.WriteLine(counter3);
            Console.WriteLine(dictFace.Count);

            StreamWriter swr = new StreamWriter("test.obj");
            foreach (var temp in dictVertex)
            {
                string st = "v  " + temp.Value.X + " " + temp.Value.Y + " " + temp.Value.Z;
                st = st.Replace(",", ".");
                swr.WriteLine(st);
            }
            foreach (var temp in dictVertexNormal)
            {
                string st = "vn " + temp.Value.X + " " + temp.Value.Y + " " + temp.Value.Z;
                st = st.Replace(",", ".");
                swr.WriteLine(st);
            }
            foreach (var temp in dictFaceTest)
            {
                string buf1 = temp.Value.IndexOfVertex1+"/1/"+temp.Value.IndexOfVertexNormal1;
                string buf2 = temp.Value.IndexOfVertex2 + "/1/" + temp.Value.IndexOfVertexNormal2;
                string buf3 = temp.Value.IndexOfVertex3 + "/1/" + temp.Value.IndexOfVertexNormal3;
                string st = "f " + buf1 + " " + buf2 + " " + buf3;
                st = st.Replace(",", ".");
                swr.WriteLine(st);
            }
            swr.Close();
            Console.ReadLine();
        }
    }
}
