namespace IRunes.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Extensions;
    using Models;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes.Http;
    using SIS.MvcFramework.Result;

    public class TracksController : Controller
    {
        public ActionResult Create( )
        {
            if (!this.IsLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = this.Request.QueryData["albumId"].ToString();

            this.ViewData["AlbumId"] = albumId;
            return this.View();
        }

        [HttpPost(ActionName = "Create")]
        public ActionResult CreateConfirm( )
        {
            if (!this.IsLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = this.Request.QueryData["albumId"].ToString();

            using (var context = new RunesDbContext())
            {
                Album albumFromDb = context.Albums.SingleOrDefault(album => album.Id == albumId);

                if (albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                string name = ((ISet<string>)this.Request.FormData["name"]).FirstOrDefault();
                string link = ((ISet<string>)this.Request.FormData["link"]).FirstOrDefault();
                string price = ((ISet<string>)this.Request.FormData["price"]).FirstOrDefault();

                Track trackForDb = new Track
                {
                    Name = name,
                    Link = link,
                    Price = decimal.Parse(price)
                };

                albumFromDb.Tracks.Add(trackForDb);
                albumFromDb.Price = (albumFromDb.Tracks
                                         .Select(track => track.Price)
                                         .Sum() * 87) / 100;
                context.Update(albumFromDb);
                context.SaveChanges();
            }

            return this.Redirect($"/Albums/Details?id={albumId}");
        }

        public ActionResult Details( )
        {
            if (!this.IsLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = this.Request.QueryData["albumId"].ToString();
            string trackId = this.Request.QueryData["trackId"].ToString();

            using (var context = new RunesDbContext())
            {
                Track trackFromDb = context.Tracks.SingleOrDefault(track => track.Id == trackId);

                if (trackFromDb == null)
                {
                    return this.Redirect($"/Albums/Details?id={albumId}");
                }

                this.ViewData["AlbumId"] = albumId;
                this.ViewData["Track"] = trackFromDb.ToHtmlDetails(albumId);
                return this.View();
            }
        }
    }
}
