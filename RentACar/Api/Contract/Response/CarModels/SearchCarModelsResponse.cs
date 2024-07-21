using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response.CarModels
{
    public class SearchCarModelsResponse
    {
        public class CarModel
        {
            public string name { get; set; }
            public int dailyRentalPrice { get; set; }
        }
        public SearchCarModelsResponse()
        {
            carModels = new List<CarModel>();
        }
        public List<CarModel> carModels { get; set; }

    }
}
