using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OBJ_Analaizer
{
    public static class Parser
    {        
        static bool IsTarget(string target, string source)
        {
            if (target == source)
                return true;
            return false;
        }

        static string[] parseString(string str, string splitString)
        {
            string[] tmp;
            str = str.Replace(".", ",");
            while (str != str.Replace("  ", " "))
                str = str.Replace("  ", " ");
            tmp = str.Split(splitString);
            return tmp;
        }
        static public Dictionary<int, Vertex> parseToVOrVn(string str, string type)//ret null if not v or vn
        {
            if ((type == "v") || (type == "vn"))
            {
                Dictionary<int, Vertex> dict = new Dictionary<int, Vertex>(1);
                string line;
                string[] parts;

                StreamReader sr = new StreamReader(str);

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    parts = parseString(line, " ");

                    if (IsTarget(type, parts[0]))
                    {
                        dict.Add(dict.Count + 1, new Vertex(Convert.ToDouble(parts[1]), Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3])));
                    }
                }
                sr.Close();
                return dict;
            }
            return null;
        }

        static public Dictionary<int, Face> parseToFace(string str)
        {
            Dictionary<int, Face> dict = new Dictionary<int, Face>(1);
            string line;
            string[] parts;
            string[] numbers1;
            string[] numbers2;
            string[] numbers3;

            StreamReader sr = new StreamReader(str);

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                parts = parseString(line, " ");

                if (IsTarget("f", parts[0]))
                {
                    numbers1 = parseString(parts[1],"/");
                    numbers2 = parseString(parts[2], "/");
                    numbers3 = parseString(parts[3], "/");

                    Face temp = new Face(Convert.ToInt32(numbers1[0]), Convert.ToInt32(numbers2[0]), Convert.ToInt32(numbers3[0]), Convert.ToInt32(numbers1[2]), Convert.ToInt32(numbers2[2]), Convert.ToInt32(numbers3[2]), 1, 1, 1);
                    dict.Add(dict.Count + 1, temp);
                }
            }
            sr.Close();
            return dict;
        }


    }
}
