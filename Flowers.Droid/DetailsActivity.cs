using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Flowers.Helpers;
using Flowers.Model;
using Flowers.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.Generic;
using System;

namespace Flowers
{
    [Activity(Label = "Flower Details")]
    public partial class DetailsActivity
    {
        // This "fools" the linker into believing that the events are used.
        // In fact we don't even subscribe to them.
        // See https://developer.xamarin.com/guides/android/advanced_topics/linking/
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static bool _falseFlag = false;

        private TextView _nameText;
        private List<Binding> _bindings = new List<Binding>();

        private FlowerViewModel Vm { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Details);

            // Retrieve navigation parameter and set as current "DataContext"
            Vm = GlobalNavigation.GetAndRemoveParameter<FlowerViewModel>(Intent);

            var headerView = LayoutInflater.Inflate(Resource.Layout.CommentsListHeaderView, null);
            _nameText = headerView.FindViewById<TextView>(Resource.Id.NameText);

            _bindings.Add(
                this.SetBinding(
                    () => Vm.Model.Name,
                    () => _nameText.Text));

            _bindings.Add(
                this.SetBinding(
                    () => Vm.Model.Name,
                    () => EditNameText.Text,
                    BindingMode.TwoWay));

            headerView.FindViewById<TextView>(Resource.Id.DescriptionText).Text 
                = Vm.Model.Description;

            CommentsList.AddHeaderView(headerView);
            CommentsList.Adapter = Vm.Model.Comments.GetAdapter(GetCommentTemplate);

            ImageDownloader.AssignImageAsync(FlowerImageView, Vm.Model.Image, this);

            AddCommentButton.SetCommand(Vm.AddCommentCommand);
            EditNameButton.Click += EditNameButtonClick;
            SaveButton.SetCommand(Vm.SaveFlowerCommand);

            // Subscribing to events to avoid linker issues in release mode ---------------------------------

            // This "fools" the linker into believing that the events are used.
            // In fact we don't even subscribe to them.
            // See https://developer.xamarin.com/guides/android/advanced_topics/linking/

            if (_falseFlag)
            {
                AddCommentButton.Click += (s, e) => { };
                SaveButton.Click += (s, e) => { };
            }
        }

        private void EditNameButtonClick(object sender, EventArgs e)
        {
            if (EditNameText.Visibility == ViewStates.Gone)
            {
                EditNameText.Visibility = ViewStates.Visible;
                _nameText.Visibility = ViewStates.Gone;
            }
            else
            {
                EditNameText.Visibility = ViewStates.Gone;
                _nameText.Visibility = ViewStates.Visible;
            }
        }

        private View GetCommentTemplate(int position, Comment comment, View convertView)
        {
            convertView = LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = comment.Text;
            convertView.FindViewById<TextView>(Android.Resource.Id.Text2).Text = comment.InputDate.ToString();
            return convertView;
        }
    }
}