using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Flowers.Helpers;
using Flowers.ViewModel;
using GalaSoft.MvvmLight.Helpers;

namespace Flowers
{
    [Activity(Label = "Flowers", MainLauncher = true, Icon = "@drawable/icon")]
    public partial class MainActivity
    {
        // This "fools" the linker into believing that the events are used.
        // In fact we don't even subscribe to them.
        // See https://developer.xamarin.com/guides/android/advanced_topics/linking/
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static bool _falseFlag = false;
        private ObservableRecyclerAdapter<FlowerViewModel, CachingViewHolder> _adapter;

        // Saving the binding to avoid garbage collection
        private readonly List<Binding> _bindings = new List<Binding>();

        public MainViewModel Vm
        {
            get { return App.Locator.Main; }
        }

        public void OnItemClick(int oldPosition, View oldView, int position, View view)
        {
            Vm.ShowDetailsCommand.Execute(Vm.Flowers[position]);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            RefreshButton.SetCommand(Vm.RefreshCommand);

            // Saving the binding to avoid garbage collection
            _bindings.Add(this.SetBinding(
                () => Vm.LastLoadedFormatted,
                () => LastLoadedText.Text));

            _adapter = Vm.Flowers.GetRecyclerAdapter(
                BindViewHolder,
                Resource.Layout.FlowerTemplate,
                OnItemClick);

            FlowersList.SetLayoutManager(new LinearLayoutManager(this));
            FlowersList.SetAdapter(_adapter);

            // Subscribing to events to avoid linker issues in release mode ---------------------------------

            // This "fools" the linker into believing that the events are used.
            // In fact we don't even subscribe to them.
            // See https://developer.xamarin.com/guides/android/advanced_topics/linking/

            if (_falseFlag)
            {
                RefreshButton.Click += (s, e) => { };
            }
        }

        private void BindViewHolder(CachingViewHolder holder, FlowerViewModel flower, int position)
        {
            var image = holder.FindCachedViewById<ImageView>(Resource.Id.FlowerImageView);
            ImageDownloader.AssignImageAsync(image, flower.Model.Image, this);

            var title = holder.FindCachedViewById<TextView>(Resource.Id.NameTextView);
            title.Text = flower.Model.Name;

            var desc = holder.FindCachedViewById<TextView>(Resource.Id.DescriptionTextView);
            desc.Text = flower.Model.Description;
        }
    }
}