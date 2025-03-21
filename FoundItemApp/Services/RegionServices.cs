using FoundItemApp.Data;
using FoundItemApp.Dto.Region;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using System.Linq;
using FoundItemApp.Interfaces;

namespace FoundItemApp.Services
{
    public class RegionServices : IRegionServices
    {
        private readonly DataContext _context;

        public RegionServices(DataContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<int, string>?> GetAllRegionNames()
        {
            var regions = await _context.Regions.ToDictionaryAsync(r => r.Id, r => r.Name);

            if(regions == null)
            { 
                return null;
            }

            return regions;
        }

        public async Task<double[]?> GetRegionEnvelope(int regionId)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == regionId);

            if(region == null)
            {
                return null;
            }

            Envelope env = region.Borders.EnvelopeInternal;

            double[] envelope = { env.MinY, env.MinX, env.MaxY, env.MaxX };

            return envelope;
        }

        public async Task<Feature?> GetRegionBorders(int regionId)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == regionId);

            if(region == null)
            {
                return null;
            }

            Envelope env = region.Borders.EnvelopeInternal;

            double[] envelope = {env.MinY, env.MinX, env.MaxY, env.MaxX };

            AttributesTable attributeTable = new AttributesTable
            {
                {"name", region.Name},
                {"envelope", envelope},

            };

            Feature feature = new Feature(region.Borders, attributeTable);

            return feature;


        }
    }
}
