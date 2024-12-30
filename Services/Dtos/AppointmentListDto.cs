namespace Services.Dtos
{
    public class AppointmentListDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public int DOCTORID { get; set; }
        public DateTime TIME_ { get; set; }
        public string DESCRIPTIONN { get; set; }
    }
}
