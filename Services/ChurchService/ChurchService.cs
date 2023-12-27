using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sacremap.Data;
using sacremap_web_api.Models;

namespace sacremap.Services.ChurchService
{
    public class ChurchService : IChurchService
    {
        private readonly DataContext _context;

        public ChurchService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Church>> SearchByCities(string cityName)
        {
            if (!string.IsNullOrWhiteSpace(cityName))
            {
                var dbResponce = await _context.Churches.Where(c => c.City.Contains(cityName)).ToListAsync();
                if (!dbResponce.IsNullOrEmpty())
                {
                    return dbResponce;
                }
                else return null;
            }
            else return null;
        }
        public async Task<Church?> GetChurch(int id)
        {
            var dbResponce = await _context.Churches.FirstAsync(x => x.Id == id);
            if (dbResponce != null)
            {
                return dbResponce;
            }
            else return null;
        }
        public async Task<List<Church>> GetChurchList()
        {
            var dbResponce = await _context.Churches.ToListAsync();
            return dbResponce;
        }
        public async Task<Church> AddChurch(Church newChurch)
        {
            newChurch.Id = 0;

            _context.Churches.Add(newChurch);
            await _context.SaveChangesAsync();

            return newChurch;
        }
        public async Task<Church?> EditChurch(int id, Church newChurch)
        {
            var dbResponce = await _context.Churches.FirstOrDefaultAsync(x => x.Id == id);
            if (dbResponce is not null && newChurch is not null)

            {
                dbResponce.Name = newChurch.Name;
                dbResponce.Street = newChurch.Street;
                dbResponce.PostalCode = newChurch.PostalCode;
                dbResponce.City = newChurch.City;
                dbResponce.Coordinats = newChurch.Coordinats;
                _context.Churches.Update(dbResponce);
                await _context.SaveChangesAsync();
                return dbResponce;



            }
            else return null;
        }
        public async Task<Church?> DeleteChurch(int id)
        {
            var dbResponce = await _context.Churches.FirstOrDefaultAsync(x => x.Id == id);
            if (dbResponce is not null)
            {
                _context.Churches.Remove(dbResponce);
                await _context.SaveChangesAsync();
                return dbResponce;
            }
            else return null;
        }

    }
}
