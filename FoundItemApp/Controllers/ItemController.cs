using Azure;
using FoundItemApp.Models;
using FoundItemApp.Dto.Item;
using FoundItemApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using FoundItemApp.Models.Enums;
using FoundItemApp.Helpers;

namespace FoundItemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _services;
        private readonly ILogger _logger;

        public ItemController(IItemService services, ILogger logger)
        {
            _services = services;
            _logger = logger;
        }

        /// <summary>
        ///         
        /// </summary>
        /// <param name="region"></param>
        /// <param name="category"></param>
        /// <param name="status"></param>
        /// <param name="date"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllItems([FromQuery] string? region, [FromQuery] ItemCategory? category, [FromQuery] ItemStatus? status, [FromQuery] DateOnly? date, [FromQuery] int pageNumber, [FromQuery] int pageSize) 
        { 

            var items = await _services.GetAllItems(region, category, status, date, pageNumber, pageSize); 

            if(items  == null)
            {
                return NotFound();
            }

            return Ok(items); 
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionName"></param>
        /// <returns></returns>
        [HttpGet("geojson/{regionName}")]
        public async Task<IActionResult> GetItemGeojson([FromRoute] string regionName) 
        { 

            var items = await _services.GetItemGeojson(regionName);

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemProfile([FromRoute] Guid id) 
        {

            var item = await _services.GetItemProfile(id);

            if(item == null)
            {
                return NotFound(); 
            }
            return Ok(item);

        }

            
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories() 
        { 
            var categories = await Task.Run(() => _services.GetItemCategories()); 

            if(categories == null) {
                return NotFound();
            }

            return Ok(categories);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageAddress"></param>
        /// <returns></returns>
        [HttpGet("Image/{imageAddress}")]
        public async Task<IActionResult> GetImage([FromRoute] string imageAddress) 
        { 
            var image = await ImageHelpers.GetImage(imageAddress);

            if(image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createItemDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemDto createItemDto) 
        { 
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            await _services.CreateItem(createItemDto);

            return StatusCode(201, "ItemCreated");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateItemDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateItemDto updateItemDto, [FromRoute] Guid id) 
        { 
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _services.UpdateItem(id, updateItemDto);

            if(item == null)
            {  
                return NotFound();
            }

            return Ok("Item has been updated");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id) 
        { 
            var item = await _services.DeleteItem(id);

            if( item == null)
            {
                return NotFound();
            }

            return Ok("Item has been deleted");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonPatch"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchItem([FromRoute] Guid id, [FromBody] JsonPatchDocument<Item> jsonPatch) 
        {
            var item = await _services.PatchItem(jsonPatch, id);

            if(jsonPatch == null)
            {
                return NotFound();
            }

            return Ok("Item has been patched");
        }
    }
}
