using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace Flowers.Model
{
    public class Flower : ObservableObject
    {
        private string _description;
        private bool _hasChanges;
        private string _name;

        public Flower()
        {
            Comments = new ObservableCollection<Comment>();
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set { Set(ref _hasChanges, value); }
        }

        [JsonProperty("comments")]
        public ObservableCollection<Comment> Comments { get; set; }

        [JsonProperty("description")]
        public string Description
        {
            get { return _description; }
            set
            {
                if (Set(ref _description, value))
                {
                    HasChanges = true;
                };
            }
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (Set(ref _name, value))
                {
                    HasChanges = true;
                };
            }
        }

        public void AddComment(string comment)
        {
            Comments.Add(
                new Comment
                {
                    Id = Guid.NewGuid().ToString(),
                    InputDate = DateTime.Now,
                    Text = comment
                });
        }

        public void DeleteComment(string id)
        {
            var comment = Comments.FirstOrDefault(c => c.Id == id);
            if (comment != null)
            {
                Comments.Remove(comment);
            }
        }
    }
}