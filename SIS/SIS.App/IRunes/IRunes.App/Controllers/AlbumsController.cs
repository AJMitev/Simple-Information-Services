namespace IRunes.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Models;
    using Services;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes.Http;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Mapping;
    using SIS.MvcFramework.Result;
    using ViewModels;

    public class AlbumsController : Controller
    {

        private readonly IAlbumService albumService;

        public AlbumsController()
        {
            this.albumService = new AlbumService();
        }

        [Authorize]
        public ActionResult All()
        {
            var allAlbums = this.albumService.GetAll();

            if (allAlbums.Count != 0)
            {
                return this.View(allAlbums.Select(ModelMapper.ProjectTo<AlbumAllViewModel>).ToList());
            }


            return this.View(new List<AlbumAllViewModel>());
        }

        [Authorize]
        public ActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost(ActionName = "Create")]
        public ActionResult CreateConfirm()
        {
            string name = ((ISet<string>)this.Request.FormData["name"]).FirstOrDefault();
            string cover = ((ISet<string>)this.Request.FormData["cover"]).FirstOrDefault();

            Album album = new Album
            {
                Name = name,
                Cover = cover,
                Price = 0M
            };


            this.albumService.Create(album);

            return this.Redirect("/Albums/All");
        }

        [Authorize]
        public ActionResult Details()
        {
            string albumId = this.Request.QueryData["id"].ToString();

            Album albumFromDb = this.albumService.GetById(albumId);
            var albumViewModel = ModelMapper.ProjectTo<AlbumDetailsViewModel>(albumFromDb);


            if (albumFromDb == null)
            {
                return this.Redirect("/Albums/All");
            }

            
            return this.View(albumViewModel );

        }
    }
}
