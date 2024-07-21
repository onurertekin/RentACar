using DatabaseModel;
using DatabaseModel.Entities;
using DomainService.Base;
using DomainService.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Operations
{
    public class RezervationOperations : DbContextHelper
    {
        private readonly MainDbContext mainDbContext;
        public RezervationOperations(MainDbContext mainDbContext) : base(mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public IList<Rezervation> Search(int? carId, int? userId, DateTime? pickUpDate, DateTime? deliveryDate, int? totalPrice)
        {
            var query = mainDbContext.Rezervations.AsQueryable();

            if (carId.HasValue)
                query = mainDbContext.Rezervations.Where(x => x.CarId == carId);

            if (userId.HasValue)
                query = mainDbContext.Rezervations.Where(x => x.UserId == userId);

            if (pickUpDate.HasValue)
                query = mainDbContext.Rezervations.Where(x => x.PickUpDate == pickUpDate);

            if (deliveryDate.HasValue)
                query = mainDbContext.Rezervations.Where(x => x.DeliveryDate == deliveryDate);

            if (totalPrice.HasValue)
                query = mainDbContext.Rezervations.Where(x => x.TotalPrice == totalPrice);

            return query.ToList();
        }

        public Rezervation GetSingle(int id)
        {
            #region Validations

            var rezervation = mainDbContext.Rezervations.Where(x => x.Id == id).SingleOrDefault();
            if (rezervation == null)
                throw new BusinessException(404, "Rezervasyon bulunamadı.");

            #endregion

            return rezervation;
        }

        public void Create(int carId, int userId, DateTime pickUpDate, DateTime deliveryDate)
        {
            #region Validations

            var car = mainDbContext.Cars.Include(c=>c.CarModel).Where(x => x.Id == carId).SingleOrDefault();
            if (car == null)
                throw new BusinessException(404, "Araç bulunamadı.");

            var user = mainDbContext.Users.Where(x => x.Id == userId).SingleOrDefault();
            if (user == null)
                throw new BusinessException(404, "Kullanıcı bulunamadı.");

            #endregion

            int rentalDays = (deliveryDate - pickUpDate).Days;
            int totalPrice = rentalDays * car.CarModel.DailyRentalPrice;

            var rezervation = new Rezervation();
            rezervation.CarId = carId;
            rezervation.UserId = userId;
            rezervation.PickUpDate = pickUpDate;
            rezervation.DeliveryDate = deliveryDate;
            rezervation.TotalPrice = totalPrice;

            SaveEntity(rezervation);
        }

        public void Update(int id, int carId, int userId, DateTime pickUpDate, DateTime deliveryDate)
        {
            #region Validations

            var rezervation = mainDbContext.Rezervations.Where(x => x.Id == id).SingleOrDefault();
            if (rezervation == null)
                throw new BusinessException(404, "Rezervasyon bulunamadı.");

            var car = mainDbContext.Cars.Include(c => c.CarModel).Where(x => x.Id == carId).SingleOrDefault();
            if (car == null)
                throw new BusinessException(404, "Araç bulunamadı.");

            var user = mainDbContext.Users.Where(x => x.Id == userId).SingleOrDefault();
            if (user == null)
                throw new BusinessException(404, "Kullanıcı bulunamadı.");

            #endregion

            int rentalDays = (deliveryDate - pickUpDate).Days;
            int totalPrice = rentalDays * car.CarModel.DailyRentalPrice;

            rezervation.CarId = carId;
            rezervation.UserId = userId;
            rezervation.PickUpDate = pickUpDate;
            rezervation.DeliveryDate = deliveryDate;
            rezervation.TotalPrice = totalPrice;

            UpdateEntity(rezervation);
        }
        public void Delete(int id)
        {
            #region Validations

            var rezervation = mainDbContext.Rezervations.Where(x => x.Id == id).SingleOrDefault();
            if (rezervation == null)
                throw new BusinessException(404, "Rezervasyon bulunamadı.");

            #endregion

            DeleteEntity(rezervation);
        }
    }
}
