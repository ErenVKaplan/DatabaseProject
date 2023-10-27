using System.ComponentModel.DataAnnotations;

namespace DatabaseProject.Entities
{
    public class Adrress
    {
        [Key]
        public int AdrressId { get; set; }
        public int UserId { get; set; }
        public string AdrressName { get; set; }
        public string AdrressDescription { get; set; }

        public User User { get; set; }
    }
}
