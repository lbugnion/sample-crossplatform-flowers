using System;
using Flowers.Design;
using Flowers.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace Flowers.ViewModel
{
    public class FlowerViewModel : ViewModelBase
    {
        private readonly IFlowersService _flowerService;

        private RelayCommand _addCommentCommand;
        private bool _isSaving;
        private RelayCommand<string> _saveCommentCommand;
        private RelayCommand _saveFlowerCommand;

        public FlowerViewModel(IFlowersService flowerService, Flower model)
        {
            _flowerService = flowerService;
            Model = model;

            Model.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Flower.HasChanges))
                {
                    SaveFlowerCommand.RaiseCanExecuteChanged();
                }
            };
        }

#if DEBUG
        // This constructor is used in the Windows Phone app at design time,
        // for the Blend visual designer.
        public FlowerViewModel()
        {
            if (IsInDesignMode)
            {
                var service = new DesignFlowersService();
                Model = service.GetFlower(0);
            }
        }
#endif

        public RelayCommand AddCommentCommand
        {
            get
            {
                return _addCommentCommand
                       ?? (_addCommentCommand = new RelayCommand(
                           () =>
                           {
                               var nav = ServiceLocator.Current.GetInstance<INavigationService>();
                               nav.NavigateTo(ViewModelLocator.AddCommentPageKey, this);
                           }));
            }
        }

        public string ImageFileName
        {
            get { return ImageUri.LocalPath; }
        }

        public Uri ImageUri
        {
            get { return new Uri(Model.Image); }
        }

        public Flower Model { get; }

        public bool IsSaving
        {
            get { return _isSaving; }
            set
            {
                if (Set(ref _isSaving, value))
                {
                    SaveCommentCommand.RaiseCanExecuteChanged();
                    SaveFlowerCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RelayCommand SaveFlowerCommand
        {
            get
            {
                return _saveFlowerCommand
                    ?? (_saveFlowerCommand = new RelayCommand(
                    async () =>
                    {
                        IsSaving = true;
                        var result = await _flowerService.Save(Model);

                        if (!result)
                        {
                            // Handle error when saving
                            var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                            await
                                dialog.ShowError(
                                    "Error when saving, the change was not saved",
                                    "Error",
                                    "OK",
                                    null);
                        }

                        IsSaving = false;
                    },
                    () => !IsSaving && Model.HasChanges));
            }
        }

        public RelayCommand<string> SaveCommentCommand
        {
            get
            {
                return _saveCommentCommand
                       ?? (_saveCommentCommand = new RelayCommand<string>(
                           async text =>
                           {
                               IsSaving = true;
                               Model.Comments.Add(
                                   new Comment
                                   {
                                       Id = Guid.NewGuid().ToString(),
                                       InputDate = DateTime.Now,
                                       Text = text
                                   });

                               var result = await _flowerService.Save(Model);

                               if (!result)
                               {
                                   // Handle error when saving
                                   var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                                   await
                                       dialog.ShowError(
                                           "Error when saving, your comment was not saved",
                                           "Error",
                                           "OK",
                                           null);
                               }

                               var nav = ServiceLocator.Current.GetInstance<INavigationService>();
                               nav.GoBack();
                               IsSaving = false;
                           },
                           text => !string.IsNullOrEmpty(text) && !IsSaving));
            }
        }
    }
}