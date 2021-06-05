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
        static public Dictionary<int, Vertex> parseToVertex(string str)
        {
            Dictionary<int, Vertex> dict = new Dictionary<int, Vertex>(1);
            string line;
            string target = "v";
            string[] parts;
            string[] numbers;

                StreamReader sr = new StreamReader(str);

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    parts = line.Split("  ");
                

                    if (IsTarget(target, parts[0]))
                    {
                    parts[1] = parts[1].Replace(".", ",");
                    numbers = parts[1].Split(' ');
                    dict.Add(dict.Count + 1, new Vertex(Convert.ToDouble(numbers[0]), Convert.ToDouble(numbers[1]), Convert.ToDouble(numbers[2])));
                    }
                }
            sr.Close();
            return dict;
        }

        static public Dictionary<int, Vertex> parseToVertexNormal(string str)
        {
            Dictionary<int, Vertex> dict = new Dictionary<int, Vertex>(1);
            string line;
            string target = "vn";
            string[] parts;

            StreamReader sr = new StreamReader(str);

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                line = line.Replace(".", ",");
                parts = line.Split(" ");


                if (IsTarget(target, parts[0]))
                {
                    dict.Add(dict.Count + 1, new Vertex(Convert.ToDouble(parts[1]), Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3])));
                }
            }
            sr.Close();
            return dict;
        }

        static public Dictionary<int, Face> parseToFace(string str)
        {
            Dictionary<int, Face> dict = new Dictionary<int, Face>(1);
            string line;
            string target = "f";
            string[] parts;
            string[] numbers1;
            string[] numbers2;
            string[] numbers3;

            StreamReader sr = new StreamReader(str);

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                parts = line.Split(" ");


                if (IsTarget(target, parts[0]))
                {
                    numbers1 = parts[1].Split('/');
                    numbers2 = parts[2].Split('/');
                    numbers3 = parts[3].Split('/');
                    Face temp = new Face(Convert.ToInt32(numbers1[0]), Convert.ToInt32(numbers2[0]), Convert.ToInt32(numbers3[0]), Convert.ToInt32(numbers1[2]), Convert.ToInt32(numbers2[2]), Convert.ToInt32(numbers3[2]), Convert.ToInt32(numbers1[1]), Convert.ToInt32(numbers2[1]), Convert.ToInt32(numbers3[1]));
                    dict.Add(dict.Count + 1, temp);
                }
            }
            sr.Close();
            return dict;
        }


    }
}
