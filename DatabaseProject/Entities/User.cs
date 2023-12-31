﻿namespace DatabaseProject.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CellPhone { get; set; }
        public string Role { get; set; }
        public string TC { get; set; }
        public string Adrress { get; set; }
        public DateTime BornDate { get; set; }

        public string Gender { get; set; }
        public int TotalUserScore { get; set; }
        public ICollection<Adrress> Adrresses { get; set; }
        public ICollection<Bank> Banks { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
