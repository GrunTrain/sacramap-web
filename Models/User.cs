namespace sacremap_web_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public string UserName { get; set; } = string.Empty;
        public required string Password { get; set; }
        public DateTime CreaterAt { get; set; }
    }
}
