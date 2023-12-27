using Microsoft.AspNetCore.Identity;
using sacremap_web_api.Models;

namespace sacremap.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Visitation> Visitations { get; set; }

    }
}
