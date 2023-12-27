using Microsoft.EntityFrameworkCore;
using sacremap.Data;
using sacremap_web_api.Models;

namespace sacremap.Services.VisitationService
{
    public class VisitationService : IVisitationService
    {
        private readonly DataContext _context;

        public VisitationService(DataContext context)
        {
            _context = context;
        }
        public async Task<Visitation> AddVisitation(String userID, int churchID)
        {

            var user = _context.Users.FirstOrDefault(u => u.Id == userID);
            var church = _context.Churches.FirstOrDefault(c => c.Id == churchID);

            if (user is not null && church is not null)
            {
                var visit = new Visitation
                {
                    UserID = userID,
                    ChurchID = churchID
                };
                _context.Visitations.Add(visit);
                await _context.SaveChangesAsync();
                return visit;
            }
            else return null;

        }

        public async Task<Visitation?> GetVisitation(int id)
        {
            var dbResponce = await _context.Visitations.FirstOrDefaultAsync(x => x.Id == id);
            if (dbResponce != null)
            {
                return dbResponce;
            }
            else
                return null;
        }

        public async Task<List<Church>> GetVisitationsListForUser(String userID)
        {

            var visitationCollection = await _context.Users.Where(u => u.Id == userID).SelectMany(u => u.Visitations).ToListAsync();
            var churchesList = new List<Church>();
            foreach (var visitation in visitationCollection)
            {
                churchesList.Add(await _context.Churches.Where(c => c.Id == visitation.ChurchID).FirstOrDefaultAsync());
            }
            return churchesList;
        }


    }
}
