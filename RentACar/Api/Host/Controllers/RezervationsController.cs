using Contract.Request.Rezervations;
using Contract.Response.Rezervations;
using DomainService.Operations;
using Host.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("rent-a-car/rezervations")]
    public class RezervationsController : ControllerBase
    {
        private readonly RezervationOperations rezervationOperations;
        public RezervationsController(RezervationOperations rezervationOperations)
        {
            this.rezervationOperations = rezervationOperations;
        }

        [HttpGet]
        [Authorizable]
        public ActionResult<SearchRezervationsResponse> Search([FromQuery] SearchRezervationsRequest request)
        {
            var rezervations = rezervationOperations.Search(request.carId, request.userId, request.pickUpDate, request.deliveryDate, request.totalPrice);
            var response = new SearchRezervationsResponse();
            foreach (var rezervation in rezervations)
            {
                response.rezervations.Add(new SearchRezervationsResponse.Rezervation()
                {
                    carId = rezervation.CarId,
                    userId = rezervation.UserId,
                    pickUpDate = rezervation.PickUpDate,
                    deliveryDate = rezervation.DeliveryDate,
                    totalPrice = rezervation.TotalPrice,
                });
            }

            return new JsonResult(response);
        }

        [HttpGet("{id}")]
        [Authorizable]
        public ActionResult<GetSingleRezervationResponse> GetSingle(int id)
        {
            var rezervation = rezervationOperations.GetSingle(id);

            var response = new GetSingleRezervationResponse();

            response.id = id;
            response.carId = rezervation.CarId;
            response.userId = rezervation.UserId;
            response.pickUpDate = rezervation.PickUpDate;
            response.deliveryDate = rezervation.DeliveryDate;
            response.totalPrice = rezervation.TotalPrice;

            return response;
        }

        [HttpPost]
        [Authorizable]
        public void Create([FromBody] CreateRezervationRequest request)
        {
            rezervationOperations.Create(request.carId, request.userId, request.pickUpDate, request.deliveryDate);
        }

        [Authorizable]
        [HttpPut("{id}")]
        public void Update([FromBody] UpdateRezervationRequest request, int id)
        {
            rezervationOperations.Update(id, request.carId, request.userId, request.pickUpDate, request.deliveryDate);
        }

        [HttpDelete("{id}")]
        [Authorizable]
        public void Delete(int id)
        {
            rezervationOperations.Delete(id);
        }
    }
}
