using Azure;
using FoundItemApp.Dto.Item;
using FoundItemApp.Models;
using FoundItemApp.Models.Enums;
using Microsoft.AspNetCore.JsonPatch;
using FeatureCollection = NetTopologySuite.Features.FeatureCollection;

namespace FoundItemApp.Interfaces
{
    public interface IItemService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="category"></param>
        /// <param name="status"></param>
        /// <param name="date"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<ItemDto>?> GetAllItems(int? regionId, ItemCategory? category, ItemStatus? status, DateOnly? date, int pageNumber, int pageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        Task<FeatureCollection?> GetItemGeojson(int regionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetItemByIdDto?> GetItemProfile(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Item?> DeleteItem(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createItem"></param>
        /// <returns></returns>
        Task CreateItem(CreateItemDto createItem);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string>? GetItemCategories();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Item?> PatchItem(ItemStatus status, Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateItem"></param>
        /// <returns></returns>
        Task<Item?> UpdateItem(Guid id, UpdateItemDto updateItem);



    }
}
