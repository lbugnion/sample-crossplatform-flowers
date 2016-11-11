using System;
using System.Collections.Generic;
using Flowers.ViewModel;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using SDWebImage;
using UIKit;

namespace Flowers.iOS
{
    public partial class MainViewController : UIViewController
    {
        private const string ReuseId = "ReuseId";

        private readonly List<Binding> _bindings = new List<Binding>();
        private ObservableTableViewSource<FlowerViewModel> _source;

        public MainViewController(IntPtr handle) : base(handle)
        {
        }

        private MainViewModel Vm
        {
            get { return Application.Locator.Main; }
        }

        public override void ViewDidLoad()
        {
            try
            {
                #region Hidden

                // See https://developer.xamarin.com/guides/android/advanced_topics/linking/#falseflag

                var falseFlag = false;

                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (falseFlag)
                    // ReSharper disable HeuristicUnreachableCode
                {
                    RefreshButton.Clicked += (s, e) => { };
                }
                // ReSharper restore HeuristicUnreachableCode

                #endregion

                _bindings.Add(this.SetBinding(
                    () => Vm.LastLoadedFormatted,
                    () => LastLoadedText.Text));

                RefreshButton.SetCommand(Vm.RefreshCommand);

                _source = Vm.Flowers.GetTableViewSource(
                    BindFlowerCell,
                    ReuseId);

                FlowersTableView.RegisterClassForCellReuse(typeof(UITableViewCell), new NSString(ReuseId));
                FlowersTableView.Source = _source;

                _source.SelectionChanged +=
                    (s, e) => Vm.ShowDetailsCommand.Execute(_source.SelectedItem);

                base.ViewDidLoad();
            }
            catch (Exception ex)
            {
            }
        }

        private void BindFlowerCell(UITableViewCell cell, FlowerViewModel flower, NSIndexPath path)
        {
            cell.TextLabel.Text = flower.Model.Name;
            cell.ImageView.SetImage(
                new NSUrl(flower.ImageUri.AbsoluteUri),
                UIImage.FromBundle("flower_256_magenta.png"));
        }
    }
}