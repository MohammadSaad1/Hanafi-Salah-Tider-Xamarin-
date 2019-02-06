using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HanafiSalahTider
{
    public partial class App : Application
    {
        public App()
        {
          
            InitializeComponent();
       

            MainPage = new MainPage();

      

        }

        protected override void OnStart()
        {
            ReadFromTxtFile readFromTxtFile = new ReadFromTxtFile();
            readFromTxtFile.getText();

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            ReadFromTxtFile readFromTxtFile = new ReadFromTxtFile();
            readFromTxtFile.getText();
        }
    }
}
