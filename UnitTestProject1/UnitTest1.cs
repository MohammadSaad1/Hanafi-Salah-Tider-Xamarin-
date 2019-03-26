using System;
using HanafiSalahTider;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public object HanafiSalahTider { get; private set; }
/*
        [TestMethod]
        public void TestMethod1()
        {
            //ARRANGE

            ReadFromTxtFile readFromTxtFile = new ReadFromTxtFile();
            DateTime datetime = DateTime.Now.AddMonths(6);


            //ACT

            readFromTxtFile.getText(datetime);
            DateTime Fajr = ReadFromTxtFile.Instanstider.Fajr;
            //ASSERT

            Assert.AreEqual(Fajr, datetime.AddHours(-1));
        }
        */


        [TestMethod]
        public void TestMethod2()
        {
            //ARRANGE

            ReadFromTxtFile readFromTxtFile = new ReadFromTxtFile();
            DateTime datetime = DateTime.Now.AddMonths(2).AddDays(10);


            //ACT

         //   readFromTxtFile.getText(datetime);
            DateTime Fajr = ReadFromTxtFile.Instanstider.Fajr;
            //ASSERT

            Assert.AreEqual(Fajr.TimeOfDay, new DateTime(1,1,1,1,33,0).TimeOfDay);
        }

    }
}
