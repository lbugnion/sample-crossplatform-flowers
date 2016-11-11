using System;
using System.Globalization;
using Flowers.Model;
using Flowers.ViewModel;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using UIKit;

namespace Flowers.iOS
{
    public partial class SeeCommentsViewController : UIViewController
    {
        private ObservableTableViewController<Comment> _tableController;

        public SeeCommentsViewController(IntPtr handle)
            : base(handle)
        {
        }

        private NavigationService Nav => ServiceLocator.Current.GetInstance<INavigationService>() as NavigationService;

        public UIBarButtonItem AddCommentButton { get; private set; }

        public FlowerViewModel Vm { get; set; }

        public override void ViewDidLoad()
        {
            Vm = (FlowerViewModel) Nav.GetAndRemoveParameter(this);

            _tableController = Vm.Model.Comments.GetController(
                CreateCommentCell,
                BindCommentCell);

            _tableController.TableView = CommentsTableView;

            AddCommentButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, null);
            NavigationItem.SetRightBarButtonItem(AddCommentButton, false);
            AddCommentButton.SetCommand(Vm.AddCommentCommand);

            base.ViewDidLoad();
        }

        private void BindCommentCell(UITableViewCell cell, Comment comment, NSIndexPath path)
        {
            cell.TextLabel.Text = comment.Text;
            cell.DetailTextLabel.Text = comment.InputDate.ToString(CultureInfo.CurrentCulture);
        }

        private UITableViewCell CreateCommentCell(NSString reuseId)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Subtitle, null);
            cell.TextLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            return cell;
        }
    }
}