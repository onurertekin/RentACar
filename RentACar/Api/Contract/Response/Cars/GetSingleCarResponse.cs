namespace Contract.Response.Cars
{
    public class GetSingleCarResponse
    {
        public int id { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string year { get; set; }
        public string fuelType { get; set; }
        public string transmissionType { get; set; }
    }
}
