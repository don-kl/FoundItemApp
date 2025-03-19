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

        Task<List<ItemDto>?> GetAllItems(string? region, ItemCategory? category, ItemStatus? status, DateOnly? date, int pageNumber, int pageSize);

        Task<FeatureCollection?> GetItemGeojson(string regionName);

        Task<GetItemByIdDto?> GetItemProfile(Guid id);

        Task<Item?> DeleteItem(Guid id);

        Task CreateItem(CreateItemDto createItem);

        List<string>? GetItemCategories();

        Task<Item?> PatchItem(ItemStatus status, Guid id);

        Task<Item?> UpdateItem(Guid id, UpdateItemDto updateItem);



    }
}
