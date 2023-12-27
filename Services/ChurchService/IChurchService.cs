using sacremap_web_api.Models;

namespace sacremap.Services.ChurchService
{
    public interface IChurchService
    {
        Task<List<Church>> GetChurchList();
        Task<Church> GetChurch(int id);
        Task<List<Church>> SearchByCities(string cityName);
        Task<Church> AddChurch(Church newChurch);
        Task<Church?> DeleteChurch(int id);
        Task<Church?> EditChurch(int id, Church newChurch);
    }
}
