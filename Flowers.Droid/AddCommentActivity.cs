using Android.App;
using Android.OS;
using Flowers.ViewModel;
using GalaSoft.MvvmLight.Helpers;

namespace Flowers
{
    [Activity(Label = "Add Comment")]
    public partial class AddCommentActivity
    {
        // This "fools" the linker into believing that the events are used.
        // In fact we don't even subscribe to them.
        // See https://developer.xamarin.com/guides/android/advanced_topics/linking/
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static bool _falseFlag = false;

        private Binding<string, string> _saveBinding;

        private FlowerViewModel Vm { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddComment);

            // Retrieve navigation parameter and set as current "DataContext"
            Vm = GlobalNavigation.GetAndRemoveParameter<FlowerViewModel>(Intent);

            _saveBinding = this.SetBinding(() => CommentText.Text);

            SaveCommentButton.SetCommand(
                Vm.SaveCommentCommand,
                _saveBinding);

            // Subscribing to events to avoid linker issues in release mode ---------------------------------

            // This "fools" the linker into believing that the events are used.
            // In fact we don't even subscribe to them.
            // See https://developer.xamarin.com/guides/android/advanced_topics/linking/

            if (_falseFlag)
            {
                SaveCommentButton.Click += (s, e) => { };
                CommentText.TextChanged += (s, e) => { };
            }
        }
    }
}