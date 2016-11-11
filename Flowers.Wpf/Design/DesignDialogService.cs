using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;

namespace Flowers.Design
{
    public class DesignDialogService : IDialogService
    {
        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            // Do nothing in design mode
        }

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            // Do nothing in design mode
        }

        public async Task ShowMessage(string message, string title)
        {
            // Do nothing in design mode
        }

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            // Do nothing in design mode
        }

        public async Task<bool> ShowMessage(
            string message,
            string title,
            string buttonConfirmText,
            string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            // Do nothing in design mode
            return true;
        }

        public async Task ShowMessageBox(string message, string title)
        {
            // Do nothing in design mode
        }
    }
}