namespace IRunes.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AlbumService : IAlbumService
    {
        private readonly RunesDbContext context;

        public AlbumService()
        {
            this.context = new RunesDbContext();
        }

        public Album Create(Album album)
        {
            Album savedAlbum = context.Albums.Add(album).Entity;
            context.SaveChanges();

            return album;
        }

        public Album GetById(string id)
        {
            Album albumFromDb = context.Albums
                .Include(album => album.Tracks)
                .SingleOrDefault(album => album.Id == id);

            return albumFromDb;
        }

        public ICollection<Album> GetAll()
        {
            ICollection<Album> allAlbums = context.Albums.ToList();

            return allAlbums;
        }

        public bool AddTrackToAlbum(string albumId, Track track)
        {
            Album albumFromDb = this.GetById(albumId);

            if (albumFromDb == null)
            {
                return false;
            }

            albumFromDb.Tracks.Add(track);
            albumFromDb.Price = (albumFromDb.Tracks
                                     .Select(tr => tr.Price)
                                     .Sum() * 87) / 100;

            this.context.Update(albumFromDb);
            this.context.SaveChanges();

            return true;
        }
    }
}