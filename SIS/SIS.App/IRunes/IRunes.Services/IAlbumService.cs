namespace IRunes.Services
{
    using System.Collections.Generic;
    using Models;

    public interface IAlbumService
    {
        Album Create(Album album);
        Album GetById(string id);
        ICollection<Album> GetAll();
        bool AddTrackToAlbum(string albumId, Track track);
    }
}