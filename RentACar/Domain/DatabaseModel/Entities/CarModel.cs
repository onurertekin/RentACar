namespace DatabaseModel.Entities
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DailyRentalPrice { get; set; } // Günlük kiralama fiyatı
    }
}
