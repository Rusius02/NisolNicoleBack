namespace NisolNicole.Utils.Dtos
{
    public class OutputDtoUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string sexe { get; set; }
        public string pseudo { get; set; }
        public string mail { get; set; }
        public string BirthDate { get; set; }
        public string role { get; set; }
        public string AddressStreet { get; set; }

        public string AddressNumber { get; set; }

        public string AddressCity { get; set; }
        public string AddressZip { get; set; }
        public string AddressCountry { get; set; }
    }
}