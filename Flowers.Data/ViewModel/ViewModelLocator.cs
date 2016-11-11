using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Flowers.Design;
using Flowers.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace Flowers.ViewModel
{
    public class ViewModelLocator
    {
        private const bool ForceDesignData = true;
        public const string AddCommentPageKey = "AddCommentPage";
        public const string DetailsPageKey = "DetailsPage";

        static ViewModelLocator()
        {
            Debug.WriteLine("ViewModelLocator");
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (UseDesignData)
            {
                if (!SimpleIoc.Default.IsRegistered<INavigationService>())
                {
                    SimpleIoc.Default.Register<INavigationService, DesignNavigationService>();
                }

                SimpleIoc.Default.Register<IFlowersService, DesignFlowersService>();
            }
            else
            {
                SimpleIoc.Default.Register<IFlowersService, FlowersService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                Debug.WriteLine("Main");
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        private static bool UseDesignData
        {
            get
            {
                return ViewModelBase.IsInDesignModeStatic
                       || ForceDesignData;
            }
        }
    }
}