namespace IRunes.App.Extensions
{
    using System.Linq;
    using System.Net;
    using Models;

    public static class EntityExtensions
    {
        private static string GetTracks(Album album)
        {
            return
                album.Tracks.Count == 0
                    ? "There are currently not tracks in this album."
                    : string.Join("", album.Tracks.Select((track, index) => track.ToHtmlAll(album.Id, index + 1)));
        }

        public static string ToHtmlAll(this Album album)
        {
            return $"<h3><a href=\"/Albums/Details?id={album.Id}\">{WebUtility.UrlDecode(album.Name)}</a></h3>";
        }

        public static string ToHtmlAll(this Track track, string albumId, int index)
        {
            return $"";
        }

        public static string ToHtmlDetails(this Track track, string albumId)
        {
            return "<div class=\"track-details\">" +
                   $"    <h4 class=\"text-center\">Track Name: {WebUtility.UrlDecode(track.Name)}</h4>" +
                   $"    <h4 class=\"text-center\">Track Price: ${track.Price:F2}</h4>" +
                   "    <hr class=\"bg-success w-50\" style=\"height: 2px\" />" +
                   "    <div class=\"d-flex justify-content-center\">" +
                   $"        <iframe src=\"{WebUtility.UrlDecode(track.Link)}\" class=\"w-50\" height=\"480\"></iframe>" +
                   "    </div>" +
                   "    <hr class=\"bg-success w-50\" style=\"height: 2px\" />" +
                   "    <div class=\"d-flex justify-content-center\">" +
                   $"        <a href=\"/Albums/Details?id={albumId}\" class=\"btn bg-success text-white\">Back To Album</a>" +
                   "    </div>" +
                   "</div>";
        }
    }
}
