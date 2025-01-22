namespace Domain
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string sexe { get; set; }
        public DateTime BirthDate { get; set; }
        public string mail { get; set; }
        
        public string pseudo { get; set; }
        public string Password { get; set; }
        
        public string Role { get; set; }

        public string AddressStreet { get; set; }

        public string AddressNumber { get; set; }

        public string AddressCity { get; set; }
        public string AddressZip { get; set; }
        public string AddressCounty { get; set; }

    }
}