using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class RegionDto
    { 
        public string Name { get; set; }

        public virtual ICollection<WeatherDto> Weathers { get; set; }
    }
}
