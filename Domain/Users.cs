namespace Domain
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Sexe { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }
        public string Mail { get; set; } = String.Empty;    
        
        public string Pseudo { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        
        public string Role { get; set; } = String.Empty;

        public string AddressStreet { get; set; } = String.Empty;

        public string AddressNumber { get; set; } = String.Empty;

        public string AddressCity { get; set; } = String.Empty;
        public string AddressZip { get; set; } = String.Empty;
        public string AddressCountry { get; set; } = String.Empty;

    }
}