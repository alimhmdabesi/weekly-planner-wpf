using Ap_project.Mydata;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ap_project.MyModels
{
    public class Weekshow
    {
        private string weeknote1;
        public DateTime startdate { get; set; }

        public DateTime enddate => startdate.AddDays(6);
        public string monthyear => startdate.ToString("MMMM yyyy");
        public ObservableCollection<roeedad>[] roeedadharrooz { get; set; } = new ObservableCollection<roeedad>[7];
        public Weekshow()
        {
            for (int i = 0; i < 7; i++)
            {
                roeedadharrooz[i] = new ObservableCollection<roeedad>();
            }
        }
        public string weeknote { get => weeknote1;
            set {
                if (weeknote1 != value)
                {

                    weeknote1 = value;
                
                handelnote.savenote(startdate, value);
                }

            }
        }
    }
    
}
