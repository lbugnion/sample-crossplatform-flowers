using System;
using Flowers.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using UIKit;

namespace Flowers.iOS
{
    public partial class AddCommentViewController : UIViewController
    {
        private Binding _commentBinding;

        public AddCommentViewController(IntPtr handle)
            : base(handle)
        {
        }

        private NavigationService Nav => ServiceLocator.Current.GetInstance<INavigationService>() as NavigationService;

        public UIBarButtonItem SaveCommentButton { get; private set; }

        public FlowerViewModel Vm { get; private set; }

        public override void ViewDidLoad()
        {
            Vm = (FlowerViewModel) Nav.GetAndRemoveParameter(this);

            SaveCommentButton = new UIBarButtonItem(UIBarButtonSystemItem.Save, null);
            NavigationItem.SetRightBarButtonItem(SaveCommentButton, false);

            _commentBinding = this.SetBinding(() => CommentText.Text);

            SaveCommentButton.SetCommand(
                Vm.SaveCommentCommand,
                _commentBinding);

            base.ViewDidLoad();
        }
    }
}