namespace IRunes.App.Controllers
{
    using Models;
    using Services;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes.Http;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Mapping;
    using SIS.MvcFramework.Result;
    using ViewModels;

    public class TracksController : Controller
    {
        private readonly ITrackService trackService;
        private readonly IAlbumService albumService;

        public TracksController(ITrackService trackService, IAlbumService albumService)
        {
            this.trackService = trackService;
            this.albumService = albumService;
        }

        [Authorize]
        public ActionResult Create(string albumId)
        {

            var model = new TrackCreateViewModel
            {
                AlbumId = albumId
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost(ActionName = "Create")]
        public ActionResult CreateConfirm(string albumId, string name, string link, decimal price)
        {
            Track trackForDb = new Track
            {
                Name = name,
                Link = link,
                Price = price
            };

            bool isTrackAddedToAlbum = this.albumService.AddTrackToAlbum(albumId, trackForDb);
            if (!isTrackAddedToAlbum)
            {
                return this.Redirect("/Albums/All");
            }

            return this.Redirect($"/Albums/Details?id={albumId}");
        }

        [Authorize]
        public ActionResult Details(string albumId, string trackId)
        {
            var trackFromDb = this.trackService.GetById(trackId);

            if (trackFromDb == null)
            {
                return this.Redirect($"/Albums/Details?id={albumId}");
            }

            var model = ModelMapper.ProjectTo<TrackDetailsViewModel>(trackFromDb);

            return this.View(model);
        }
    }
}
