using DatabaseModel;
using DatabaseModel.Entities;
using DomainService.Base;
using DomainService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Operations
{
    public class CarModelOperations : DbContextHelper
    {
        private readonly MainDbContext mainDbContext;
        public CarModelOperations(MainDbContext mainDbContext) : base(mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public IList<CarModel> Search(string? name, int? dailyRentalPrice)
        {
            var query = mainDbContext.CarModels.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = mainDbContext.CarModels.Where(x => x.Name == name);

            if (dailyRentalPrice.HasValue)
                query = mainDbContext.CarModels.Where(x => x.DailyRentalPrice == dailyRentalPrice);

            return query.ToList();
        }

        public CarModel GetSingle(int id)
        {
            var carModel = mainDbContext.CarModels.Where(x => x.Id == id).SingleOrDefault();

            if (carModel == null)
                throw new BusinessException(404, "Model bulunamadı.");
            return carModel;
        }
        public void Create(string name, int dailyRentalPrice)
        {
            var carModel = mainDbContext.CarModels.Where(x => x.Name == name).SingleOrDefault();

            if (carModel != null)
                throw new BusinessException(404, "Bu model zaten mevcut");

            CarModel _carModel = new CarModel();
            _carModel.DailyRentalPrice = dailyRentalPrice;
            _carModel.Name = name;

            SaveEntity(_carModel);
        }

        public void Update(int id, string name, int dailyRentalPrice)
        {
            var carModel = mainDbContext.CarModels.Where(x => x.Id == id).SingleOrDefault();

            if (carModel == null)
                throw new BusinessException(404, "Model bulunamadı.");

            carModel.DailyRentalPrice = dailyRentalPrice;
            carModel.Name = name;

            UpdateEntity(carModel);
        }

        public void Delete(int id)
        {
            var carModel = mainDbContext.CarModels.Where(x => x.Id == id).SingleOrDefault();

            if (carModel == null)
                throw new BusinessException(404, "Model bulunamadı.");


            DeleteEntity(carModel);
        }
    }
}
