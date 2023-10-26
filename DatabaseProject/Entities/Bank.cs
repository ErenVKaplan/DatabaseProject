

using System.ComponentModel.DataAnnotations;

namespace DatabaseProject.Entities
{
    public class Bank
    {
        [Key]   
        public int BankId { get; set; }
        public int UserId { get; set; }
        public string BankName { get; set; }  
        public string CardNo { get; set; }

        public User User { get; set; }
    }
}
