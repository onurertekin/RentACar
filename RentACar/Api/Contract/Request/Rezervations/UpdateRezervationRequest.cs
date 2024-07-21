namespace Contract.Request.Rezervations
{
    public class UpdateRezervationRequest
    {
        public int carId { get; set; }
        public int userId { get; set; }
        public DateTime pickUpDate { get; set; }
        public DateTime deliveryDate { get; set; }
    }
}
