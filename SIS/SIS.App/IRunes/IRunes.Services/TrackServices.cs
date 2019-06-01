namespace IRunes.Services
{
    using System.Linq;
    using Data;
    using Models;

    public class TrackServices : ITrackService
    {
        private readonly RunesDbContext context;

        public TrackServices()
        {
            this.context = new RunesDbContext();
        } 

        public Track GetById(string id)
        {
            Track trackFromDb = context.Tracks.SingleOrDefault(track => track.Id == id);

            return trackFromDb;
        }
    }
}