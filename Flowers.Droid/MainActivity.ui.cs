using Android.Support.V7.Widget;
using Android.Widget;
using Flowers.Helpers;

namespace Flowers
{
    public partial class MainActivity : ActivityBaseEx
    {
        private RecyclerView _flowersList;
        private TextView _lastLoadedText;
        private Button _refreshButton;

        public RecyclerView FlowersList
        {
            get
            {
                return _flowersList
                       ?? (_flowersList = FindViewById<RecyclerView>(Resource.Id.FlowersList));
            }
        }



        public TextView LastLoadedText
        {
            get
            {
                return _lastLoadedText
                       ?? (_lastLoadedText = FindViewById<TextView>(Resource.Id.LastLoadedText));
            }
        }

        public Button RefreshButton
        {
            get
            {
                return _refreshButton
                       ?? (_refreshButton = FindViewById<Button>(Resource.Id.RefreshButton));
            }
        }
    }
}