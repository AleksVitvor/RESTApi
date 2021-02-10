using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.FrontModels
{
    public class WeatherCreation
    {
        public int regionId { get; set; }

        public DateTime DateAndTime { get; set; }

        public int Degrees { get; set; }
    }
}
