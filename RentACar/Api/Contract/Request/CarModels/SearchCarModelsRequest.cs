using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Request.CarModels
{
    public class SearchCarModelsRequest
    {
        public string? name { get; set; }
        public int? dailyRentalPrice { get; set; }
    }
}
