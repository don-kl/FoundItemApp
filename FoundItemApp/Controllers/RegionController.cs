﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoundItemApp.Interfaces;
using NetTopologySuite.Features;

namespace FoundItemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionServices _services;
        private readonly ILogger<RegionController> _logger;

        public RegionController(IRegionServices service, ILogger<RegionController> logger)
        {
            _services = service;
            _logger = logger;
        }

        /// <summary>
        /// Get all region Names
        /// </summary>
        /// <returns></returns>
        ///
        [HttpGet("names")]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllRegionNames()
        {
            _logger.LogInformation("api/region/names has been requested");
            try
            {
                var names = await _services.GetAllRegionNames();

                if (names == null)
                {
                    _logger.LogInformation("api/region/names not found");
                    return NotFound();
                }

                return Ok(names);

            } catch (Exception ex)
            {
                _logger.LogInformation("api/region/names has produced an error {ex}", ex);
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Get the envelope coordinates of the region
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Double[]), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("envelope/{name}")]
        public async Task<IActionResult> GetRegionEnvelope([FromRoute] string name)
        {
            _logger.LogInformation("api/region/{name} has been requested", name);
            try
            {
                var envelope = _services.GetRegionEnvelope(name);

                if (envelope == null)
                {
                    _logger.LogInformation("api/region/{name} not found", name);
                    return NotFound();
                }

                return Ok(envelope); 
            } catch (Exception ex)
            {
                _logger.LogInformation("api/region/names has produced an error {ex}", ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get the borders of a region in geojson format
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Feature), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRegionBorder(string name)
        {
            _logger.LogInformation("");
            try
            {
                var region = await _services.GetRegionBorders(name);

                if(region == null)
                {
                    return NotFound();
                }

                return Ok(region);

            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
