using Contract.Request.CarModels;
using Contract.Response.CarModels;
using DatabaseModel;
using DomainService.Operations;
using Host.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("rent-a-car/carModels")]
    public class CarModelsController : ControllerBase
    {
        private readonly CarModelOperations carModelOperations;
        public CarModelsController(CarModelOperations carModelOperations)
        {
            this.carModelOperations = carModelOperations;
        }

        [HttpGet]
        [Authorizable]
        public ActionResult<SearchCarModelsResponse> Search([FromQuery] SearchCarModelsRequest request)
        {
            var carModels = carModelOperations.Search(request.name, request.dailyRentalPrice);
            var response = new SearchCarModelsResponse();
            foreach (var carModel in carModels)
            {
                response.carModels.Add(new SearchCarModelsResponse.CarModel()
                {
                    name = carModel.Name,
                    dailyRentalPrice = carModel.DailyRentalPrice,
                });
            }

            return new JsonResult(response);
        }

        [HttpGet("{id}")]
        [Authorizable]
        public ActionResult<GetSingleCarModelResponse> GetSingle(int id)
        {
            var carModel = carModelOperations.GetSingle(id);

            var response = new GetSingleCarModelResponse();
            response.name = carModel.Name;
            response.dailyRentalPrice = carModel.DailyRentalPrice;

            return response;
        }

        [HttpPost]
        [Authorizable]
        public void Create([FromBody] CreateCarModelRequest request)
        {
            carModelOperations.Create(request.name, request.dailyRentalPrice);
        }

        [HttpPut("{id}")]
        [Authorizable]
        public void Update([FromBody] UpdateCarModelRequest request, int id)
        {
            carModelOperations.Update(id, request.name, request.dailyRentalPrice);
        }

        [HttpDelete("{id}")]
        [Authorizable]
        public void Delete(int id)
        {
            carModelOperations.Delete(id);
        }
    }
}
