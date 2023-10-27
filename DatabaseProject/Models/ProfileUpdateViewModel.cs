namespace DatabaseProject.Models
{
    public class ProfileUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string TC { get; set; }
        public DateTime BornDate { get; set; }

        public List<string>? Genders { get; set; }
        public string Gender { get; set; }
    }
}
