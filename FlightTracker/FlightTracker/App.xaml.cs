using FlightTracker.DataAccess;
using FlightTracker.DataAccess.Queries;
using FlightTracker.DataAccess.Queries.Response;
using FlightTracker.Validation;
using FlightTracker.ViewModels;
using Xamarin.Forms;

namespace FlightTracker
{
    public partial class App : Application
    {
        public App()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-CH");
            InitializeComponent();

            DependencyService.Register<IParaglidingDbContext, ParaglidingDbContext>();
            DependencyService.RegisterSingleton<IMigrationHelper>(new MigrationHelper(DependencyService.Resolve<IParaglidingDbContext>()));
            //DependencyService.RegisterSingleton<IValidationService>(new ValidationService());
            //DependencyService.RegisterSingleton<ICryptoService>(new CryptoService());

            RegisterQueries();
            RegisterViewModels();

            MainPage = new AppShell();
        }

        private void RegisterQueries()
        {
            DependencyService.RegisterSingleton<IDbQuery<(string, string), IdentityUser>>(new IdentityQuery(DependencyService.Resolve<IParaglidingDbContext>()));
        }

        private void RegisterViewModels()
        {           
            var query = DependencyService.Resolve<IDbQuery<(string, string), IdentityUser>>();
            var context = DependencyService.Resolve<IParaglidingDbContext>();
            DependencyService.RegisterSingleton<LoginViewModel>(new LoginViewModel(query, context));
            DependencyService.RegisterSingleton<HomeViewModel>(new HomeViewModel(context));
            DependencyService.RegisterSingleton<UsersViewModel>(new UsersViewModel(context));
            DependencyService.RegisterSingleton<UserViewModel>(new UserViewModel(context));
            DependencyService.RegisterSingleton<FlightViewModel>(new FlightViewModel(context));
        }
        protected override async void OnStart()
        {
            var migrations = DependencyService.Resolve<IMigrationHelper>();
            await migrations.MigrateAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
