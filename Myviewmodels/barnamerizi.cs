using Ap_project.Mydata;
using Ap_project.MyModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ap_project.Myviewmodels
{
    public class barnamerizi : INotifyPropertyChanged
    {
        private ObservableCollection<roeedad> roeedads;
        private string title1;
        private DateTime selecteddate1=DateTime.Now;
        private TimeSpan selectedtime1=DateTime.Now.TimeOfDay;
        private int index;
        private roeedad selecting1;
        private Weekshow weeknow1;
    

        public ObservableCollection<roeedad> Roeedads
        {
            get => roeedads;
            set
            {
                roeedads = value;
                OnPropertyChanged();
            }
        }

        public barnamerizi()
        {
            Roeedads = new ObservableCollection<roeedad>(handeldata.Bazyabeedata());
                weeknow = Weekly();
            
            
        }

        public string title { get => title1;
            set { title1 = value;
                OnPropertyChanged();}
        }

        public DateTime selecteddate { get => selecteddate1;
            set { selecteddate1 = value;
                OnPropertyChanged();
            } 
        }
        public TimeSpan selectedtime { get => selectedtime1;
            set { selectedtime1 = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Showtime));
            } 
        }
        public string Showtime
        {
            get => selectedtime.ToString(@"hh\:mm");
            set
            {
                if(TimeSpan.TryParse(value,out TimeSpan result)){
                    selectedtime = result;
                };
            }
        }



        public roeedad selecting
        {
            get => selecting1;
            set
            {
                if (selecting1 == value) return;

                selecting1 = value;
                OnPropertyChanged();

                if (value != null)
                {
                    title = value.Title;
                    selecteddate = value.StartDateTimeUtc;
                    selectedtime = value.StartDateTimeUtc.TimeOfDay;
                }
            }
        }
  

        public void Additem()
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Title should be written!");
                return;
            }
            if ((selectedtime.TotalHours >= 24) && (selectedtime.TotalMinutes >= 60))
            {
                MessageBox.Show("The time is not valid !");
                return;
            }
            int newid = 1;
            foreach(var r in Roeedads)
            {
                if (r.ID >= newid)
                {
                    newid = r.ID + 1;
                }
            }
            DateTime dateTime = selecteddate.Date.Add(selectedtime);
            var newroeedad = new roeedad
            {
                ID = newid,
                Title = title,
                StartDateTimeUtc = dateTime,
                DurationMinutes = 60
            };
            Roeedads.Add(newroeedad);
            handeldata.sabtdata(Roeedads.ToList());

            title = string.Empty;
            selecteddate = DateTime.Now;
            selectedtime = DateTime.Now.TimeOfDay;
            weeknow = Weekly();
            OnPropertyChanged(nameof(weeknow));
        }
        public int selecteditem { get => index; 
            set { index = value;
                OnPropertyChanged();
            } 
        }
        public void Deleteitem()
        {   if((index>=0)&&(index<Roeedads.Count))
            {
                Roeedads.Remove(selecting);
                handeldata.sabtdata(Roeedads.ToList());
                title = string.Empty;
                selecteddate = DateTime.Now;
                selectedtime = DateTime.Now.TimeOfDay;
                weeknow = Weekly();
                OnPropertyChanged(nameof(weeknow));
            }
            

        }
        public void Edititem()
        {   if (selecting == null)
            {
                return;
            }
            if ((selectedtime.TotalHours >= 24) && (selectedtime.TotalMinutes >= 60))
            {
                MessageBox.Show("The time is not valid !");
                return;
            }
            DateTime dateTime = selecteddate.Date.Add(selectedtime);
            selecting.Title = title;
            selecting.StartDateTimeUtc = dateTime ;
            handeldata.sabtdata(Roeedads.ToList());
            title = string.Empty;
            selecteddate = DateTime.Now;
            selectedtime = DateTime.Now.TimeOfDay;
            weeknow = Weekly();
            OnPropertyChanged(nameof(weeknow));
        }

        public Weekshow Weekly()
        {
            var week = new Weekshow();
            var startDate = weeknow?.startdate ?? DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            week.startdate = startDate;

            for(int j = 0; j < 7; j++)
            {
                week.roeedadharrooz[j].Clear();
            }
            for(int i = 0; i < 7; i++)
            {
                var day = week.startdate.AddDays(i);
                var dayEvents = new List<roeedad>();
                foreach (var r in Roeedads)
                {
                    if (r.StartDateTimeUtc.Date == day.Date)
                    {
                        dayEvents.Add(r);
                    }
                }
                dayEvents.Sort((x, y) => x.StartDateTimeUtc.TimeOfDay.CompareTo(y.StartDateTimeUtc.TimeOfDay));
                foreach (var item in dayEvents)
                {
                    week.roeedadharrooz[i].Add(item);
                }

            }
            week.weeknote = handelnote.loadnote(startDate);
           

            return week;
        }

        public Weekshow weeknow { get => weeknow1;
            set { weeknow1 = value;
                OnPropertyChanged();
            }
        }

        public void nextweek()
        {
            weeknow.startdate = weeknow.startdate.AddDays(7);
            weeknow = Weekly();
            OnPropertyChanged(nameof(weeknow));
            OnPropertyChanged(nameof(weeknow.roeedadharrooz));
        }

        public void lastweek()
        {
            weeknow.startdate = weeknow.startdate.AddDays(-7);
            weeknow = Weekly();
            OnPropertyChanged(nameof(weeknow));
            OnPropertyChanged(nameof(weeknow.roeedadharrooz));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
