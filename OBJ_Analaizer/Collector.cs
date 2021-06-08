using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OBJ_Analaizer
{
    public class Collector
    {
        
        public static Dictionary<int,Face> GetDictionaryOfFace(Dictionary<int,Face> oldFaceDictionary,List<int> listOfFace)
        {
            Dictionary<int, Face> res = new Dictionary<int, Face>(1);
            Face buf;
            foreach (var tmp in listOfFace)
            {
                oldFaceDictionary.TryGetValue(tmp, out buf);
                res.Add(res.Count + 1, buf);
            }

            return res;
        }

        public static void WriteVertexToObj(List<Vertex> vertices, StreamWriter swr)
        {

            foreach (var tmp in vertices)
            {
                swr.WriteLine("v " + tmp.ToString());
            }

            swr.WriteLine("# " + (vertices.Count) + " vertices");
        }

        public static void WriteVertexNormalToObj(List<Vertex> vertexNormal, StreamWriter swr)
        {

            foreach (var tmp in vertexNormal)
            {
                swr.WriteLine("vn " + tmp.ToString());
            }

            swr.WriteLine("# " + (vertexNormal) + " vertices");
        }


        public static void WriteFaceToObj(List<Face> faces, StreamWriter swr)
        {

            foreach (var tmp in faces)
            {
                swr.WriteLine(tmp.ToString());
            }

            swr.WriteLine("# 0 polygons - " + faces.Count + " triangles");
        }
        public static void WriteToObjFile(List<Face> faces, List<Vertex> vertices, List<Vertex> vertexNormal, string nameOfFile)
        {
            using (StreamWriter swr = new StreamWriter(nameOfFile))
            {
                WriteVertexToObj(vertices,swr);
                WriteVertexNormalToObj(vertexNormal, swr);
                WriteFaceToObj(faces, swr);
            }
        }

    }
}
