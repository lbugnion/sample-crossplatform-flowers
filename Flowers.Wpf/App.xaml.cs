using Flowers.Design;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace Flowers
{
    public partial class App
    {
        static App()
        {
            // TEMPO to avoid a crash only
            // Later we will implement a real navigation and dialog service for this app

            if (!SimpleIoc.Default.IsRegistered<INavigationService>()
                && !ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<INavigationService, DesignNavigationService>();
            }

            if (!SimpleIoc.Default.IsRegistered<IDialogService>())
            {
                SimpleIoc.Default.Register<IDialogService, DesignDialogService>();
            }
        }
    }
}