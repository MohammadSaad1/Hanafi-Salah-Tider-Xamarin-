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

namespace HanafiSalahTider
{
    public class ReadFromTxtFile : INotifyPropertyChanged
    {
        string line = "";
        string fileContent;
        DateTime dateTime = DateTime.Now;
        string[] prayertimes;


        public static Tider Instanstider { get; set; }

        public ReadFromTxtFile()
        {
            Instanstider = new Tider();
            getText();

        }
        public void getText()
        {
            string date = dateTime.Year + "   " + dateTime.Month + "   " + dateTime.Day;

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ReadFromTxtFile)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("HanafiSalahTider.salahtider2019.txt");

            using (StreamReader myreader = new StreamReader(stream))
            {
                fileContent = myreader.ReadToEnd();
            }



            if (fileContent.Contains(date))
            {
                string[] apts = fileContent.Split('\n').Where(x => x.Contains(date)).ToArray();

                prayertimes = apts[0].Split(new[] { "   " }, StringSplitOptions.None);

                Instanstider.Fajr = prayertimes[3];
                Instanstider.Shuruk = prayertimes[4];
                Instanstider.Dhuhr = prayertimes[5];
                Instanstider.Asr = prayertimes[7];
                Instanstider.Maghrib = prayertimes[8];
                Instanstider.Isha = prayertimes[9];

            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName))


    ;
        } } }

