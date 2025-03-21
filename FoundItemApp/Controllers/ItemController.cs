
using FoundItemApp.Dto.Item;
using FoundItemApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FoundItemApp.Models.Enums;
using FoundItemApp.Helpers;
using FoundItemApp.Dto.Item.ReponseType;


namespace FoundItemApp.Controllers
{
    /// <summary>
    /// The controller for Item related actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _services;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService services, ILogger<ItemController> logger)
        {
            _services = services;
            _logger = logger;
        }

        /// <summary>
        ///  Gets Items based on input region, category, status and date found
        /// </summary>
        /// <param name="regionId">The id of the region</param>
        /// <param name="category">The category of the item</param>
        /// <param name="status">The status of the item</param>
        /// <param name="date">The date it was found</param>
        /// <param name="pageNumber">The number of the page requested</param>
        /// <param name="pageSize">The number of items requested</param>
        /// <returns>Returns a list of Items</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ItemDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllItems([FromQuery] int? regionId, [FromQuery] ItemCategory? category, [FromQuery] ItemStatus? status, [FromQuery] DateOnly? date, [FromQuery] int pageNumber, [FromQuery] int pageSize) 
        {

            var items = await _services.GetAllItems(regionId, category, status, date, pageNumber, pageSize); 

            if(items  == null)
            {
                _logger.LogInformation("404 NotFound: No items have been found");
                return NotFound();
            }

            return Ok(items); 
        }

        /// <summary>
        /// Gets the items coordinates to display on the map.
        /// </summary>
        /// <param name="regionId">The id of the region.</param>
        /// <returns>Returns a GeoJSON file with the coordinates of all items from the region.</returns>
        [HttpGet("{regionName}/geojson")]
        [ProducesResponseType(typeof(GetGeojsonResponseType), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetItemGeojson([FromRoute] int regionId) 
        {
            var items = await _services.GetItemGeojson(regionId);

            if (items == null)
            {
                _logger.LogInformation("404 NotFound: No items in {region} have been found", regionId);
                return NotFound();
            }

            return Ok(items);
        }

        /// <summary>
        /// Get the information for a profile of an item
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <returns>Information on the single item</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetItemByIdDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetItemProfile([FromRoute] Guid id) 
        {
            var item = await _services.GetItemProfile(id);

            if(item == null)
            {
                _logger.LogInformation("404 NotFound: The {id} has not been found", id);
                return NotFound(); 
            }
            return Ok(item);

        }

            
        /// <summary>
        /// Get all item categories
        /// </summary>
        /// <returns>List of all categories</returns>
        [HttpGet("categories")]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetItemCategories() 
        {
            var categories = _services.GetItemCategories(); 

            if(categories == null) {
                _logger.LogInformation("404 NotFound: Item categories have not been found");
                return NotFound();
            }

            return Ok(categories); 
        }
        
        /// <summary>
        /// Get an image of an item
        /// </summary>
        /// <param name="path">The image path</param>
        /// <returns>An image</returns>
        [HttpGet("{path}/image")]
        [ProducesResponseType(typeof(FileStream), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetImage([FromRoute] string path) 
        {
            var image = await ImageHelpers.GetImage(path);

            if(image == null)
            {
                _logger.LogInformation("404 NotFound: The {path} has not been found", path);
                return NotFound();
            }

            return Ok(image);
        }


        /// <summary>
        /// Creates a new Item in the database
        /// </summary>
        /// <param name="createItemDto">A DTO containing all the information on the Item</param>
        /// <returns>Returns a message confirming successful operation.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemDto createItemDto) 
        { 
            if(!ModelState.IsValid) 
            {
                _logger.LogInformation("401 BadRequest: Invalid data input to create new item");
                return BadRequest(ModelState);
            }

            await _services.CreateItem(createItemDto);

            return StatusCode(201, "new Item created successfully");
        }

        /// <summary>
        /// Updates the item 
        /// </summary>
        /// <param name="=id">The id of the item</param>
        /// <param name="updateItemDto">A DTO containing the new updated information of the item</param>
        /// <returns>Returns 'NoContent' if operation successful</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateItemDto updateItemDto, [FromRoute] Guid id) 
        { 
            if(!ModelState.IsValid)
            {
                _logger.LogInformation("401 BadRequest: Invalid data input to update {id}", id);
                return BadRequest(ModelState);
            }

            var item = await _services.UpdateItem(id, updateItemDto);

            if(item == null)
            {
                _logger.LogInformation("404 NotFound: The {id} item has not been found", id);
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <returns>Returns 'NoContent' if operation successful</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id) 
        { 
            var item = await _services.DeleteItem(id);

            if( item == null)
            {
                _logger.LogInformation("404 NotFound: The {id} item has not been found", id);
                return NotFound();
            }

            return NoContent();
        }


        /// <summary>
        /// Update just the status of the item
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <param name="PatchItemDto">The DTO containing the information to patch</param>
        /// <returns>Returns 'NoContent' if operation successful</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PatchItem([FromRoute] Guid id, [FromBody] ItemStatus status) 
        {
            var item = await _services.PatchItem(status, id);

            if (item == null)
            {
                _logger.LogInformation("404 NotFound: The {id} item has not been found", id);
                return NotFound();
            }
       
            return NoContent();
        }
    }
}
