using Ap_project.MyModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;


namespace Ap_project.Mydata
{
    public class handeldata
    {
        public static string masirfile = "dataroeedad.json";
        
        public static List<roeedad> Bazyabeedata()
        {
            if (!File.Exists(masirfile))
            {
                return new List<roeedad>();
            }

            string data = File.ReadAllText(masirfile);

            return JsonConvert.DeserializeObject<List<roeedad>>(data);
        }

        public static void sabtdata(List<roeedad> roeedads)
        {
            string newdata = JsonConvert.SerializeObject(roeedads);
            File.WriteAllText(masirfile, newdata);
        }
    }
}
