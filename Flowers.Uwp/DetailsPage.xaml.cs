using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Flowers
{
    public partial class DetailsPage
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = e.Parameter;
        }

        private void ExpandImageButtonChecked(object sender, RoutedEventArgs e)
        {
            ContentPanel.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            ContentPanel.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
        }

        private void ExpandImageButtonUnchecked(object sender, RoutedEventArgs e)
        {
            ContentPanel.RowDefinitions[0].Height = new GridLength(0.3, GridUnitType.Star);
            ContentPanel.RowDefinitions[1].Height = new GridLength(0.7, GridUnitType.Star);
        }
    }
}