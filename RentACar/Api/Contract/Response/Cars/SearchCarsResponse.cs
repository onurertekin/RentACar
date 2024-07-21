namespace Contract.Response.Cars
{
    public class SearchCarsResponse
    {
        public class Car
        {
            public string brand { get; set; }
            public string model { get; set; }
            public string year { get; set; }
            public string fuelType { get; set; }
            public string transmissionType { get; set; }
        }
        public SearchCarsResponse()
        {
            cars = new List<Car>();
        }

        public List<Car> cars { get; set; }
    }
}
