using sacremap_web_api.Models;

namespace sacremap.Services.VisitationService
{
    public interface IVisitationService
    {
        Task<List<Church>> GetVisitationsListForUser(String userID);
        Task<Visitation?> GetVisitation(int id);
        Task<Visitation> AddVisitation(String userID, int churchID);

    }
}
