namespace Contract.Response.Rezervations
{
    public class SearchRezervationsResponse
    {
        public class Rezervation
        {
            public int carId { get; set; }
            public int userId { get; set; }
            public DateTime pickUpDate { get; set; }
            public DateTime deliveryDate { get; set; }
            public int totalPrice { get; set; }
        }
        public SearchRezervationsResponse()
        {
            rezervations = new List<Rezervation>();
        }
        public List<Rezervation> rezervations { get; set; }
    }
}
