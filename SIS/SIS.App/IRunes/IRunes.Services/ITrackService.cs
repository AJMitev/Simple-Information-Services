namespace IRunes.Services
{
    using Models;

    public interface ITrackService
    {
        Track GetById(string id);
    }
}