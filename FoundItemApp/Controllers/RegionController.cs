﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoundItemApp.Interfaces;
using NetTopologySuite.Features;
using FoundItemApp.Dto.Region;
using FoundItemApp.Dto.Region.ResponseType;

namespace FoundItemApp.Controllers
{
    /// <summary>
    /// The controller for Region related actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionServices _service;
        private readonly ILogger<RegionController> _logger;

        public RegionController(IRegionServices service, ILogger<RegionController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Gets the names of all the regions stored.
        /// </summary>
        /// <returns>Returns a list with all the names</returns>
        ///
        [HttpGet("names")]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllRegionNames()
        {

            var names = await _service.GetAllRegionNames();

            if (names == null)
            {
                _logger.LogInformation("404 NotFound: The region name list was not found");
                return NotFound();
            }

            return Ok(names);
        }


        /// <summary>
        /// 
        /// Gets the envelope coordinates of the the specified region
        /// </summary>
        /// <param name="regionId">The id of the region</param>
        /// <returns>Returns an array with the envelope coordinates</returns>
        [HttpGet("{regionName}/envelope")]
        [ProducesResponseType(typeof(double[]), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRegionEnvelope([FromRoute] int regionId)
        {

            var envelope = await _service.GetRegionEnvelope(regionId);

            if (envelope == null)
            {
                _logger.LogInformation("404 NotFound: The {region} envelope not found", regionId);
                return NotFound();
            }

            return Ok(envelope); 

        }

        /// <summary>
        /// Gets the border coordinates of the specified region
        /// </summary>
        /// <param name="regionId">The id of the region</param>
        /// <returns>Returns the coordinates of the region in GeoJSON format</returns>
        [HttpGet("{regionName}")]
        [ProducesResponseType(typeof(GetRegionResponseType), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRegionBorder([FromRoute] int regionId)
        {

            var region = await _service.GetRegionBorders(regionId);

            if(region == null)
            {
                _logger.LogInformation("404 NotFound: The {region} was not found", regionId);
                return NotFound();
            }

            return Ok(region);
        }
    }
}
 