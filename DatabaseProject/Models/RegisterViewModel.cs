﻿namespace DatabaseProject.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string TC { get; set; }
        public string CellPhone { get; set; }
        public string Adrress { get; set; }
        public DateTime BornDate { get; set; }

        public string Gender { get; set; }
    }
}
