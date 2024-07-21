namespace DatabaseModel.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string FuelType { get; set; }
        public string TransmissionType { get; set; }
        public int CarModelId { get; set; }
        public CarModel CarModel { get; set; }

    }
}
