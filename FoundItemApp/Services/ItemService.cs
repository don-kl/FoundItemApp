﻿using FoundItemApp.Data;
using FoundItemApp.Dto.Item;
using FoundItemApp.Models;
using FoundItemApp.Models.Enums;
using FoundItemApp.Helpers;
using NetTopologySuite.Geometries;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;
using FeatureCollection = NetTopologySuite.Features.FeatureCollection;
using FoundItemApp.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Azure;
using Microsoft.IdentityModel.Tokens;

namespace FoundItemApp.Services
{
    public class ItemService : IItemService
    {
        private readonly DataContext _context; 

        public ItemService(DataContext context)
        {
            _context = context;
        }

        public async Task CreateItem(CreateItemDto item)
        {
            var imagePaths = ImageHelpers.UploadImages(item.Pictures);

            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Name == item.RegionName);

            Point coord = new Point(item.Latitude, item.Longitude);

            if(!region.Borders.Contains(coord) || region == null)
            {
                throw new InvalidOperationException("The name of the region does not exist or the coordinates or not within the region"); 
            }

            Item newItem = new Item
            {
                Id = new Guid(),
                Description = item.Description,
                Title = item.Title,
                Location = item.Location,
                Coordinates = coord,
                Category = item.Category,
                Status = ItemStatus.Missing,
                DateFound = item.DateFound,
                TimeFound = item.TimeFound,
                PostedDate = new DateTime(),
                RegionId = region.Id,
                Images = imagePaths != null ? new List<string>((IList<string>)imagePaths) : new List<string>()
            };

        _context.Items.Add(newItem);

        await _context.SaveChangesAsync();

        }

        public async Task<Item?> DeleteItem(Guid id)
        {
            var foundItem = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);

            if(foundItem == null)
            {
                return null;
            }

            _context.Items.Remove(foundItem);

            await _context.SaveChangesAsync();

            return foundItem;
        }

        public async Task<FeatureCollection?> GetItemGeojson(string regionName)
        {
            var items = await _context.Items.Where(x => x.Region.Name == regionName).ToListAsync();

            if (items.Count == 0)
            {
                return null;
            }

            FeatureCollection featureCollection = new FeatureCollection();

            foreach(var item in items)
            {
                AttributesTable attributeTable = new AttributesTable
                {
                    {"Id", item.Id },
                    {"Category", item.Category},
                    {"Date", item.DateFound }
                };

                featureCollection.Add(new Feature(item.Coordinates, attributeTable));
            }

            return featureCollection;
        }

        public async Task<GetItemByIdDto?> GetItemProfile(Guid id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(item => item.Id == id);

            if (item == null)
            {
                return null;
            }

            var foundItemDTo = new GetItemByIdDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Location = item.Location,
                UserEmail = item.UserEmail,
                Category = item.Category,
                DateFound = item.DateFound,
                TimeFound = item.TimeFound,
                RegionName = item.Region.Name,
                Pictures = new List<string>(item.Images)
            };

            return foundItemDTo;
        }

        public List<string>? GetItemCategories()
        {
            var categoryList = Enum.GetValues(typeof(ItemCategory)).Cast<string>().ToList(); 

            if(categoryList.Count == 0)
            {
                return null; 
            }

            return categoryList;
        }

        public async Task<Item?> PatchItem(JsonPatchDocument<Item> patchDocument, Guid id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(item => item.Id == id);

            if (item == null)
            {
                return null;
            }

            patchDocument.ApplyTo(item);

            return item;
        }

        public async Task<Item?> UpdateItem(Guid id, UpdateItemDto updateItem)
        {
            var item = await _context.Items.FirstOrDefaultAsync(item => item.Id == id);

            if (item == null)
            {
                return null;
            }

            var region = await _context.Regions.FirstOrDefaultAsync(region => region.Name == updateItem.RegionName);

            Point coord = new Point(updateItem.Latitude, updateItem.Longitude);

            if (!region.Borders.Contains(coord) || region == null)
            {
                throw new InvalidOperationException("The name of the region does not exist or the coordinates or not within the region");
            }

            var imagePaths = ImageHelpers.UploadImages(updateItem.Images);

            item.Description = updateItem.Description;
            item.Title = updateItem.Title;
            item.Location = updateItem.Location;
            item.UserEmail = updateItem.UserEmail;
            item.Coordinates = new Point(updateItem.Longitude, updateItem.Latitude);
            item.Category = updateItem.Category;
            item.DateFound = updateItem.DateFound;
            item.TimeFound = updateItem.TimeFound;
            item.RegionId = region.Id;
            item.Images = imagePaths != null ? new List<string>((IList<string>)imagePaths) : new List<string>();

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<List<ItemDto>?> GetAllItems(string? region, ItemCategory? category, ItemStatus? status, DateOnly? date, int pageNumber, int pageSize)
        {

            var itemsQuery = _context.Items.AsQueryable();

            if (itemsQuery == null)
            {
                return null;
            }

            if (!region.IsNullOrEmpty())
            {
                itemsQuery = itemsQuery.Where(item => item.Region.Name == region);
            }

            if (category != null)
            {
                itemsQuery = itemsQuery.Where(item => item.Category == category);
            }

            if (category != null)
            {
                itemsQuery = itemsQuery.Where(item => item.Status == status);
            }

            if (date != null)
            {
                itemsQuery = itemsQuery.Where(item => item.DateFound >= date);
            }

            itemsQuery = itemsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var items = await itemsQuery.ToListAsync();

            List<ItemDto> result = new List<ItemDto>();

            foreach ( var item in items)
            {
                result.Add(new ItemDto
                {
                    Id = item.Id,
                    Title = item.Title,
                    Category = item.Category,
                    Status = item.Status,
                    DateFound = item.DateFound,
                    RegionName = item.Region.Name
                });
            }

            return result;

        }


    }
}
