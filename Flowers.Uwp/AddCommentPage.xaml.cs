using Windows.UI.Xaml.Navigation;

namespace Flowers
{
    public sealed partial class AddCommentPage
    {
        public AddCommentPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = e.Parameter;
        }
    }
}