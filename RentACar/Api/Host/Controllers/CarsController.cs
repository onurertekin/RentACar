using Contract.Request.Cars;
using Contract.Response.Cars;
using DomainService.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("rent-a-car/cars")]
    public class CarsController : ControllerBase
    {
        private readonly CarOperations carOperations;
        public CarsController(CarOperations carOperations)
        {
            this.carOperations = carOperations;
        }

        [HttpGet]
        public ActionResult<SearchCarsResponse> Search([FromQuery] SearchCarsRequest request)
        {
            var cars = carOperations.Search(request.brand, request.model, request.year, request.rentalPrice, request.fuelType, request.transmissionType);
            var response = new SearchCarsResponse();
            foreach (var car in cars)
            {
                response.cars.Add(new SearchCarsResponse.Car()
                {
                    brand = car.Brand,
                    model = car.Model,
                    year = car.Year,
                    rentalPrice = car.RentalPrice,
                    fuelType = car.FuelType,
                    transmissionType = car.TransmissionType,
                });
            }
            return new JsonResult(response);
        }

        [HttpGet("{id}")]
        public ActionResult<GetSingleCarResponse> GetSingle(int id)
        {
            var car = carOperations.GetSingle(id);

            var response = new GetSingleCarResponse();

            response.id = id;
            response.brand = car.Brand;
            response.model = car.Model;
            response.year = car.Year;
            response.rentalPrice = car.RentalPrice;
            response.fuelType = car.FuelType;
            response.transmissionType = car.TransmissionType;

            return response;
        }

        [HttpPost]
        public void Create([FromBody] CreateCarRequest request)
        {
            carOperations.Create(request.brand, request.model, request.year, request.rentalPrice, request.fuelType, request.transmissionType);
        }

        [HttpPut("{id}")]
        public void Update(int id, [FromBody] UpdateCarRequest request)
        {
            carOperations.Update(id, request.brand, request.model, request.year, request.rentalPrice, request.fuelType, request.transmissionType);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            carOperations.Delete(id);
        }
    }
}
