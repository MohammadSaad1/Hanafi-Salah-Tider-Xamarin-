using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace HanafiSalahTider
{
    public  class ReadFromTxtFile : INotifyPropertyChanged
    {
        string line = "";
        int i = 0;

        string[] prayertimes;
        private string[] prayertimesIsha;
        private string[] prayertimesFajr;

        public static Tider Instanstider { get; set; }

        public ReadFromTxtFile()
        {
            Instanstider = new Tider();
            getText(DateTime.Now, DateTime.Now, "HanafiSalahTider.salahtider.txt", "HanafiSalahTider.ishatid.txt");
            SalahNotification();
        }


        public void getText(DateTime dateTime, DateTime datetimeFajr, String timepath, String timepathIsha)
        {
            dateTime = DateTime.Now.AddDays(-1);

           String date = getDateString(dateTime);
            String dateFajr = getDateString(datetimeFajr);



            String fileContent = getStreamFromTxtFile(timepath);
          String fileContentIsha = getStreamFromTxtFile(timepathIsha);


            if (fileContent.Contains(date) || fileContentIsha.Contains(date))
      
            {

                string[] apts = fileContent.Split('\n').Where(x => x.Contains(date)).ToArray();
                string[] aptsIsha = fileContentIsha.Split('\n').Where(x => x.Contains(date)).ToArray();
                string[] aptsFajr = fileContent.Split('\n').Where(x => x.Contains(dateFajr)).ToArray();

                prayertimes = apts[0].Split(new[] { "   ", "  "}, StringSplitOptions.None);
                prayertimesFajr = aptsFajr[0].Split(new[] { "   ", "  " }, StringSplitOptions.None);
   
                prayertimesIsha = aptsIsha[0].Split(new[] { "   ", "  " }, StringSplitOptions.None);
                DateTime dt;

                string[] nytider = prayertimesFajr[3].Split();
                if (nytider[1].Trim() == "@7")
                {
                    getText(dateTime, datetimeFajr.AddDays(-1), timepath, timepathIsha);
                    nytider = prayertimesFajr[3].Split();

                   
                }

                dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, int.Parse(nytider[1]), int.Parse(nytider[2]), 0);

                Instanstider.Fajr = dt;
                Instanstider.Imsak = dt.AddMinutes(-4);

                nytider = prayertimes[4].Split();
                dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, int.Parse(nytider[1]), int.Parse(nytider[2]), 0);
                Instanstider.Shuruk = dt;

                nytider = prayertimes[5].Split();
                dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, int.Parse(nytider[1]), int.Parse(nytider[2]), 0);
                Instanstider.Dhuhr = dt.AddMinutes(5);

                nytider = prayertimes[7].Split();
                dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, int.Parse(nytider[1]), int.Parse(nytider[2]), 0);
                Instanstider.Asr = dt;

                nytider = prayertimes[8].Split();
                dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, int.Parse(nytider[1]), int.Parse(nytider[2]), 0);
                Instanstider.Maghrib = dt.AddMinutes(4);

                nytider = prayertimesIsha[9].Split();
                if (nytider[1].Trim() == "@7" )
                {
                    dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 0, 0);
                    SummerWinterTime(dateTime);

                    Instanstider.Isha = dt;

                }

                else { 
                    dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, int.Parse(nytider[1]), int.Parse(nytider[2]), 0);

                    Instanstider.Isha = dt;

                    SummerWinterTime(dateTime);

                    if (Instanstider.Isha.Hour >= 23 || Instanstider.Isha.Hour == 0)
                    {
                        Instanstider.Isha = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 0, 0);
                    }


                }


            }


        }

        public void SummerWinterTime(DateTime dateTime)
        {
            //  DateTime date1 = GetLastWeekdayOfMonth(new DateTime(DateTime.Now.Year, 10, 1, 2, 0, 0), new DateTime().AddDays(7).DayOfWeek);
            // DateTime date2 = GetLastWeekdayOfMonth(new DateTime(DateTime.Now.Year, 3, 1, 2, 0, 0), new DateTime().AddDays(7).DayOfWeek);
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();

            if (dateTime.Year == 2019)
            {
                 date1 = new DateTime(2019, 03, 31);
                 date2 = new DateTime(2019, 10, 27);
            }
            
            else if (dateTime.Year == 2020)
            {
                 date1 = new DateTime(2020, 03, 29);
                 date2 = new DateTime(2020, 10, 25);

            }


   

            if (dateTime >= date1 && dateTime < date2)
            {
                Instanstider.Imsak = Instanstider.Imsak.AddHours(1);

                Instanstider.Fajr =  Instanstider.Fajr.AddHours(1);
                Instanstider.Shuruk = Instanstider.Shuruk.AddHours(1);
                Instanstider.Dhuhr = Instanstider.Dhuhr.AddHours(1);
                Instanstider.Asr = Instanstider.Asr.AddHours(1);
                Instanstider.Isha = Instanstider.Isha.AddHours(1);
                Instanstider.Maghrib = Instanstider.Maghrib.AddHours(1);


            }

        }

        public String getDateString(DateTime dateTime)
        {
            string date = dateTime.Year + "   " + dateTime.Month + "   " + dateTime.Day;

            if (!(dateTime.Day > 0) || !(dateTime.Day < 10))
            {
                date = dateTime.Year + "   " + dateTime.Month + "  " + dateTime.Day;
            }

            else if (!(dateTime.Month > 0) || !(dateTime.Month < 10))
            {
                date = dateTime.Year + "  " + dateTime.Month + "   " + dateTime.Day;
            }

            else if (!(dateTime.Month > 0) || !(dateTime.Day < 10) || !((dateTime.Day > 0) || !(dateTime.Day < 10)))
            {
                date = dateTime.Year + "  " + dateTime.Month + "  " + dateTime.Day;
            }

            return date;
        }

        public String getStreamFromTxtFile(String textpath)
        {
            String fileContent;

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ReadFromTxtFile)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(textpath);


            using (StreamReader myreader = new StreamReader(stream))
            {
                fileContent = myreader.ReadToEnd();
            }

            return fileContent;

        }


        public static DateTime GetLastWeekdayOfMonth(DateTime date, DayOfWeek day)
        {
            DateTime lastDayOfMonth = new DateTime(date.Year, date.Month, 1)
                .AddMonths(1).AddDays(-1);
            int wantedDay = (int)day;
            int lastDay = (int)lastDayOfMonth.DayOfWeek;
            return lastDayOfMonth.AddHours(date.Hour).AddDays(
                lastDay >= wantedDay ? wantedDay - lastDay : wantedDay - lastDay - 7);
        }

        public void SalahNotification()
        {
            /*
            CrossLocalNotifications.Current.Show("Hayya' 'alaa salaah!", "Tid til Maghrib-bønnen!",1, Instanstider.Maghrib);
            CrossLocalNotifications.Current.Show("Hayya' 'alaa salaah!", "Tid til Isha-bønnen!", 2, Instanstider.Isha);
            CrossLocalNotifications.Current.Show("Hayya' 'alaa salaah!", "Tid til Fajr-bønnen!", 3, Instanstider.Fajr);
            CrossLocalNotifications.Current.Show("Solopgang", "Det er nu solopgang", 4, Instanstider.Shuruk);
            CrossLocalNotifications.Current.Show("Hayya' 'alaa salaah!", "Tid til Dhuhr-bønnen!", 5, Instanstider.Dhuhr);
            CrossLocalNotifications.Current.Show("Hayya' 'alaa salaah!", "Tid til Asr-bønnen!", 6, Instanstider.Asr);
            */
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName))


    ;
        } } }

