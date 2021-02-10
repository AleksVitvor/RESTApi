namespace RESTWithSwagger.Controllers
{
    using AutoMapper;
    using DAL.UoW;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.DataModels;
    using Models.FrontModels;
    using RESTWithSwagger.Filters;
    using System;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WeatherController(ILogger<WeatherController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get weather for region by id of region
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetWeather(int regionId)
        {
            if (regionId < 0) 
            {
                return BadRequest("Wrong regionId");
            }

            try
            {
                var result = _unitOfWork.GetRepository<Region>().GetItem(x => x.Id == regionId);
               
                return Ok(_mapper.Map<WeatherDisplay>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GetWeather(WeatherFilter filter)
        {
            if (filter == null)
            {
                return BadRequest("Filter can't be null");
            }

            try
            {
                var region = _unitOfWork.GetRepository<Region>().GetItem(x => x.Id == filter.RegionId);

                var weather = region.Weathers.Where(x => x.DateAndTime > filter.StartDate && x.DateAndTime < filter.EndDate).ToList();
                var result = new Region()
                {
                    Name = region.Name,
                    Weathers = weather
                };
                return Ok(_mapper.Map<WeatherDisplay>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public IActionResult CreateWeather(WeatherCreation weather)
        {
            if (weather == null)
            {
                return BadRequest("Weather can't be null");
            }

            try
            {
                var newWeather = _mapper.Map<Weather>(weather);
                _unitOfWork.GetRepository<Weather>().Create(newWeather);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
