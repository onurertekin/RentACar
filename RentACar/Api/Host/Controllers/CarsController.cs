using Contract.Request.Cars;
using Contract.Response.Cars;
using DomainService.Operations;
using Host.Filter;
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
        [Authorizable]
        public ActionResult<SearchCarsResponse> Search([FromQuery] SearchCarsRequest request)
        {
            var cars = carOperations.Search(request.brand, request.model, request.year, request.fuelType, request.transmissionType);
            var response = new SearchCarsResponse();
            foreach (var car in cars)
            {
                response.cars.Add(new SearchCarsResponse.Car()
                {
                    brand = car.Brand,
                    model = car.Model,
                    year = car.Year,
                    fuelType = car.FuelType,
                    transmissionType = car.TransmissionType,
                });
            }
            return new JsonResult(response);
        }

        [HttpGet("{id}")]
        [Authorizable]
        public ActionResult<GetSingleCarResponse> GetSingle(int id)
        {
            var car = carOperations.GetSingle(id);

            var response = new GetSingleCarResponse();

            response.id = id;
            response.brand = car.Brand;
            response.model = car.Model;
            response.year = car.Year;
            response.fuelType = car.FuelType;
            response.transmissionType = car.TransmissionType;

            return response;
        }

        [HttpPost]
        [Authorizable]
        public void Create([FromBody] CreateCarRequest request)
        {
            carOperations.Create(request.brand, request.model, request.year, request.fuelType, request.transmissionType, request.carModelId);
        }

        [HttpPut("{id}")]
        [Authorizable]
        public void Update(int id, [FromBody] UpdateCarRequest request)
        {
            carOperations.Update(id, request.brand, request.model, request.year, request.fuelType, request.transmissionType, request.carModelId);

        }

        [HttpDelete("{id}")]
        [Authorizable]
        public void Delete(int id)
        {
            carOperations.Delete(id);
        }
    }
}
