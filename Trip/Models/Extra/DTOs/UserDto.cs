namespace Trip.Models.Extra.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Avatar => $"{Name.ToUpper()[0]}{Surname.ToUpper()[0]}";
    }
}
