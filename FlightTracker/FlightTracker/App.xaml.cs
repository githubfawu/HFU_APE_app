using System;
using System.IO;
using FlightTracker.Services;
using FlightTracker.Data;
using FlightTracker.Models;
using SQLite;
using Xamarin.Forms;

namespace FlightTracker
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3"));
                }
                return database;
            }
        }

        CreateDemoData demoData = new CreateDemoData();

        public App()
        {
            InitializeComponent();
            
            demoData.CreateUsers();
            //demoData.CreateFlights();
            
            DependencyService.Register<MockDataStore>();

            MainPage = new AppShell();


            //Shell.Current.GoToAsync("//LoginPage");
            //MainPage = new LoginPage();

            //var mainPage = new ItemsPage();
            //var navigationPage = new NavigationPage(mainPage);

            //Services = ContainerExtensions.CreateContainer();
            //Services.RegisterInstance<Page>(navigationPage);

            //MainPage = navigationPage;

            
        }

        protected void DeleteDB()
        {
            FileInfo fi = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB.db3"));
            try
            {
                if (fi.Exists)
                {
                    SQLiteConnection connection = new SQLiteConnection("Data Source=" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB.db3") + ";");
                    connection.Close();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    fi.Delete();
                }
            }
            catch (Exception ex)
            {
                fi.Delete();
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
