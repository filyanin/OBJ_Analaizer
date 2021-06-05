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
        public static void CreateObjFile(string nameOfFile, Dictionary<int, Vertex> vertexDictionary, Dictionary<int, Vertex> NormalVertexDictionary, Dictionary<int,Face> FaceDictionary)
        {
            StreamWriter swr = new StreamWriter(nameOfFile);
            foreach (var temp in vertexDictionary)
            {
                string st = "v  " + temp.Value.X + " " + temp.Value.Y + " " + temp.Value.Z;
                st = st.Replace(",", ".");
                swr.WriteLine(st);
            }
            foreach (var temp in NormalVertexDictionary)
            {
                string st = "vn " + temp.Value.X + " " + temp.Value.Y + " " + temp.Value.Z;
                st = st.Replace(",", ".");
                swr.WriteLine(st);
            }
            foreach (var temp in FaceDictionary)
            {
                string buf1 = temp.Value.IndexOfVertex1 + "/1/" + temp.Value.IndexOfVertexNormal1;
                string buf2 = temp.Value.IndexOfVertex2 + "/1/" + temp.Value.IndexOfVertexNormal2;
                string buf3 = temp.Value.IndexOfVertex3 + "/1/" + temp.Value.IndexOfVertexNormal3;
                string st = "f " + buf1 + " " + buf2 + " " + buf3;
                st = st.Replace(",", ".");
                swr.WriteLine(st);
            }
        }

        public static Dictionary<int, Vertex> getNewVertexDictionary(Dictionary<int,Vertex> lastDictionaryOfVertex, Dictionary<int,Face> faceDictionaryForChange,List<int> listOfUsingPoints)
        {
            Dictionary<int, Vertex> buf = new Dictionary<int, Vertex>(1);
 
            for (int i = 0; i < listOfUsingPoints.Count; i++)
            {
                int bufferIndex = listOfUsingPoints[i];

                Vertex vertexTmp;
                if (lastDictionaryOfVertex.TryGetValue(listOfUsingPoints[i], out vertexTmp))
                {
                    buf.Add(i + 1, vertexTmp);

                foreach (var tmp in faceDictionaryForChange)
                    {
                        if (tmp.Value.IndexOfVertex1 == listOfUsingPoints[i])
                        {
                            tmp.Value.IndexOfVertex1 = i + 1;
                        }
                        if (tmp.Value.IndexOfVertex2 == listOfUsingPoints[i])
                        {
                            tmp.Value.IndexOfVertex2 = i + 1;
                        }
                        if (tmp.Value.IndexOfVertex3 == listOfUsingPoints[i])
                        {
                            tmp.Value.IndexOfVertex3 = i + 1;
                        }
                    }
                }
            } 
            return buf;
        }

        public static Dictionary<int, Vertex> getNewVertexNormalDictionary(Dictionary<int, Vertex> lastDictionaryOfVertexNormal, Dictionary<int, Face> faceDictionaryForChange, List<int> listOfUsingPoints)
        {
            Dictionary<int, Vertex> buf = new Dictionary<int, Vertex>(1);

            for (int i = 0; i < listOfUsingPoints.Count; i++)
            {
                int bufferIndex = listOfUsingPoints[i];

                Vertex VertexNormalTmp;
                if (lastDictionaryOfVertexNormal.TryGetValue(listOfUsingPoints[i], out VertexNormalTmp))
                {
                    buf.Add(i + 1, VertexNormalTmp);

                    foreach (var tmp in faceDictionaryForChange)
                    {
                        if (tmp.Value.IndexOfVertexNormal1 == listOfUsingPoints[i])
                        {
                            tmp.Value.IndexOfVertexNormal1 = i + 1;
                        }
                        if (tmp.Value.IndexOfVertexNormal2 == listOfUsingPoints[i])
                        {
                            tmp.Value.IndexOfVertexNormal2 = i + 1;
                        }
                        if (tmp.Value.IndexOfVertexNormal3 == listOfUsingPoints[i])
                        {
                            tmp.Value.IndexOfVertexNormal3 = i + 1;
                        }
                    }
                }
            }
            return buf;
        }

        public static void FinalOperationSet(string nameOfFile,  Dictionary<int, Face> newFaceDictionary, Dictionary<int, Vertex> oldVertexDictionary, Dictionary<int, Vertex> oldVertexNormalDictionary)
        {
            List<int> ArrayOfVertexIndex = Collector.GetArrayOfVertexIndex(newFaceDictionary);
            List<int> ArrayOfVertexNormalIndex = Collector.GetArrayOfVertexNormalIndex(newFaceDictionary);

            Dictionary<int, Vertex> newVertex = getNewVertexDictionary(oldVertexDictionary, newFaceDictionary, ArrayOfVertexIndex);
            Dictionary<int, Vertex> newVertexNormal = getNewVertexNormalDictionary(oldVertexNormalDictionary, newFaceDictionary, ArrayOfVertexNormalIndex);

            Collector.CreateObjFile(nameOfFile, newVertex, newVertexNormal, newFaceDictionary);
        }
    }
}
