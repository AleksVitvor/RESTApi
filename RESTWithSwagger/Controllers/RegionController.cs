namespace RESTWithSwagger.Controllers
{
    using AutoMapper;
    using DAL.UoW;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.DataModels;
    using Models.FrontModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : Controller
    {
        private readonly ILogger<RegionController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegionController(ILogger<RegionController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateRegion(RegionCreation region)
        {
            if (region == null)
            {
                return BadRequest("Region can't be null");
            }

            try
            {
                var newRegion = _mapper.Map<Region>(region);
                _unitOfWork.GetRepository<Region>().Create(newRegion);
                _unitOfWork.Save();
                var addedRegion = _unitOfWork.GetRepository<Region>().GetItem(x => x.Name == newRegion.Name);
                return Ok(addedRegion.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteRegion(int regionId)
        {
            if (regionId < 0)
            {
                return BadRequest("Region can't be less than zero");
            }

            try
            {
                var weather = _unitOfWork.GetRepository<Region>().GetItem(x => x.Id == regionId).Weathers.Select(x => x.Id).ToList();
                _unitOfWork.GetRepository<Weather>().Delete(x => weather.Contains(x.Id));
                _unitOfWork.GetRepository<Region>().Delete(x => x.Id == regionId);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{regionId}")]
        public IActionResult GetRegion(int regionId)
        {
            if (regionId<=0)
            {
                return BadRequest("Region can't be less or equal to zero");
            }

            try
            {
                var region = _unitOfWork.GetRepository<Region>().GetItem(x => x.Id == regionId);
                return Ok(_mapper.Map<RegionCreation>(region));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetRegions()
        {
            try
            {
                var regions = _unitOfWork.GetRepository<Region>().GetItemsList().ToList();
                return Ok(_mapper.Map<List<RegionCreation>>(regions));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateRegion(RegionUpdate region)
        {
            if (region == null) 
            {
                return BadRequest("Region can't be null");
            }

            try
            {
                var oldRegion = _unitOfWork.GetRepository<Region>().GetItem(x => x.Id == region.Id);
                oldRegion.Name = region.Name;
                _unitOfWork.GetRepository<Region>().Update(oldRegion);
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
