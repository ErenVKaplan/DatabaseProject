namespace DatabaseProject.Entities
{
    public class Bank
    {
        public int BankId { get; set; }
        public int UserId { get; set; }
        public string BankName { get; set; }  
        public string CardNo { get; set; }

        public User User { get; set; }
    }
}
