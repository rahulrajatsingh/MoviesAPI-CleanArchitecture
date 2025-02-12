using System.ComponentModel.DataAnnotations;

namespace Movies.Authorization
{
    public class Tenant
    {
        [Key]
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
