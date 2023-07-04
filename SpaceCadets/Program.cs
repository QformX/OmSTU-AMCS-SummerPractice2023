using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpaceCadets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = args[0];
            string outputFilePath = args[1];
            string inputFile;
            string outputFile;
            dynamic jsonObject = new JObject();
            List<dynamic>? outputObject = new List<dynamic>();

            using(StreamReader sr = new(inputFilePath))
            {
                inputFile = sr.ReadToEnd();
            }

            if (inputFile is not null)
            {
                jsonObject = JsonConvert.DeserializeObject(inputFile) ?? new JObject();
            }
            else
            {
                throw new Exception("input file was null");
            }

            List<Cadet> cadets = jsonObject.data.ToObject<List<Cadet>>();
            string taskName = jsonObject.taskName.ToString();

            if (taskName == "GetStudentsWithHighestGPA")
            {
                outputObject = GetStudentsWithHighestGPA(cadets);
            }
            else if (taskName == "CalculateGPAByDiscipline")
            {
                outputObject = CalculateGPAByDiscipline(cadets);
            }
            else if (taskName == "GetBestGroupsByDiscipline")
            {
                outputObject = GetBestGroupsByDiscipline(cadets);
            }

            outputFile = JsonConvert.SerializeObject(new { Response = outputObject }, Formatting.Indented);

            using(StreamWriter sw = new(outputFilePath))
            {
                sw.Write(outputFile);
            }
        }

        public static List<dynamic> GetStudentsWithHighestGPA(List<Cadet> cadets)
        {
            var cadetsArrayWithMarks = cadets.GroupBy(p => p.Name).Select(m => new
            {
                Name = m.Key,
                Marks = m.Select(v => v.Mark).ToArray()
            });

            var highGPA = cadetsArrayWithMarks.Max(c => c.Marks.Sum() / c.Marks.Length);

            var cadetsWithHighestGPA = cadetsArrayWithMarks.Where(c => c.Marks.Sum() / c.Marks.Length == highGPA).Select(m => new
            {
                m.Name,
                Mark = Math.Round(m.Marks.Sum() / m.Marks.Length, 2)
            }).ToList<dynamic>();

            return cadetsWithHighestGPA;
        }

        public static List<dynamic> CalculateGPAByDiscipline(List<Cadet> cadets)
        {
            var disciplinesWithMarks = cadets.GroupBy(p => p.Discipline).Select(m => new
            {
                Discipline = m.Key,
                Marks = m.Select(p => p.Mark).ToArray()
            });

            var disciplinesWithGPA = disciplinesWithMarks.Select(m => new
            {
                m.Discipline,
                GPA = Math.Round(m.Marks.Sum() / m.Marks.Length, 3)
            }).ToList<dynamic>();

            return disciplinesWithGPA;
        }

        public static List<dynamic> GetBestGroupsByDiscipline(List<Cadet> cadets)
        {
            var cadetsByGroup = cadets.GroupBy(p => new {
                p.Discipline,
                p.Group
            }).Select(g => new
            {
                g.Key.Discipline,
                g.Key.Group,
                Mark = g.Average(v => v.Mark)
            }).GroupBy(p => p.Discipline)
            .Select(q => new
            {
                Discipline = q.Key,
                Group = q.Where(k => k.Mark == q.Max(z => z.Mark)).Select(k => k.Group).ToArray()[0],
                GPA = q.Max(k => k.Mark)
            }).ToList<dynamic>();

            return cadetsByGroup;
        }
    }

    internal class Cadet
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public string Discipline { get; set; }
        public double Mark { get; set; }

        public Cadet(string name, string group, string discipline, int mark)
        {
            Name = name;
            Group = group;
            Discipline = discipline;
            Mark = mark;
        }
    }
}