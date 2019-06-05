namespace IRunes.App.ViewModels
{
    using System.Collections.Generic;
    using Models;

    public class AlbumDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Cover { get; set; }

        public decimal Price { get; set; }

        public List<TrackAlbumAllViewModel> Tracks { get; set; }
    }
}