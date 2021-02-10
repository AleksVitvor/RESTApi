using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.FrontModels
{
    public class WeatherDisplay
    {
        public string RegionName { get; set; }

        public ICollection<WeatherDto> Weathers { get; set; }
    }
}
