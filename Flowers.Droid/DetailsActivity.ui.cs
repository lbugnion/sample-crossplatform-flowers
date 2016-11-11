using Android.Widget;
using Flowers.Helpers;

namespace Flowers
{
    public partial class DetailsActivity : ActivityBaseEx
    {
        private Button _addCommentButton;
        private Button _editNameButton;
        private Button _saveButton;
        private EditText _editNameText;
        private ListView _commentsList;
        private ImageView _flowerImageView;

        public Button AddCommentButton
        {
            get
            {
                return _addCommentButton
                       ?? (_addCommentButton = FindViewById<Button>(Resource.Id.AddCommentButton));
            }
        }

        public Button EditNameButton
        {
            get
            {
                return _editNameButton
                       ?? (_editNameButton = FindViewById<Button>(Resource.Id.EditNameButton));
            }
        }

        public Button SaveButton
        {
            get
            {
                return _saveButton
                       ?? (_saveButton = FindViewById<Button>(Resource.Id.SaveButton));
            }
        }

        public EditText EditNameText
        {
            get
            {
                return _editNameText
                       ?? (_editNameText = FindViewById<EditText>(Resource.Id.EditNameText));
            }
        }

        public ListView CommentsList
        {
            get
            {
                return _commentsList
                       ?? (_commentsList = FindViewById<ListView>(Resource.Id.CommentsList));
            }
        }

        public ImageView FlowerImageView
        {
            get
            {
                return _flowerImageView
                       ?? (_flowerImageView = FindViewById<ImageView>(Resource.Id.FlowerImageView));
            }
        }
    }
}