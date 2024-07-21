namespace Contract.Request.Cars
{
    public class UpdateCarRequest
    {
        public string brand { get; set; }
        public string model { get; set; }
        public string year { get; set; }
        public string fuelType { get; set; }
        public string transmissionType { get; set; }
        public int carModelId { get; set; }

    }
}
