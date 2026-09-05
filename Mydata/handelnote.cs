using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ap_project.Mydata
{
    public class handelnote
    {
        public static string filepath = "note.json";
        public static Dictionary<DateTime, string> notes = new Dictionary<DateTime, string>();

        static handelnote()
        {
            readfromfile();
        }

        public static void savenote(DateTime x , string y)
        {
            notes[x] = y;
            savetofile();


        }

        public static string loadnote(DateTime z)
        {
            if (notes.ContainsKey(z))
            {
                return notes[z];
            }
            return string.Empty;
        }

        public static void savetofile()
        {
            string text = JsonConvert.SerializeObject(notes, Formatting.Indented);
            File.WriteAllText(filepath, text);
        }

        public static void readfromfile()
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);
                notes = JsonConvert.DeserializeObject<Dictionary<DateTime, string>>(json) ?? new Dictionary<DateTime, string>();
            }
        }


    }
}
