using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OBJ_Analaizer
{
    public class Collector
    {
        public static List<int> GetArrayOfVertexIndex(Dictionary<int, Face> dict)
        {
            List<int> buffer = new List<int>();

            foreach (var buf in dict)
            {
                int index = buf.Value.IndexOfVertex1;
                if (!buffer.Contains(index))
                {
                    buffer.Add(index);
                }
                index = buf.Value.IndexOfVertex2;
                if (!buffer.Contains(index))
                {
                    buffer.Add(index);
                }
                index = buf.Value.IndexOfVertex3;
                if (!buffer.Contains(index))
                {
                    buffer.Add(index);
                }
            }
            buffer.Sort();
            StreamWriter swr = new StreamWriter("vertex.txt");
            foreach (var tmp in buffer)
            {
                swr.WriteLine(tmp);
            }
            swr.Close();
            return buffer;
        }

        public static List<int> GetArrayOfVertexNormalIndex(Dictionary<int, Face> dict)
        {
            List<int> buffer = new List<int>();

            foreach (var buf in dict)
            {
                int index = buf.Value.IndexOfVertexNormal1;
                if (!buffer.Contains(index))
                {
                    buffer.Add(index);
                }

                index = buf.Value.IndexOfVertexNormal2;
                if (!buffer.Contains(index))
                {
                    buffer.Add(index);
                }

                index = buf.Value.IndexOfVertexNormal3;
                if (!buffer.Contains(index))
                {
                    buffer.Add(index);
                }
            }

            buffer.Sort();
            StreamWriter swr = new StreamWriter("vertexNormal.txt");
            foreach (var tmp in buffer)
            {
                swr.WriteLine(tmp);
            }
            swr.Close();
            return buffer;
            
        }

        public static void ChangeFaceDictionary(Dictionary<int, Face> dictionaryToChange, List<int> newArrayOfVertex, List<int> newArrayOfVertexNormal)
        {
            foreach (var tmp in dictionaryToChange)
            {
                tmp.Value.IndexOfVertex1 = newArrayOfVertex.IndexOf(tmp.Value.IndexOfVertex1) + 1;
                tmp.Value.IndexOfVertex2 = newArrayOfVertex.IndexOf(tmp.Value.IndexOfVertex2) + 1;
                tmp.Value.IndexOfVertex3 = newArrayOfVertex.IndexOf(tmp.Value.IndexOfVertex3) + 1;

                tmp.Value.IndexOfVertexNormal1 = newArrayOfVertexNormal.IndexOf(tmp.Value.IndexOfVertexNormal1) + 1;
                tmp.Value.IndexOfVertexNormal2 = newArrayOfVertexNormal.IndexOf(tmp.Value.IndexOfVertexNormal2) + 1;
                tmp.Value.IndexOfVertexNormal3 = newArrayOfVertexNormal.IndexOf(tmp.Value.IndexOfVertexNormal3) + 1;
            }
        }


        public static bool WriteVertexOrVertexNormalToObj(Dictionary<int, Vertex> dictionary, List<int> array, string type, StreamWriter sw)
        {
            if ((type == "v")||(type == "vn"))
            {
                for (int i = 0; i < array.Count; i++)
                {
                    Vertex point;
                    dictionary.TryGetValue(array[i], out point);
                    string str = type + " " + point.ToString();
                    sw.WriteLine(str);
                }
                if (type == "v")
                {
                    sw.WriteLine("# " + (array.Count + 1) + " vertices");
                }
                else
                {
                    sw.WriteLine("# " + (array.Count + 1) + " vertex normals");
                }
                sw.WriteLine();
                return true;
            }
            return false;
        }

        public static void WriteFaceToObj(Dictionary<int,Face> dictionary, string name, StreamWriter sw)
        {
            sw.WriteLine("o " + name);
            sw.WriteLine("g " + name);
            sw.WriteLine("usemtl default");
            sw.WriteLine("s 1");
            foreach (var tmp in dictionary)
            {
                sw.WriteLine("f " + tmp.Value.ToString());
            }

            sw.WriteLine("# 0 polygons - " + dictionary.Count + " triangles");
        }
        public static void WriteToObjFile(string nameOfFile, Dictionary<int, Vertex> vertexDictionary, Dictionary<int, Vertex> vertexNormalDictionary, Dictionary<int,Face> FaceDictionary, List<int> arrayOfNewVertex, List<int> arrayOfNewVertexNormal)
        {
            StreamWriter swr = new StreamWriter(nameOfFile);
            WriteVertexOrVertexNormalToObj(vertexDictionary, arrayOfNewVertex, "v", swr);
            WriteVertexOrVertexNormalToObj(vertexNormalDictionary, arrayOfNewVertexNormal, "vn", swr);
            WriteFaceToObj(FaceDictionary, nameOfFile, swr);
            swr.Close();
        }
        public static void CreateObjFile(string nameOfFile,  Dictionary<int, Face> newFaceDictionary, Dictionary<int, Vertex> VertexDictionary, Dictionary<int, Vertex> VertexNormalDictionary)
        {
            List<int> ArrayOfVertexIndex = Collector.GetArrayOfVertexIndex(newFaceDictionary);
            List<int> ArrayOfVertexNormalIndex = Collector.GetArrayOfVertexNormalIndex(newFaceDictionary);

            ChangeFaceDictionary(newFaceDictionary, ArrayOfVertexIndex, ArrayOfVertexNormalIndex);

            
            WriteToObjFile(nameOfFile, VertexDictionary, VertexNormalDictionary, newFaceDictionary, ArrayOfVertexIndex, ArrayOfVertexNormalIndex);
        }
    }
}
