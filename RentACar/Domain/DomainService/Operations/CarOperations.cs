using DatabaseModel;
using DatabaseModel.Entities;
using DomainService.Exceptions;
using DomainService.Base;

namespace DomainService.Operations
{
    public class CarOperations : DbContextHelper
    {
        private readonly MainDbContext mainDbContext;
        public CarOperations(MainDbContext mainDbContext) : base(mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public IList<Car> Search(string? brand, string? model, DateTime? year, string? rentalPrice, string? fuelType, string? transmissionType)
        {
            var query = mainDbContext.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(brand))
                query = mainDbContext.Cars.Where(x => x.Brand == brand);

            if (!string.IsNullOrEmpty(model))
                query = mainDbContext.Cars.Where(x => x.Model == model);

            if (year.HasValue)
                query = mainDbContext.Cars.Where(x => x.Year == year);

            if (!string.IsNullOrEmpty(rentalPrice))
                query = mainDbContext.Cars.Where(x => x.RentalPrice == rentalPrice);

            if (!string.IsNullOrEmpty(fuelType))
                query = mainDbContext.Cars.Where(x => x.FuelType == fuelType);

            if (!string.IsNullOrEmpty(transmissionType))
                query = mainDbContext.Cars.Where(x => x.TransmissionType == transmissionType);

            return query.ToList();
        }

        public Car GetSingle(int id)
        {
            #region Validations

            var car = mainDbContext.Cars.Where(x => x.Id == id).SingleOrDefault();
            if (car == null)
                throw new BusinessException(404, "Araç bulunamadı");

            #endregion

            return car;
        }

        public void Create(string brand, string model, DateTime year, string rentalPrice, string fuelType, string transmissionType)
        {
            Car car = new Car();
            car.Brand = brand;
            car.Model = model;
            car.Year = year;
            car.RentalPrice = rentalPrice;
            car.FuelType = fuelType;
            car.TransmissionType = transmissionType;

            SaveEntity(car);
        }

        public void Update(int id, string brand, string model, DateTime year, string rentalPrice, string fuelType, string transmissionType)
        {
            #region Validations

            var car = mainDbContext.Cars.Where(x => x.Id == id).SingleOrDefault();
            if (car == null)
                throw new BusinessException(404, "Güncellenecek araç bulunamadı.");

            #endregion

            car.Brand = brand;
            car.Model = model;
            car.Year = year;
            car.RentalPrice = rentalPrice;
            car.FuelType = fuelType;
            car.TransmissionType = transmissionType;

            UpdateEntity(car);
        }
        public void Delete(int id)
        {
            #region Validations

            var car = mainDbContext.Cars.Where(x => x.Id == id).SingleOrDefault();
            if (car == null)
                throw new BusinessException(404, "Silinecek araç bulunamadı.");

            #endregion

            DeleteEntity(car);
        }
    }
}
