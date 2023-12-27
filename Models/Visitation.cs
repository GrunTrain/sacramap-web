using sacremap.Models;

namespace sacremap_web_api.Models
{
    public class Visitation()
    {

        public int Id { get; set; }
        public Church Church { get; set; }
        public User User { get; set; }
        public string UserID { get; set; }
        public int ChurchID { get; set; }



    }
}
